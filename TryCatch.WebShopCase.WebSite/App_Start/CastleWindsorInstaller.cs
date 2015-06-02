using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TryCatch.WebShopCase.WebSite.App_Start.CastleWindsorInfraestructure;
using TryCatch.WebShopCase.WebSite.Services.Implementations;
using TryCatch.WebShopCase.WebSite.Services.Interfaces;

namespace TryCatch.WebShopCase.WebSite.App_Start
{
    public class CastleWindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //Service registration
            container.Register(Component
                     .For(typeof(ICheckoutConfirmationService))
                     .ImplementedBy(typeof(CheckoutConfirmationService))
                     .LifeStyle
                     .Transient);

            //Controller factory
            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);

            //Controller registration
            container.Register(Classes.FromThisAssembly()
                .Pick().If(t => t.Name.EndsWith("Controller"))
                .Configure(configurer => configurer.Named(configurer.Implementation.Name))
                .LifestylePerWebRequest());
        }
    }
}