using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Data
{
    public static class JsonUtil
    {
        private static readonly JsonSerializerSettings Setting = new JsonSerializerSettings
        {
          
        };

        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, Setting);
        }

        public static object Deserialize(string json)
        {
            return JsonConvert.DeserializeObject(json, Setting);
        }

        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }

    }
}
