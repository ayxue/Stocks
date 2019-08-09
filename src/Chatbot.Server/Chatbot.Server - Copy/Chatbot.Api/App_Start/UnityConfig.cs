using Chatbot.Api.Models;
using Chatbot.Api.Services;
using Chatbot.Unit.Model;
using Chatbot.Unit.Services;
using Crm.Dynamics.Models;
using Crm.Dynamics.Services;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace Chatbot.Api
{
    public static class UnityConfig
    {
        public static void RegisterComponents(BaiduSession baiduSession, bool isInternal)
        {
            var container = new UnityContainer();

            var chatSessions = new ChatLogCache();
            var crmSession = CrmSession.CreateDefault(isInternal);

            container.RegisterInstance(crmSession);
            container.RegisterInstance(baiduSession, InstanceLifetime.Singleton);
            container.RegisterInstance(chatSessions, InstanceLifetime.Singleton);
            container.RegisterType<ChatService>();
            container.RegisterType<RobotService>();
            container.RegisterType<TokenService>();
            container.RegisterType<ChatLogService>();
            container.RegisterType<LeadService>();            
            container.RegisterType<CrmService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}