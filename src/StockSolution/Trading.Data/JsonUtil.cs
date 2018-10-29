using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        };

        static JsonUtil()
        {
            Setting.Converters.Add(new IsoDateTimeConverter());
        }


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
