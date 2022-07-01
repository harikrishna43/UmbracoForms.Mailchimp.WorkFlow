using Our.Umbraco.Forms.Mailchimp.Workflow.Extensions;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace Our.Umbraco.Forms.Mailchimp.Workflow.Composers
{
    public class MailChimpWorkflowTypeComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.AddMailChimpWorkFlowType();
        }
    }
}
