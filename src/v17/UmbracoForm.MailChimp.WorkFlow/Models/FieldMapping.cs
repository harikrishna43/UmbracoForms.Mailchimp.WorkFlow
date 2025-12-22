using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace UmbracoForms.MailChimp.Workflow.Models
{
    [DataContract(Name = "fieldMapping")]
    public class FieldMapping
    {
        //
        // Summary:
        //     Gets or sets the alias for the field mapping.
        [DataMember(Name = "alias")]
        public string Alias { get; set; }

        //
        // Summary:
        //     Gets or sets the value to use for the field mapping.
        [DataMember(Name = "value")]
        public string Value { get; set; }

        //
        // Summary:
        //     Gets or sets a static value to use for the field mapping, in preference to the
        //     dynamic value provided by Umbraco.Forms.Core.Providers.Models.FieldMapping.Value.
        [DataMember(Name = "staticValue")]
        public string StaticValue { get; set; }
    }
}
