using CompanyAPISimple.Helpers;
using CompanyAPISimple.Interfaces;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace CompanyAPISimple
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

            container.RegisterType<IDataHandler, DataHandler>();            
        }
    }
}