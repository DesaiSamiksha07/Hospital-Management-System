using DataAccess;
using DataEntity;
using DataModel.Commons;
using DataModel.IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DataModel.Repository
{
    public class Patient : Repository<PatientEntity>, IPatient
    {
        private readonly ApplicationDbContext _context;
        public Patient(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public ResponseEntity<PatientEntity> GetAllPatient(decimal PatientID = 0)
        {
            ResponseEntity<PatientEntity> responseEntity = new ResponseEntity<PatientEntity>();
            responseEntity.listEntity = null;
            responseEntity.Entity = null;
            responseEntity.ErrorMsg = string.Empty;
            try
            {
                var param = new SqlParameter[]
                {
                    new SqlParameter()
                    {
                        ParameterName = "@PatientID",
                        SqlDbType = System.Data.SqlDbType.Decimal,
                        Direction = System.Data.ParameterDirection.Input,
                        Value =  PatientID
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@ErrorMgs",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Size = 500,
                        Direction = System.Data.ParameterDirection.Output,
                        Value = responseEntity.ErrorMsg  ?? (object) DBNull.Value
                    }
                };

                string query = "EXEC [dbo]." + SpName.GetPatients + " @PatientID,@ErrorMgs output";
                if (PatientID == 0)
                    responseEntity.listEntity = _context.Patients.FromSqlRaw(query, param.ToArray()).ToList();
                else
                {
                    var entity = _context.Patients.FromSqlRaw(query, param.ToArray()).ToList();
                    responseEntity.Entity = entity.SingleOrDefault();
                }
                responseEntity.ErrorMsg = Convert.ToString(param[1].Value);
                if (!string.IsNullOrEmpty(responseEntity.ErrorMsg))
                    responseEntity.Status = false;
                else
                    responseEntity.Status = true;
            }
            catch (Exception ex)
            {
                responseEntity.Status = false;
                responseEntity.ErrorMsg = ex.Message;
            }
            return responseEntity;
        }
        public ResponseEntity<PatientEntity> InsertPatient(PatientEntity patientEntity)
        {
            ResponseEntity<PatientEntity> responseEntity = new ResponseEntity<PatientEntity>();
            responseEntity.listEntity = null;
            responseEntity.Entity = null;
            try
            {
                var param = new SqlParameter[]
                 {
                    new SqlParameter()
                    {
                        ParameterName = "@PatientID",
                        SqlDbType = System.Data.SqlDbType.Decimal,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = patientEntity.PatientID
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@PatientName",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Size = 50,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = patientEntity.PatientName
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@Age",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Size = 50,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = patientEntity.Age
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@Gender",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Size = 10,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = patientEntity.Gender
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@Address",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Size = 50,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = patientEntity.Address
                    },
                   
                   
                    new SqlParameter()
                    {
                        ParameterName = "@CreatedBy",
                        SqlDbType = System.Data.SqlDbType.Decimal,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = patientEntity.CreatedBy
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@ErrorMgs",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Size = 500,
                        Direction = System.Data.ParameterDirection.Output,
                        Value = responseEntity.ErrorMsg
                    }

                };
                string SqlQuery = "EXEC [dbo]." + SpName.InsertPatient + " @PatientID,@PatientName,@Age,@Gender,@Address,@CreatedBy,@ErrorMgs output";
                var result = _context.Database.ExecuteSqlRaw(SqlQuery, param.ToArray());

                responseEntity.ErrorMsg = Convert.ToString(param[6].Value);

                if (responseEntity.ErrorMsg.ToUpper().Contains("SUCCESSFULLY") == true)
                {
                    responseEntity.Status = true;
                }
                else
                {
                    responseEntity.Status = false;
                }
            }
            catch (Exception ex)
            {
                responseEntity.ErrorMsg = ex.Message;
                responseEntity.Status = false;
            }
            return responseEntity;
        }
        public ResponseEntity<PatientEntity> DeletePatient(PatientEntity patientEntity)
        {
            ResponseEntity<PatientEntity> responseEntity = new ResponseEntity<PatientEntity>();
            responseEntity.listEntity = null;
            responseEntity.Entity = null;
            try
            {
                var param = new SqlParameter[]
                  {
                    new SqlParameter()
                    {
                        ParameterName = "@PatientID",
                        SqlDbType = System.Data.SqlDbType.Decimal,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = patientEntity.PatientID
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@DeletedBy",
                        SqlDbType = System.Data.SqlDbType.Decimal,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = patientEntity.DeletedBy
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@ErrorMgs",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Size = 500,
                        Direction = System.Data.ParameterDirection.Output,
                        Value = responseEntity.ErrorMsg  ?? (object) DBNull.Value
                    }
                  };

                string query = "EXEC [dbo]." + SpName.DeletePatient + " @PatientID,@DeletedBy,@ErrorMgs output";
                if (patientEntity.PatientID == 0)
                    responseEntity.listEntity = _context.Patients.FromSqlRaw(query, param.ToArray()).ToList();
                else
                {
                    var entity = _context.Patients.FromSqlRaw(query, param.ToArray()).ToList();
                    responseEntity.Entity = entity.SingleOrDefault();
                }
                responseEntity.ErrorMsg = Convert.ToString(param[2].Value);
                if (!string.IsNullOrEmpty(responseEntity.ErrorMsg))
                    responseEntity.Status = false;
                else
                    responseEntity.Status = true;
            }
            catch (Exception ex)
            {
                responseEntity.Status = false;
                responseEntity.ErrorMsg = ex.Message;
            }
            return responseEntity;
        }
        public ResponseEntity<PatientEntity> UpdatePatient(PatientEntity patientEntity)
        {
            ResponseEntity<PatientEntity> responseEntity = new ResponseEntity<PatientEntity>();
            responseEntity.listEntity = null;
            responseEntity.Entity = null;

            try
            {
                var param = new SqlParameter[]
                {
                    new SqlParameter()
                    {
                        ParameterName = "@PatientID",
                        SqlDbType = System.Data.SqlDbType.Decimal,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = patientEntity.PatientID
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@PatientName",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Size = 50,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = patientEntity.PatientName
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@Age",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Size = 50,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = patientEntity.Age
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@Gender",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Size = 10,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = patientEntity.Gender
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@Address",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Size = 50,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = patientEntity.Address
                    },

                    new SqlParameter()
                    {
                        ParameterName = "@UpdatedBy",
                        SqlDbType = System.Data.SqlDbType.Decimal,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = patientEntity.UpdatedBy
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@ErrorMgs",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Size = 500,
                        Direction = System.Data.ParameterDirection.Output,
                        Value = responseEntity.ErrorMsg
                    }

                };
                string query = "EXEC [dbo]." + SpName.UpdatePatient +"@PatientID,@PatientName,@Age,@Gender,@Address,@UpdatedBy,@ErrorMgs output";
                if (patientEntity.PatientID == 0)
                    responseEntity.listEntity = _context.Patients.FromSqlRaw(query, param.ToArray()).ToList();
                else
                {
                    var entity = _context.Patients.FromSqlRaw(query, param.ToArray()).ToList();
                    responseEntity.Entity = entity.SingleOrDefault();
                }
                responseEntity.ErrorMsg = Convert.ToString(param[6].Value);
                if (!string.IsNullOrEmpty(responseEntity.ErrorMsg))
                    responseEntity.Status = false;
                else
                    responseEntity.Status = true;
            }
            catch (Exception ex)
            {
                responseEntity.Status = false;
                responseEntity.ErrorMsg = ex.Message;
            }
            return responseEntity;
        }
    }
}
