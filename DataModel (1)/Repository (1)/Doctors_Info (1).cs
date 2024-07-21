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

namespace DataModel.Repository
{
    public class DoctorsInfo : Repository<DoctorEntity>, IDoctors_Info
    {
        private readonly ApplicationDbContext _context;
        public DoctorsInfo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public ResponseEntity<DoctorEntity> GetAllDoctors(decimal DoctorID = 0)
        {
            ResponseEntity<DoctorEntity> responseEntity = new ResponseEntity<DoctorEntity>();
            responseEntity.listEntity = null;
            responseEntity.Entity = null;
            responseEntity.ErrorMsg = string.Empty;
            try
            {
                var param = new SqlParameter[]
                {
                    new SqlParameter()
                    {
                        ParameterName = "@Doctor_ID",
                        SqlDbType = System.Data.SqlDbType.Decimal,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = DoctorID
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

                string query = "EXEC [dbo]." + SpName.GetDoctors + " @Doctor_ID,@ErrorMgs output";
                if (DoctorID == 0)
                    responseEntity.listEntity = _context.Doctors.FromSqlRaw(query, param.ToArray()).ToList();
                else
                {
                    var entity = _context.Doctors.FromSqlRaw(query, param.ToArray()).ToList();
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

        public ResponseEntity<DoctorEntity>InsertDoctor(DoctorEntity doctorEntity)
        {
            ResponseEntity<DoctorEntity> responseEntity = new ResponseEntity<DoctorEntity>();
            responseEntity.listEntity = null;
            responseEntity.Entity = null;
            try
            {

                var param = new SqlParameter[]
                {
                    new SqlParameter()
                    {
                        ParameterName = "@Doctor_ID",
                        SqlDbType = System.Data.SqlDbType.Decimal,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = doctorEntity.DoctorID
                    },

                    new SqlParameter()
                    {
                        ParameterName = "@Doctor_Name",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Size = 50,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = doctorEntity.DoctorName
                    },

                    new SqlParameter()
                    {
                        ParameterName = "@Doctor_Specialization",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Size = 50,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = doctorEntity.Specialization
                    },

                     new SqlParameter()
                    {
                        ParameterName = "@Doctor_Contact",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Size = 12,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = doctorEntity.Contact
                    },

                     new SqlParameter()
                    {
                        ParameterName = "@CreatedBy",
                        SqlDbType = System.Data.SqlDbType.Decimal,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = doctorEntity.CreatedBy
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
                string SqlQuery = "EXEC [dbo]." + SpName.InsertDoctor + " @Doctor_ID,@Doctor_Name,@Doctor_Specialization,@Doctor_Contact,@CreatedBy,@ErrorMgs output";
                var result = _context.Database.ExecuteSqlRaw(SqlQuery, param.ToArray());

                responseEntity.ErrorMsg = Convert.ToString(param[5].Value);

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

        public ResponseEntity<DoctorEntity> DeleteDoctor(DoctorEntity doctorEntity)
        {
            ResponseEntity<DoctorEntity> responseEntity = new ResponseEntity<DoctorEntity>();
            responseEntity.listEntity = null;
            responseEntity.Entity = null;
            try
            {
                var param = new SqlParameter[]
                  {
                    new SqlParameter()
                    {
                        ParameterName = "@Doctor_ID",
                        SqlDbType = System.Data.SqlDbType.Decimal,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = doctorEntity.DoctorID
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@DeletedBy",
                        SqlDbType = System.Data.SqlDbType.Decimal,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = doctorEntity.DeletedBy
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

                string SqlQuery = "EXEC [dbo]." + SpName.DeleteDoctor + " @Doctor_ID,@DeletedBy,@ErrorMgs output";
                var result = _context.Database.ExecuteSqlRaw(SqlQuery, param.ToArray());

                responseEntity.ErrorMsg = Convert.ToString(param[2].Value);

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
        public ResponseEntity<DoctorEntity> UpdateDoctor(DoctorEntity doctorEntity)
        {
            ResponseEntity<DoctorEntity> responseEntity = new ResponseEntity<DoctorEntity>();
            responseEntity.listEntity = null;
            responseEntity.Entity = null;

            try
            {
                var param = new SqlParameter[]
                {

                      new SqlParameter()
                    {
                        ParameterName = "@Doctor_ID",
                        SqlDbType = System.Data.SqlDbType.Decimal,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = doctorEntity.DoctorID
                    },


                       new SqlParameter()
                    {
                        ParameterName = "@Doctor_Name",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Size = 50,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = doctorEntity.DoctorName
                    },

                        new SqlParameter()
                    {
                        ParameterName = "@Doctor_Specialization",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Size = 50,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = doctorEntity.Specialization
                    },


                          new SqlParameter()
                    {
                        ParameterName = "@Doctor_Contact",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Size = 12,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = doctorEntity.Contact
                    },


                    new SqlParameter()
                    {
                        ParameterName = "@UpdatedBy",
                        SqlDbType = System.Data.SqlDbType.Decimal,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = doctorEntity.UpdatedBy
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

                string query = "EXEC [dbo]." + SpName.UpdateDoctor + " @Doctor_ID,@Doctor_Name,@Doctor_Specialization,@Doctor_Contact,@UpdatedBy,@ErrorMgs output";

                _context.Database.ExecuteSqlRaw(query, param);

                responseEntity.Entity = doctorEntity;
                responseEntity.ErrorMsg = Convert.ToString(param[5].Value); // Correct index for @ErrorMgs
                responseEntity.Status = string.IsNullOrEmpty(responseEntity.ErrorMsg);
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

        
   
