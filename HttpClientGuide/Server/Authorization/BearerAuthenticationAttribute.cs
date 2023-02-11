using System.Net.Http.Headers;
using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HttpClientGuide.Server.Authorization
{
    public class BearerAuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("Bearer", out var bearer))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (bearer != "FakeBearerToken")
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            base.OnActionExecuting(context);
        }
    }

}
