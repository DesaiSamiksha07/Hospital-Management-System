using DataModel.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Repository
{
    public class MethodService : IMethodService
    {
        public bool ValidateControllerMaster(string UserId, string ControllerName, string ActionName)
        {
            //apply logic here ...count > 0 then true else false
            return true;
        }
    }
}
