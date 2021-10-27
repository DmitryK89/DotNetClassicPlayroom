using System.Linq;
using System.Web.Http;
using BookService.DbContext;
using BookService.DependencyInjection;
using BookService.DependencyInjection.DependencyResolver;
using BookService.Models;
using Unity;
using Unity.Lifetime;

namespace BookService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Add unity container as IDependencyResolver implementation
            var container = ContainerFactory.Build();
            config.DependencyResolver = new UnityResolver(container);
                
            // Web API configuration and services
            
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );                      

            //Remove Xml to set Json by default
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
                config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
        }
    }
}
