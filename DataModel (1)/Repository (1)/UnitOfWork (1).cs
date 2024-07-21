using DataAccess;
using DataModel.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            doctorsInfo = new DoctorsInfo(_context);
            token = new Token(_context);
           
            patient = new Patient(_context);
           
        }

        public IDoctors_Info doctorsInfo { get; private set; }
        public IToken token { get; private set; }
      
        public IPatient patient { get; private set; }
       

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
