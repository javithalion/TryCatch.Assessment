using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TryCatch.WebShopCase.DataAccess.Xml.Repository.Implementations;
using TryCatch.WebShopCase.DataAccess.Xml.Repository.Interfaces;
using TryCatch.WebShopCase.Services.Implementations;
using TryCatch.WebShopCase.Services.Interfaces;

namespace TryCatch.WebShopCase.WebApi.CastleWindsor
{
    public class CastleWindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            try
            {
                //Controller registration
                container.Register(Classes.FromThisAssembly()
                .Pick().If(t => t.Name.EndsWith("Controller"))
                .Configure(configurer => configurer.Named(configurer.Implementation.Name))
                .LifestylePerWebRequest());

                //Service registration
                container.Register(Component
                         .For(typeof(IProductService))
                         .ImplementedBy(typeof(ProductService))
                         .LifeStyle
                         .Transient);

                //Repository registration
                var productXmlFilePath = Path.Combine(
                    System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/"),
                    ConfigurationManager.AppSettings["System.DataAccess.Products.FileName"]
                    );

                container.Register(Component
                    .For(typeof(IProductRepository))
                    .ImplementedBy(typeof(ProductRepository))
                    .DependsOn(Dependency.OnValue("filePath", productXmlFilePath))
                    .LifeStyle
                    .Transient);

            }
            catch (Exception ex)
            {
                throw new Exception("Problem during the castle windsor configuration.", ex);
            }

        }
    }
}