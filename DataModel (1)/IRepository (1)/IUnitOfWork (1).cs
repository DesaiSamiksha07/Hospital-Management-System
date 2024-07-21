using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.IRepository
{
    public interface IUnitOfWork
    {
        IDoctors_Info doctorsInfo { get; }
         IToken token { get; }
        IPatient patient { get; }
       
        void Save();
    }
}
