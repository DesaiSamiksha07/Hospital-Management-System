using CoreMVC.Models;
using DataEntity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace CoreMVC.ActionFilter
{
    public class SessionTimeoutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(filterContext.HttpContext.Session.GetString("_Token") == null)
            {
                var responseMessage = HttpStatusCode.Gone.ToString() + ", Session timeout has expired.";
                var values = new RouteValueDictionary(
                    new
                    {
                        Action = "ValidateSession",
                        Controller = "Errors",
                        code = responseMessage.ToString()
                    });
                filterContext.Result = new RedirectToRouteResult(values);
            }
            

            base.OnActionExecuting(filterContext);
        }
    }
}
