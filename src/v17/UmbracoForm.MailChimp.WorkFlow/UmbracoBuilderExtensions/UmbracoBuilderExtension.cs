using System;
using System.Collections.Generic;
using System.Text;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Forms.Core.Providers;
using UmbracoForms.MailChimp.Workflow.Workflows;

namespace UmbracoForms.MailChimp.Workflow.UmbracoBuilderExtensions
{
    public static class UmbracoBuilderExtensions
    {
        public static IUmbracoBuilder AddMailChimpWorkFlowType(this IUmbracoBuilder builder)
        {
            builder.WithCollectionBuilder<WorkflowCollectionBuilder>().Add<MailChimpWorkFlowType>();
            return builder;
        }
    }
}
