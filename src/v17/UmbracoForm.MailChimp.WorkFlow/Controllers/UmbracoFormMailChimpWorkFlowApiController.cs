using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UmbracoForm.MailChimp.WorkFlow.Controllers
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "UmbracoForm.MailChimp.WorkFlow")]
    public class UmbracoFormMailChimpWorkFlowApiController : UmbracoFormMailChimpWorkFlowApiControllerBase
    {

        [HttpGet("ping")]
        [ProducesResponseType<string>(StatusCodes.Status200OK)]
        public string Ping() => "Pong";
    }
}
