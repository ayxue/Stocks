using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ValueProviders;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace Web.ApiSite.Utils
{
    public class JsonDotNetValueProviderFactory : System.Web.Mvc.ValueProviderFactory
    {
        public override System.Web.Mvc.IValueProvider GetValueProvider(ControllerContext context)
        {
            var content = string.Empty;
            using (var reader = new StreamReader(context.HttpContext.Request.InputStream))
            {
                content = reader.ReadToEnd();
            }

            if (string.IsNullOrEmpty(content))
                return null;
         
            var obj = JsonConvert.DeserializeObject<ExpandoObject>(content, new ExpandoObjectConverter()) as IDictionary<string, object>;
            return new System.Web.Mvc.DictionaryValueProvider<object>(obj, CultureInfo.CurrentCulture);
        }
    }




}