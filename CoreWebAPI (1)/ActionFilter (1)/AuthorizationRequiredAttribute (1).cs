using DataModel.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CoreWebAPI.ActionFilter
{
    public class AuthorizationRequiredAttribute : Attribute, IAsyncActionFilter
    {
        private const string apiKey = "App-Key";
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //In request ,Pass App-Key with Token in Headers
            var provider = context.HttpContext.RequestServices.GetService<IToken>();
            if(!context.HttpContext.Request.Headers.TryGetValue(apiKey, out var token))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "API Key is not Given"
                };
                return;
            }

            //Check token is valid or not
            if(provider != null && !provider.CheckValidateToken(token,out string error))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = error // Api key is not valid
                };
                return;
            }
            await next();
        }
    }
}
