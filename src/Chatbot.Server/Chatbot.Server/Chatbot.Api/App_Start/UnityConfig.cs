using Chatbot.Unit.Model;
using Chatbot.Unit.Services;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace Chatbot.Api
{
    public static class UnityConfig
    {
        public static void RegisterComponents(BaiduSession baiduSession)
        {
            var container = new UnityContainer();

            container.RegisterInstance(baiduSession, InstanceLifetime.Singleton);
            container.RegisterType<ChatService>();
            container.RegisterType<RobotService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}