using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.IRepository
{
    public interface IMethodService
    {
        bool ValidateControllerMaster(string UserId, string ControllerName, string ActionName);
    }
}
