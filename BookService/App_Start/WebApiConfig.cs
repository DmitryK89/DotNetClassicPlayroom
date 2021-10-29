using System.Linq;
using System.Web.Http;
using BookService.DependencyInjection;
using BookService.DependencyInjection.DependencyResolver;
using Swashbuckle.Application;

namespace BookService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ConfigureDependencyInjection(config);

            // Web API configuration and services
            
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );                      

            SetJsonByDefault(config);
            ConfigureSwagger(config);
        }

        private static void SetJsonByDefault(HttpConfiguration config)
        {
            var appXmlType =
                config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
        }

        private static void ConfigureDependencyInjection(HttpConfiguration config)
        {
            var container = ContainerFactory.Build();
            config.DependencyResolver = new UnityResolver(container);
        }

        private static void ConfigureSwagger(HttpConfiguration config)
        {
            config
                .EnableSwagger(c => c.SingleApiVersion("v1", "A title for your API"))
                .EnableSwaggerUi();
        }
    }
}
