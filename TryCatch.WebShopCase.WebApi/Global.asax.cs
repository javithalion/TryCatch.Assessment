using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TryCatch.WebShopCase.WebApi.App_Start.CastleWindsorInfraestructure;
using TryCatch.WebShopCase.WebApi.CastleWindsor;

namespace TryCatch.WebShopCase.WebApi
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        private IWindsorContainer _container;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);         

            //Castle windsor
            _container = new WindsorContainer()
            .Install(new CastleWindsorInstaller());

            //Castle windsor controller activator
            GlobalConfiguration.Configuration.Services.Replace(
            typeof(IHttpControllerActivator),
            new WindsorCompositionRoot(_container));
        }
    }
}