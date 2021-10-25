using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using BookService.Controllers;
using BookService.DbContext;
using BookService.DependencyInjection;
using Castle.Facilities.AspNet.WebApi;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace BookService
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }   
}