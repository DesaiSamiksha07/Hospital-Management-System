using DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.IRepository
{
    public interface IDoctors_Info : IRepository<DoctorEntity>
    {
        ResponseEntity<DoctorEntity> InsertDoctor(DoctorEntity doctorEntity);
        ResponseEntity<DoctorEntity> GetAllDoctors(decimal DoctorID);
        ResponseEntity<DoctorEntity> DeleteDoctor(DoctorEntity doctorEntity);
        ResponseEntity<DoctorEntity> UpdateDoctor(DoctorEntity doctorEntity);
    }
}
