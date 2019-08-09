using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Chatbot.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Json
            var xml = config.Formatters.FirstOrDefault(f => f is XmlMediaTypeFormatter);
            if (xml != null)
                config.Formatters.Remove(xml);

            // CORS
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
