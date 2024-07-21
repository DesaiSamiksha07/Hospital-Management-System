using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DataAccess
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() {
            Database.SetCommandTimeout(9000);
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) {
            Database.SetCommandTimeout(9000);
        }

        //Run time need to another connection connectivity.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         {
              if ((DBManager.ConnectDB != null) && (!optionsBuilder.IsConfigured))
              {
                  var dbName = DBManager.ConnectDB;
                  var dbConnectionString = DBManager.GetConnectionString(dbName);

                  object connection = optionsBuilder.UseSqlServer(dbConnectionString);
              }
         }

        public virtual DbSet<DoctorEntity> Doctors { get; set; }
        public virtual DbSet<TokenEntity> Tokens { get; set; }
        public virtual DbSet<PatientEntity> Patients { get; set; }
       }
}
