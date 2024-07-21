using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class DBConnectionManager
    {
        public static string GetConnectionString(string dbName)
        {
            return GetAllConnection().FirstOrDefault(c => c.Name.ToUpper() == dbName.ToUpper()).DBConnection;
        }


        public static List<DBConnections> GetAllConnection()
        {
            List<DBConnections> result;
            using (StreamReader sr = new StreamReader("App_Data/myMultipleConnection.json"))
            {
                string json = sr.ReadToEnd();
                result = DBConnections.FromJson(json);
            }
            return result;
        }
    }

    public class DataSettingManager
    {
        private const string _dataSettingsfilePath = "App_Data/myConnection.json";

        public virtual ConnetionStringSettings LoadSettings()
        {
            var text = File.ReadAllText(_dataSettingsfilePath);
            if (string.IsNullOrEmpty(text))
                return new ConnetionStringSettings();

            ConnetionStringSettings settings = JsonConvert.DeserializeObject<ConnetionStringSettings>(text);
            return settings;
        }
    }

    //public class DataSettingManager
    //{
    //    private const string _dataSettingsfilePath = "App_Data/myConnection.json";

    //    public virtual ConnetionStringSettings LoadSettings()
    //    {
    //        var text = File.ReadAllText(_dataSettingsfilePath);
    //        if (string.IsNullOrEmpty(text))
    //            return new ConnetionStringSettings();

    //        ConnetionStringSettings settings = JsonConvert.DeserializeObject<ConnetionStringSettings>(text);
    //        return settings;
    //    }
    //}
}
