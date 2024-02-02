using System.Web.Http;
using GenericControllers.BusinessLogic;
using GenericControllers.Interface;
using GenericControllers.Models;
using Unity;
using Unity.WebApi;

namespace GenericControllers
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IServices<BOK01>, BLBook>();
            container.RegisterType<IServices<ALB01>, BLAlbum>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}