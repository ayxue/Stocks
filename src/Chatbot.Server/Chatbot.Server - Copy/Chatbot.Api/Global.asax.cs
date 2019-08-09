using Chatbot.Unit.Model;
using Chatbot.Unit.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;

namespace Chatbot.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var isInternal = Convert.ToBoolean(ConfigurationManager.AppSettings["IsInternal"]);
            var session = LoginBaidu(isInternal);
            UnityConfig.RegisterComponents(session, isInternal);
        }

        public override void Init()
        {
            PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;
            base.Init();
        }

        void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
            HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
        }


        protected void Session_End()
        {

        }


        private BaiduSession LoginBaidu(bool isInternal)
        {
            var service = new AuthorizationService();
            var session = BaiduSession.CreateDefault(isInternal);

            service.GetAccessToken(session);
            return session;
        }
    }
}
