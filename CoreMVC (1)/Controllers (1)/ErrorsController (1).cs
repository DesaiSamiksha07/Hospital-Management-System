using Microsoft.AspNetCore.Mvc;

namespace CoreMVC.Controllers
{
    public class ErrorsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ValidateSession(string code)
        {
            ViewBag.Message = code;
            return View();
        }
    }
}
