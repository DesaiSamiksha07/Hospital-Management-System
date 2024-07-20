using CoreMVC.Models;
using DataEntity;
using DataModel.IRepository;
using DataModel.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private const string SessionToken = "_Token";
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
            // Set session using string object
            _contextAccessor.HttpContext.Session.SetString("_Token", GetToken());
            //GetToken();
        }   

        private string GetToken()
        {
            List<DoctorEntity> doctors = new List<DoctorEntity>();
            ResponseEntity<TokenEntity> tokenEntity = _unitOfWork.token.GetToken("Bhavsar");
            string token = string.Empty;
            if (tokenEntity.Entity != null)
            {
                //Example: set session with complex object like Token Entity
                _contextAccessor.HttpContext.Session.SetComplexData("tokenEntity",tokenEntity.Entity);
                token = tokenEntity.Entity.AuthToken;
            }
            return token;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
