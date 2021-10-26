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
            // Web API configuration and services
            ContainerFactory.Build(config);
            
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );                      
        }
    }
}
