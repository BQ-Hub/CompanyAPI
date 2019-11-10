using System.Web.Http;
using Unity;
using Unity.WebApi;
using CompanyAPISimple.Interfaces;
using CompanyAPISimple.Tests.Helpers;

namespace CompanyAPISimple.Tests
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();            

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);

            //add datahandler for use by controllers
            container.RegisterType<IDataHandler, TestDataHandler>();
        }
    }
}