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
    public class Token : Repository<TokenEntity>, IToken
    {
        private readonly ApplicationDbContext _context;
        public Token(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public bool CheckValidateToken(string AuthToken, out string ErrorMsg)
        {
            bool Flag = false;
            ErrorMsg = string.Empty;
            try
            {
                var param = new SqlParameter[]
                {
                    new SqlParameter()
                    {
                        ParameterName = "@AuthToken",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Size = 50,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = AuthToken ?? (object) DBNull.Value
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@result",
                        SqlDbType = System.Data.SqlDbType.Bit,
                        Direction = System.Data.ParameterDirection.Output,
                        Value = Flag
                    },new SqlParameter()
                    {
                        ParameterName = "@ErrorMgs",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Size = 500,
                        Direction = System.Data.ParameterDirection.Output,
                        Value = ErrorMsg  ?? (object) DBNull.Value
                    }
                };

                string query = "EXEC [dbo]." + SpName.GetValidateToken + " @AuthToken,@result output,@ErrorMgs output";
                var result = _context.Database.ExecuteSqlRaw(query, param.ToArray());
                Flag = Convert.ToBoolean( param[1].Value);
                ErrorMsg = Convert.ToString(param[2].Value);
            }
            catch (Exception ex)
            {
                Flag = false;
                ErrorMsg = ex.Message;
            }
            return Flag;
        }

        public ResponseEntity<TokenEntity> GetToken(string name = "")
        {
            
            ResponseEntity<TokenEntity> responseEntity = new ResponseEntity<TokenEntity>();
            responseEntity.listEntity = null;
            responseEntity.Entity = null;
            responseEntity.ErrorMsg = string.Empty;
            try
            {
                var param = new SqlParameter[]
                {
                    new SqlParameter()
                    {
                        ParameterName = "@Name",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Size = 50,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = name ?? (object) DBNull.Value
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

                string query = "EXEC [dbo]." + SpName.GetTokenMaster + " @Name,@ErrorMgs output";
                var result = _context.Tokens.FromSqlRaw(query, param.ToArray()).ToList();
                responseEntity.Entity = result.FirstOrDefault();
                responseEntity.Status = true;
                responseEntity.ErrorMsg = Convert.ToString(param[1].Value);
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
