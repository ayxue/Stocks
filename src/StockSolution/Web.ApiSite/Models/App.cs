
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.ApiSite.Converters;

namespace Web.ApiSite.Models
{
    [TypeConverter(typeof(AppConverter))]
    public class App
    {
        public string Flow { get; set; }

        public string Secret { get; set; }
    }


    public class SecretConverter : JsonConverter<App>
    {
        public override bool CanWrite => true;

        public override App ReadJson(JsonReader reader, Type objectType, App existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, App value, JsonSerializer serializer)
        {
            try
            {
                writer.WriteStartObject();

                writer.WritePropertyName("Flow");
                writer.WriteValue(value.Flow);

                if (value.Flow.Equals("code", StringComparison.OrdinalIgnoreCase))
                {
                    writer.WritePropertyName("Code");
                    if (value.Secret == null)
                        writer.WriteValue(string.Empty);
                    else
                        writer.WriteValue(value.Secret);
                }

                writer.WriteEnd();
            }
            catch(Exception ex)
            {

            }
        }
    }
}
