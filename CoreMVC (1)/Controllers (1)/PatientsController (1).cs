using AspNetCoreHero.ToastNotification.Abstractions;
using CoreMVC.ActionFilter;
using CoreMVC.Models;
using DataAccess;
using DataEntity;
using DataModel.IRepository;
using DataModel.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using static DataModel.Commons.EnumNames;

namespace CoreMVC.Controllers
{
    [SessionTimeout]
    public class PatientsController : Controller
    {
        private IWebHostEnvironment _webHostEnvironment;
        private string _APISettings;
        public INotyfService _notyf { get; }
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;

        public PatientsController(IWebHostEnvironment webHostEnvironment, INotyfService notyf, IUnitOfWork unitOfWork, IOptions<APIStringSetting> options, IHttpContextAccessor contextAccessor)
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

            List<PatientEntity> patients = new List<PatientEntity>();
            string token = _contextAccessor.HttpContext.Session.GetComplexData<TokenEntity>("tokenEntity").AuthToken;

            HttpClient client = HttpEvent.DefaultRequestHeaders(_APISettings, token);

            var response = await client.GetAsync("api/Patients/AllPatients");
            if (response.IsSuccessStatusCode == true)
            {
                var result = await response.Content.ReadAsStringAsync();
                if (result != null)
                {
                    ResponseEntity<PatientEntity> entity = JsonConvert.DeserializeObject<ResponseEntity<PatientEntity>>(result);
                    if (entity.Status == true)
                    {
                        patients = entity.listEntity;
                    }
                }
            }
            return View(patients);
        }

        public async Task<IActionResult> Details(decimal PatientID)
        {

            PatientEntity patients = null;
            string token = _contextAccessor.HttpContext.Session.GetComplexData<TokenEntity>("tokenEntity").AuthToken;

            HttpClient client = HttpEvent.DefaultRequestHeaders(_APISettings, token);

            var response = await client.GetAsync(string.Format("api/Patients/AllPatients/{0}", PatientID));

            if (response.IsSuccessStatusCode == true)
            {
                var result = await response.Content.ReadAsStringAsync();
                if (result != null)
                {
                    ResponseEntity<PatientEntity> entity = JsonConvert.DeserializeObject<ResponseEntity<PatientEntity>>(result);
                    if (entity.Status == true)
                    {
                        patients = entity.Entity;
                    }
                }
            }
            return View(patients);
        }


        public IActionResult Create()
        {
            var patient = new PatientEntity();
            return View(patient);
        }



        [HttpPost]
        public async Task<IActionResult> Create(PatientEntity patientEntity)
        {
            if (ModelState.IsValid)
            {
                string token = _contextAccessor.HttpContext.Session.GetString("_Token");
                HttpClient client = HttpEvent.DefaultRequestHeaders(_APISettings, token);

                patientEntity.Gender = (string)Enum.GetName(typeof(MyEnum), Convert.ToInt32(patientEntity.Gender));
                var response = await client.PostAsJsonAsync<PatientEntity>("api/Patients/InsertPatient/", patientEntity);

                if (response.IsSuccessStatusCode == true)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    if (result != null)
                    {
                        ResponseEntity<PatientEntity> entity = JsonConvert.DeserializeObject<ResponseEntity<PatientEntity>>(result);
                        if (entity.Status == true)
                        {
                            return RedirectToAction("Index", new { @Message = entity.ErrorMsg });

                        }
                        else
                        {
                            ViewBag.Message = entity.ErrorMsg;
                            return View(patientEntity);
                        }
                    }
                }
            }
            //clear the model
            ModelState.Clear();
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(patientEntity);
        }

    }
}