using DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.IRepository
{
    public interface IToken : IRepository<TokenEntity>
    {
        public ResponseEntity<TokenEntity> GetToken(string name);

        public Boolean CheckValidateToken(string AuthToken, out string ErrorMsg);
    }
}
