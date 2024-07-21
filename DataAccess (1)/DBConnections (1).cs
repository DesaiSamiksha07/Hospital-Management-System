using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DBConnections : ConnetionStringSettings
    {

        //[JsonPropertyName("name")]
        //public string Name { get; set; }
        //[JsonPropertyName("DBConnection")]
        //public string DBConnection { get; set; }

        public static List<DBConnections> FromJson(string json) => JsonConvert.DeserializeObject<List<DBConnections>>(json, Converter.settings);

    }


    public class ConnetionStringSettings
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("DBConnection")]
        public string DBConnection { get; set; }
    }

    public class APIStringSetting
    {
        [Key]
        public string APISettings { get; set; }
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter{DateTimeStyles = System.Globalization.DateTimeStyles.AssumeUniversal}
            },
        };

    }
}
