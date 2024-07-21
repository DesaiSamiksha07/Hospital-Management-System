using CoreWebAPI.ActionFilter;
using DataEntity;
using DataModel.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebAPI.Controllers
{
    [AuthorizationRequired]
    [Route("api/[controller]")]
    //[Route("Doctors]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public DoctorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


    
        [HttpGet]
        [Route("AllDoctors/{DoctorID?}")]
        [ValidateModel]
        public ResponseEntity<DoctorEntity> GetDoctors(decimal DoctorID = 0)
        {
            return _unitOfWork.doctorsInfo.GetAllDoctors(DoctorID);
        }


        [HttpPost]
        [Route("InsertDoctor")]
        [ValidateModel]

        public ResponseEntity<DoctorEntity> InsertDoctors([FromBody] DoctorEntity doctorEntity)
        {
            return _unitOfWork.doctorsInfo.InsertDoctor(doctorEntity);
        }

        [HttpDelete]
        [Route("DeleteDoctor/")]
        [ValidateModel]
        public ResponseEntity<DoctorEntity> DeleteDoctors([FromBody] DoctorEntity doctorEntity)
        {
            return _unitOfWork.doctorsInfo.DeleteDoctor(doctorEntity);
        }

        // Update a doctor's details
        [HttpPut]
        [Route("api/Doctors/UpdateDoctor/{DoctorID}")]
        [ValidateModel]
        public ResponseEntity<DoctorEntity> UpdateDoctors([FromBody] DoctorEntity doctorEntity)
        {
            return _unitOfWork.doctorsInfo.UpdateDoctor(doctorEntity);
        }
    }
}
