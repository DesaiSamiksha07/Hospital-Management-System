using CoreWebAPI.ActionFilter;
using DataEntity;
using DataModel.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebAPI.Controllers
{
    [AuthorizationRequired]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public PatientsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

       /* [HttpGet]
        [Route("Patients/AllPatients/{PatientID?}")]
        [ValidateModel]
        public ResponseEntity<PatientEntity> GetPatients(decimal PatientID = 0)
        {
            return _unitOfWork.patient.GetAllPatients(PatientID);
        }*/
        

       /* [HttpPost]
        [Route("InsertPatient")]
        [ValidateModel]

        public ResponseEntity<PatientEntity> InsertPatient([FromBody] PatientEntity patientEntity)
        {
            return _unitOfWork.patient.InsertPatient(patientEntity);
        }
        [HttpDelete]
        [Route("DeletePatient/{PatientID}")]
        [ValidateModel]
        public ResponseEntity<PatientEntity> DeletePatients([FromBody] PatientEntity patientEntity)
        {
            return _unitOfWork.patient.DeletePatients(patientEntity);
        }

        // Update a Patient's details
        [HttpPut]
        [Route("UpdatePatient")]
        [ValidateModel]
        public ResponseEntity<PatientEntity> UpdatePatients([FromBody] PatientEntity patientEntity)
        {
            return _unitOfWork.patient.UpdatePatients(patientEntity);
        }*/
    }
}
