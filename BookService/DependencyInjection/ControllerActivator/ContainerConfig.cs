using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace BookService.DependencyInjection.ControllerActivator
{
    public static class ContainerConfig {
        public static void Config()
        {
            var container = ContainerFactory.Build();

            // Set resolver to MVC.
            // var controllerActivator = new UnityControllerActivator(container);
            // ControllerBuilder.Current.SetControllerFactory(new DefaultControllerFactory(controllerActivator));

            // Set resolver to WebApi.
            var httpControllerActivator = new UnityHttpControllerActivator(container);
            GlobalConfiguration.Configuration.Services.Replace(typeof (IHttpControllerActivator), httpControllerActivator);

            // Set resolver to SignalR.
            // var hubActivator = new UnityHubActivator(container);
            // GlobalHost.DependencyResolver.Register(typeof (IHubActivator), () => hubActivator);
        }
    }
}