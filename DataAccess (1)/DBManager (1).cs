using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class DBManager
    {
        public static string ConnectDB;


        public static string GetConnectionString(string ConnectDB)
        {
            return DBConnectionManager.GetConnectionString(ConnectDB);
        }
    }
}
