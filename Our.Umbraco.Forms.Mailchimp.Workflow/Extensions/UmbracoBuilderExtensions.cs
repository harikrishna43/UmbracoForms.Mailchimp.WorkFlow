using Our.Umbraco.Forms.Mailchimp.Workflow.Workflows;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Forms.Core.Providers;

namespace Our.Umbraco.Forms.Mailchimp.Workflow.Extensions
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
