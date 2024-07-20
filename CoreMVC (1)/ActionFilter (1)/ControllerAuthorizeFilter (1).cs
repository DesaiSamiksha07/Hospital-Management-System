using DataAccess;
using DataModel.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.SignalR;
using System.Net;

namespace CoreMVC.ActionFilter
{
    public class ControllerAuthorizeFilter : ActionFilterAttribute
    {
        private ApplicationDbContext _context { get; set; }

        private readonly IMethodService _dataService;
        public ControllerAuthorizeFilter(IMethodService dataService) { 
            _dataService = dataService;
        }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var ActionName = ((ControllerBase)filterContext.Controller).ControllerContext.ActionDescriptor.ActionName;
            var ControllerName = ((ControllerBase)filterContext.Controller).ControllerContext.ActionDescriptor.ControllerName;
            bool IsAdmin = false;
            string UserId = "Shalin";
            if(IsAdmin == false)
            {
                //if(!_dataService.ValidateMethodMaster(UserId ,ControllerName, ActionName))
                if(!_dataService.ValidateControllerMaster(UserId ,ControllerName, ActionName))
                {
                    var responseMessage = HttpStatusCode.Unauthorized.ToString() + ", You are not authorize to access " + ControllerName + " request.";
                    var values = new RouteValueDictionary(
                    new
                    {
                        Action = "Validate",
                        Controller = "Errors",
                        code = responseMessage.ToString()
                    });
                    filterContext.Result = new RedirectToRouteResult(values);
                }
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if(filterContext.Exception != null)
            {
                filterContext.ExceptionHandled = true;
                var ActionName = ((ControllerBase)filterContext.Controller).ControllerContext.ActionDescriptor.ActionName;
                var ControllerName = ((ControllerBase)filterContext.Controller).ControllerContext.ActionDescriptor.ControllerName;
                var values = new RouteValueDictionary(
                   new
                   {
                       Action = "Validate",
                       Controller = "Errors",
                       code = ControllerName + "/" + ", Error: " + filterContext.Exception.Message
                   }) ;
                filterContext.Result = new RedirectToRouteResult(values);
            }
        }


    }
}
