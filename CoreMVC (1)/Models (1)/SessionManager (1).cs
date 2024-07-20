﻿using Newtonsoft.Json;

namespace CoreMVC.Models
{
    public static class SessionManager
    {
        public static double? GetDouble(this ISession session,string key)
        {
            var data = session.Get(key);
            if(data == null)
            {
                return null;
            }
            return BitConverter.ToDouble(data);
        }

        public static void SetDouble(this ISession session, string key, double value)
        {
            session.Set(key,BitConverter.GetBytes(value));
        }

        public static bool? GetBoolean(this ISession session, string key)
        {
            var data = session.Get(key);
            if (data == null)
            {
                return null;
            }
            return BitConverter.ToBoolean(data,0);
        }

        public static void SetBoolean(this ISession session, string key, bool value)
        {
            session.Set(key, BitConverter.GetBytes(value));
        }

        public static T GetComplexData<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            if(data == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(data);
        }


        public static void SetComplexData(this ISession session,string key,object value)
        {
            session.SetString(key,JsonConvert.SerializeObject(value));
        }


    }
}
