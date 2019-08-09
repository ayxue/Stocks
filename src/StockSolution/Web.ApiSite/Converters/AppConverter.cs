using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using Web.ApiSite.Models;

namespace Web.ApiSite.Converters
{
    public class AppConverter: ConfigurationConverterBase
    {
        public override bool CanConvertTo(ITypeDescriptorContext ctx, Type type)
        {
            return true;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return JsonConvert.SerializeObject(value, Formatting.None, new SecretConverter());
        }
    }
}