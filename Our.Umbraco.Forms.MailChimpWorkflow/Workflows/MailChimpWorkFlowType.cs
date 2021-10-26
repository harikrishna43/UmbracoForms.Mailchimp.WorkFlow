﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MailChimp.Net;
using MailChimp.Net.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Umbraco.Forms.Core;
using Umbraco.Forms.Core.Enums;
using Umbraco.Forms.Core.Interfaces;
using Umbraco.Forms.Core.Providers.Models;
using UmbracoSetting = Umbraco.Forms.Core.Attributes.Setting;

namespace Our.Umbraco.Forms.Mailchimp.Workflow.Workflows
{
    public class MailChimpWorkFlowType : WorkflowType
    {
        private readonly ILogger<MailChimpWorkFlowType> _logger;
        #region Settings

        [UmbracoSetting("API KEY", View = "TextField", Description = "Enter the Mailchimp API key.")]
        public string ApiKey { get; set; }

        [UmbracoSetting("List ID", View = "TextField", Description = "Enter the Mailchimp List ID.")]
        public string ListID { get; set; }

        [UmbracoSetting("Fields", View = "FieldMapper", Description = "Map the needed fields .Minimum Email field for subscribe.")]
        public string Fields { get; set; }

        [UmbracoSetting("Tags", View = "TextField", Description = "List of Tags. Separate by semicolon ';'. Tag must be created before being used. i.e: User; Help Center")]
        public string Tags { get; set; }

        #endregion

        public MailChimpWorkFlowType(ILogger<MailChimpWorkFlowType> logger)
        {
            _logger = logger;
            this.Id = new Guid("aBc142b1-f12e-be01-af31-9a5860AB4510");
            this.Name = "Subscribe(MailChimp)";
            this.Description = "Subscribe email address using MailChimp";
            this.Icon = "icon-autofill";
        }

        public override WorkflowExecutionStatus Execute(WorkflowExecutionContext context)
        {
            try
            {
                var (email, mergeFields) = ParseEmailAndMergeFields(context.Record, Fields);
                if (string.IsNullOrEmpty(email))
                {
                    throw new Exception("Email is missing");
                }

                Task.Run(async () =>
                {
                    await SubscribeMember(email, mergeFields);

                    var tagNames = ParseTags(Tags).ToList();
                    if (tagNames.Any())
                    {
                        await TagMember(email, tagNames);
                    }
                });

                return WorkflowExecutionStatus.Completed;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error : UFMailchimp FlowType");
                return WorkflowExecutionStatus.Failed;
            }
        }

        public override List<Exception> ValidateSettings()
        {
            List<Exception> exceptionList = new List<Exception>();
            if (string.IsNullOrEmpty(this.ApiKey))
                exceptionList.Add(new Exception("'API Key' setting has not been set"));
            if (string.IsNullOrEmpty(this.ListID))
                exceptionList.Add(new Exception("'List ID' setting has not been set'"));
            if (string.IsNullOrEmpty(this.Fields))
                exceptionList.Add(new Exception("'Fields' setting has not been set. set minimum email field.'"));
            return exceptionList;
        }

        private async Task SubscribeMember(string email, Dictionary<string, object> mergeFields)
        {
            var mc = new MailChimpManager(this.ApiKey);

            var member = new Member
            {
                EmailAddress = email,
                Status = Status.Subscribed,
                EmailType = "html",
                TimestampOpt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                StatusIfNew = Status.Subscribed,
                MergeFields = mergeFields,
            };

            await mc.Members.AddOrUpdateAsync(this.ListID, member);
        }

        private async Task TagMember(string email, IEnumerable<string> tagNames)
        {
            var mc = new MailChimpManager(this.ApiKey);

            var tagIds = await GetExistingTagIds(tagNames);
            var member = new Member { EmailAddress = email };

            foreach (int tagId in tagIds)
            {
                await mc.ListSegments.AddMemberAsync(this.ListID, $"{tagId}", member);
            }
        }

        private async Task<IEnumerable<int>> GetExistingTagIds(IEnumerable<string> tagNames)
        {
            var mc = new MailChimpManager(this.ApiKey);

            var tagsMap = (await mc.ListSegments.GetAllAsync(this.ListID))
                .Where(s => s.Type == "static")
                .Aggregate(
                    new Dictionary<string, int>(),
                    (dict, seg) => {
                        dict.Add(seg.Name, seg.Id);
                        return dict;
                    }
                );

            var tagIds = tagNames
                .Where(t => tagsMap.ContainsKey(t))
                .Select(t => tagsMap[t]);

            return tagIds;
        }

        #region Helpers

        private static Tuple<string, Dictionary<string, object>> ParseEmailAndMergeFields(IRecord record, string fields)
        {
            if (string.IsNullOrEmpty(fields))
            {
                return null;
            }

            List<FieldMapping> source = JsonConvert.DeserializeObject<IEnumerable<FieldMapping>>(fields)?.ToList();
            if (source == null || !source.Any())
            {
                return null;
            }

            string email = "";
            var mergeFields = new Dictionary<string, object>();

            foreach (var fieldMapping in source)
            {

                string alias = fieldMapping.Alias;
                string str = string.IsNullOrEmpty(fieldMapping.StaticValue) ? record.RecordFields[new Guid(fieldMapping.Value)].ValuesAsString(false) : fieldMapping.StaticValue;
                if (IsEmail(str))
                {
                    email = str;
                }
                mergeFields.Add(alias, str);
            }

            return Tuple.Create(email, mergeFields);
        }

        private static IEnumerable<string> ParseTags(string tags)
        {
            return string.IsNullOrEmpty(tags) ? new List<string>() : tags.Split(';').Select(t => t.Trim());
        }

        private static bool IsEmail(string str)
        {
            return Regex.IsMatch(str, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }

        #endregion
    }
}
