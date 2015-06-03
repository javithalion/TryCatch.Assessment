using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TryCatch.WebShopCase.DataAccess.NHibernate.Maps;
using TryCatch.WebShopCase.DataAccess.NHibernate.Repository.Implementations;
using TryCatch.WebShopCase.DataAccess.Xml.Repository.Implementations;
using TryCatch.WebShopCase.DataAccess.Xml.Repository.Interfaces;
using TryCatch.WebShopCase.Infraestructure.Repository;
using TryCatch.WebShopCase.Services.Implementations;
using TryCatch.WebShopCase.Services.Interfaces;
using TryCatch.WebShopCase.WebApi.App_Start;

namespace TryCatch.WebShopCase.WebApi.CastleWindsor
{
    public class CastleWindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            try
            {
                //NHibernate registration
                container.Register(Component
                    .For<ISessionFactory>()
                    .UsingFactoryMethod(CreateNhSessionFactory)
                    .LifeStyle
                    .Singleton);

                container.Register(Component
                    .For<ISession>()
                    .LifeStyle
                    .PerWebRequest
                    .UsingFactoryMethod(kernel => kernel.Resolve<ISessionFactory>()
                        .OpenSession()));
                
                //NHibernate repository registration
                container.Register(Component
                .For(typeof(ICrudRepository<,>))
                .ImplementedBy(typeof(BaseCrudRepository<,>))
                .LifeStyle
                .Transient);
                
                //XML Repository registration
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

                //Service registration
                container.Register(Component
                         .For(typeof(IProductService))
                         .ImplementedBy(typeof(ProductService))
                         .LifeStyle
                         .Transient);                

                container.Register(Component
                         .For(typeof(IOrderService))
                         .ImplementedBy(typeof(OrderService))
                         .LifeStyle
                         .Transient);

                //Controller registration
                container.Register(Classes.FromThisAssembly()
                .Pick().If(t => t.Name.EndsWith("Controller"))
                .Configure(configurer => configurer.Named(configurer.Implementation.Name))
                .LifestylePerWebRequest());
                

            }
            catch (Exception ex)
            {
                throw new Exception("Problem during the castle windsor configuration.", ex);
            }

        }

        private static ISessionFactory CreateNhSessionFactory()
        {
            try
            {
                return Fluently.Configure()
                   .Database(MsSqlConfiguration.MsSql2012.ConnectionString(c => c.FromConnectionStringWithKey("TryCatchConnection")))
                   .Mappings(m => m.FluentMappings.AddFromAssemblyOf<CustomerMap>())
                   .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(true, true)) //TODO :: Remove on production environment
                   .BuildSessionFactory();
            }
            catch (Exception ex)
            {
                throw new Exception("Problem creating NHibernate session factory.", ex);
            }
        }
    }
}