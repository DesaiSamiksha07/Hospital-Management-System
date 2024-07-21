using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Commons
{
    public static class SpName
    {
        #region "Token"
        public const string GetValidateToken = "usp_GetValidateToken";
        public const string GetTokenMaster = "usp_GetTokenMaster";
        #endregion "Token"

        #region "Doctors"
        public const string GetDoctors = "usp_GetDoctors";
        public const string InsertDoctor = "usp_InsertDoctor";
        public const string DeleteDoctor = "usp_DeleteDoctor";
        public const string UpdateDoctor = "usp_UpdateDoctor";
        #endregion "Doctors"

       

        #region "Patients"
        public const string GetPatients = "usp_GetPatients";
        public const string InsertPatient = "usp_InsertPatient";
        public const string DeletePatient = "usp_DeletePatient";
        public const string UpdatePatient = "usp_UpdatePatient";
        #endregion "Patients"

    }
}
