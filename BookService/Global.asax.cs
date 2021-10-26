using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using BookService.DependencyInjection;

namespace BookService
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            //To Add Unity as HttpControllerActivator
            //ContainerConfig.Config();

            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }   
}