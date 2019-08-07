using Chatbot.Unit.Model;
using Chatbot.Unit.Services;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace Web.ApiSite
{
    public static class UnityConfig
    {
        public static void RegisterComponents(BaiduSession session)
        {
			var container = new UnityContainer();

            container.RegisterInstance(session, InstanceLifetime.Singleton);
            container.RegisterType<ChatService>();
            container.RegisterType<RobotService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}