using AspNetCoreHero.ToastNotification.Abstractions;
using CoreMVC.ActionFilter;
using CoreMVC.Models;
using DataAccess;
using DataEntity;
using DataModel.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Json;
using static DataModel.Commons.EnumNames;

namespace CoreMVC.Controllers
{
    [SessionTimeout]
    //Check User has controller access or not. ...If controller got error then handled.
    //[TypeFilter(typeof(ControllerAuthorizeFilter))]
    public class DoctorsController : Controller
    {
        private IWebHostEnvironment _webHostEnvironment;
        private string _APISettings;
        public INotyfService _notyf { get; }
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;

        public DoctorsController(IWebHostEnvironment webHostEnvironment, INotyfService notyf, IUnitOfWork unitOfWork, IOptions<APIStringSetting> options, IHttpContextAccessor contextAccessor)
        {
            _webHostEnvironment = webHostEnvironment;
            _APISettings = options.Value.APISettings;
            _contextAccessor = contextAccessor;
            _unitOfWork = unitOfWork;
            _notyf = notyf;

        }

        public async Task<IActionResult> Index(string Message = "")
        {
            if (!string.IsNullOrEmpty(Message))
                ViewBag.Message = Message;

            List<DoctorEntity> doctors = new List<DoctorEntity>();
            //ResponseEntity<TokenEntity> tokenEntity = _unitOfWork.token.GetToken("Bhavsar");
            //string token = string.Empty;
            //if (tokenEntity.Entity != null)
            //{
            //    token = tokenEntity.Entity.AuthToken;
            //}

            //Get session using string object
            //string token = _contextAccessor.HttpContext.Session.GetString("_Token");

            //Get Authentication token value from custom object like Token Entity
            string token = _contextAccessor.HttpContext.Session.GetComplexData<TokenEntity>("tokenEntity").AuthToken;

            HttpClient client = HttpEvent.DefaultRequestHeaders(_APISettings, token);

            //var response = await client.GetAsync( string.Format("api/Students/AllStudents/{0}",0));

            var response = await client.GetAsync("api/Doctors/AllDoctors");
            if (response.IsSuccessStatusCode == true)
            {
                var result = await response.Content.ReadAsStringAsync();
                if (result != null)
                {
                    ResponseEntity<DoctorEntity> entity = JsonConvert.DeserializeObject<ResponseEntity<DoctorEntity>>(result);
                    if (entity.Status == true)
                    {
                        doctors = entity.listEntity;
                    }
                }
            }
            return View(doctors);
        }
        [HttpGet]
        public async Task<IActionResult> Details(decimal DoctorID)
        {
           
            DoctorEntity doctors = null;
            string token = _contextAccessor.HttpContext.Session.GetComplexData<TokenEntity>("tokenEntity").AuthToken;

            HttpClient client = HttpEvent.DefaultRequestHeaders(_APISettings, token);

            var response = await client.GetAsync(string.Format("api/Doctors/AllDoctors/{0}", DoctorID));
            
            if (response.IsSuccessStatusCode == true)
            {
                var result = await response.Content.ReadAsStringAsync();
                if (result != null)
                {
                    ResponseEntity<DoctorEntity> entity = JsonConvert.DeserializeObject<ResponseEntity<DoctorEntity>>(result);
                    if (entity.Status == true)
                    {
                        doctors = entity.Entity;
                    }
                }
            }
            return View(doctors);
        }

        public IActionResult Create()
        {
            var doctor = new DoctorEntity();
            return View(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DoctorEntity doctorEntity)
        {
            if (ModelState.IsValid)
            {
                //ResponseEntity<TokenEntity> tokenEntity = _unitOfWork.token.GetToken("Bhavsar");
                //string token = string.Empty;
                //if (tokenEntity.Entity != null)
                //{
                //    token = tokenEntity.Entity.AuthToken;
                //}
                string token = _contextAccessor.HttpContext.Session.GetString("_Token");
                HttpClient client = HttpEvent.DefaultRequestHeaders(_APISettings, token);

              //  doctorEntity.DoctorGender = (string)Enum.GetName(typeof(MyEnum), Convert.ToInt32(doctorEntity.DoctorGender));
                var response = await client.PostAsJsonAsync<DoctorEntity>("api/Doctors/InsertDoctor/", doctorEntity);

                if (response.IsSuccessStatusCode == true)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    if (result != null)
                    {
                        ResponseEntity<DoctorEntity> entity = JsonConvert.DeserializeObject<ResponseEntity<DoctorEntity>>(result);
                        if (entity.Status == true)
                        {
                            return RedirectToAction("Index", new { @Message = entity.ErrorMsg });
                            // return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewBag.Message = entity.ErrorMsg;
                            return View(doctorEntity);
                        }
                    }
                }
            }
            //clear the model
            ModelState.Clear();
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(doctorEntity);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(decimal DoctorID)
        {
            //List<DoctorEntity> doctors = new List<DoctorEntity>();
            DoctorEntity doctors = null;
            string token = _contextAccessor.HttpContext.Session.GetComplexData<TokenEntity>("tokenEntity").AuthToken;

            HttpClient client = HttpEvent.DefaultRequestHeaders(_APISettings, token);

            var response = await client.GetAsync(string.Format("api/Doctors/AllDoctors/{0}", DoctorID));
            //var response = await client.GetAsync("api/Doctors/AllDoctors");
            if (response.IsSuccessStatusCode == true)
            {
                var result = await response.Content.ReadAsStringAsync();
                if (result != null)
                {
                    ResponseEntity<DoctorEntity> entity = JsonConvert.DeserializeObject<ResponseEntity<DoctorEntity>>(result);
                    if (entity.Status == true)
                    {
                        doctors = entity.Entity;
                    }
                }
            }
            return View(doctors);
        }
        public async Task<IActionResult> Update(DoctorEntity doctorEntity)
        {
            if (ModelState.IsValid)
            {
                //ResponseEntity<TokenEntity> tokenEntity = _unitOfWork.token.GetToken("Bhavsar");
                //string token = string.Empty;
                //if (tokenEntity.Entity != null)
                //{
                //    token = tokenEntity.Entity.AuthToken;
                //}
                string token = _contextAccessor.HttpContext.Session.GetString("_Token");
                HttpClient client = HttpEvent.DefaultRequestHeaders(_APISettings, token);

            //    doctorEntity.DoctorGender = (string)Enum.GetName(typeof(MyEnum), Convert.ToInt32(doctorEntity.DoctorGender));
                var response = await client.PostAsJsonAsync<DoctorEntity>("api/Doctors/UpdateDoctor/", doctorEntity);

                if (response.IsSuccessStatusCode == true)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    if (result != null)
                    {
                        ResponseEntity<DoctorEntity> entity = JsonConvert.DeserializeObject<ResponseEntity<DoctorEntity>>(result);
                        if (entity.Status == true)
                        {
                            return RedirectToAction("Index", new { @Message = entity.ErrorMsg });
                            // return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewBag.Message = entity.ErrorMsg;
                            return View(doctorEntity);
                        }
                    }
                }
            }
            //clear the model
            ModelState.Clear();
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(doctorEntity);
        }
        public async Task<IActionResult> Delete(DoctorEntity doctorEntity)
        {
            if (ModelState.IsValid)
            {
                
                string token = _contextAccessor.HttpContext.Session.GetString("_Token");
                HttpClient client = HttpEvent.DefaultRequestHeaders(_APISettings, token);

            //    studentEntity.StudentGender = (string)Enum.GetName(typeof(MyEnum), Convert.ToInt32(studentEntity.StudentGender));
                var response = await client.PostAsJsonAsync<DoctorEntity>("api/Doctors/DeleteDoctor/", doctorEntity);

                if (response.IsSuccessStatusCode == true)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    if (result != null)
                    {
                        ResponseEntity<DoctorEntity> entity = JsonConvert.DeserializeObject<ResponseEntity<DoctorEntity>>(result);
                        if (entity.Status == true)
                        {
                            return RedirectToAction("Index", new { @Message = entity.ErrorMsg });
                            // return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewBag.Message = entity.ErrorMsg;
                            return View(doctorEntity);
                        }
                    }
                }
            }
            //clear the model
            ModelState.Clear();
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(doctorEntity);
        }
    }
}
