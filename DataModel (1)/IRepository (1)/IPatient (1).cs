using DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.IRepository
{
    public interface IPatient : IRepository<PatientEntity>
    {
        ResponseEntity<PatientEntity> InsertPatient(PatientEntity patientEntity);
        ResponseEntity<PatientEntity> GetAllPatient(decimal PatientId);
        ResponseEntity<PatientEntity> DeletePatient(PatientEntity patientEntity);
        ResponseEntity<PatientEntity> UpdatePatient(PatientEntity patientEntity);

    }
}
