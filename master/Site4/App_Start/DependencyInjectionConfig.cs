using System.Web.Http;
using System.Web.Mvc;
using Portfotolio.DependencyInjection;
using Portfotolio.DependencyInjection.Unity;
using Portfotolio.Domain;
using Portfotolio.Domain.Configuration;
using Portfotolio.Domain.Persistency;
using Portfotolio.FlickrEngine;
using Portfotolio.Services.Caching;
using Portfotolio.Site.Services;
using Portfotolio.Site4.Controllers;
using Portfotolio.Site4.Mvc;

namespace Portfotolio.Site4
{
    public static class DependencyInjectionConfig
    {
         public static void Register(HttpConfiguration config)
         {
             IDependencyEngine dependencyEngine = new UnityDependencyEngine();

             dependencyEngine.Register<IConfigurationProvider, AppSettingConfigurationProvider>(DependencyLifeStyle.Singleton);
             dependencyEngine.Register<IUserSession, AspNetUserSession>(DependencyLifeStyle.Singleton);
             dependencyEngine.Register<ICacheProvider, CacheProvider>(DependencyLifeStyle.Singleton);

             dependencyEngine.Register<PhotoController>(DependencyLifeStyle.PerWebRequest);
             dependencyEngine.Register<IPhotoEngine, FlickrPhotoEngine>(DependencyLifeStyle.Singleton);
             dependencyEngine.Register<IFlickrPhotoProvider, FlickrPhotoProvider>(DependencyLifeStyle.Singleton);
             dependencyEngine.Register<IFlickrConverter, FlickrConverter>(DependencyLifeStyle.Singleton);
             dependencyEngine.Register<IFlickrFactory, FlickrFactory>(DependencyLifeStyle.Singleton);

             dependencyEngine.RegisterAndDecorate<IOptoutUserService, OptoutUserService, CachedOptoutUserService>(DependencyLifeStyle.Singleton);
             dependencyEngine.Register<IOptoutUserStorePathProvider, OptoutUserStorePathProvider>(DependencyLifeStyle.Singleton);
             dependencyEngine.Register<IOptoutUserStore, OptoutUserStore>(DependencyLifeStyle.Singleton);

             var dependencyResolver = new EnginedDependencyResolver(dependencyEngine);
             config.DependencyResolver = dependencyResolver;
             ControllerBuilder.Current.SetControllerFactory(new EnginedControllerFactory());
         }
    }
}