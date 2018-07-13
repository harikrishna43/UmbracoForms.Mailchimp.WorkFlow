using MailChimp;
using MailChimp.Helper;
using MailChimp.Lists;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Umbraco.Forms.Core;
using Umbraco.Forms.Core.Attributes;
using Umbraco.Forms.Core.Enums;
using Umbraco.Forms.Core.Providers.Models;

namespace UmbracoForms.MailChimp
{
    public class UmbracoMacilChimpFlowType : WorkflowType
    {
        [Setting("API KEY", description = "Enter the Mailchimp API key.", view = "TextField")]
        public string ApiKey { get; set; }

        [Setting("List ID", description = "Enter the Mailchimp List ID.)", view = "TextField")]
        public string ListID { get; set; }

        [Setting("Fields", description = "Map the needed fields .Minimum Email field for subscribe.", view = "FieldMapper")]
        public string Fields { get; set; }

        public string Email { get; set; }

        public UmbracoMacilChimpFlowType()
        {
            this.Id = new Guid("aBc142b1-f12e-be01-af31-9a5860AB4510");//00000000-0000-0000-0000-000000000000
            this.Name = "Subscribe(MailChimp)";
            this.Description = "Subscribe email address using MailChimp";
            this.Icon = "icon-autofill";
            //this.Group = nameof(ApiKey);
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

        public override WorkflowExecutionStatus Execute(Record record, RecordEventArgs e)
        {
            try
            {
                List<FieldMapping> source = new List<FieldMapping>();
                if (!string.IsNullOrEmpty(this.Fields))
                    source = JsonConvert.DeserializeObject<IEnumerable<FieldMapping>>(this.Fields).ToList<FieldMapping>();

                var data = new MergeVar();
                if (source.Any<FieldMapping>())
                {
                    foreach (FieldMapping fieldMapping in source)
                    {

                        string alias = fieldMapping.Alias;
                        string str = string.IsNullOrEmpty(fieldMapping.StaticValue) ? record.RecordFields[new Guid(fieldMapping.Value)].ValuesAsString(false) : fieldMapping.StaticValue;
                        bool isEmail = Regex.IsMatch(str, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                        if (isEmail)
                        {
                            this.Email = str;
                        }
                        data.Add(alias, str);
                    }
                }

                var mc = new MailChimpManager(this.ApiKey);
                var subsciberListID = this.ListID;

                var email = new EmailParameter()
                {
                    Email = this.Email
                };
                EmailParameter results = mc.Subscribe(subsciberListID, email, data, "html", false, true, false, false);

                return WorkflowExecutionStatus.Completed;

            }
            catch (Exception ex)
            {
                Umbraco.Core.Logging.LogHelper.Error<string>("error : FlowType", ex);
                return WorkflowExecutionStatus.Failed;
            }
        }
    }
}
