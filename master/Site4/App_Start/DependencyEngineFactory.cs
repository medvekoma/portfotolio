using System.Web.Mvc;
using Portfotolio.DependencyInjection;
using Portfotolio.DependencyInjection.Unity;
using Portfotolio.Domain;
using Portfotolio.Domain.Configuration;
using Portfotolio.Domain.Persistency;
using Portfotolio.FlickrEngine;
using Portfotolio.Services.Caching;
using Portfotolio.Services.Logging;
using Portfotolio.Site.Services;
using Portfotolio.Site4.Controllers;
using Portfotolio.Site4.Mvc;

namespace Portfotolio.Site4
{
    public static class DependencyEngineFactory
    {
        public static IDependencyEngine Create()
        {
            IDependencyEngine dependencyEngine = new UnityDependencyEngine();

            const DependencyLifeStyle controllerLifeStyle = DependencyLifeStyle.PerWebRequest;

            // application
            dependencyEngine.Register<IConfigurationProvider, AppSettingConfigurationProvider>(DependencyLifeStyle.Singleton);
            dependencyEngine.Register<IUserSession, AspNetUserSession>(DependencyLifeStyle.Singleton);
            dependencyEngine.Register<ICacheProvider, CacheProvider>(DependencyLifeStyle.Singleton);
            dependencyEngine.Register<ILoggerFactory, NLogLoggerFactory>(DependencyLifeStyle.Singleton);

            // home
            dependencyEngine.Register<HomeController>(controllerLifeStyle);

            // authentication
            dependencyEngine.Register<AccountController>(controllerLifeStyle);
            dependencyEngine.Register<IAuthenticationProvider, FlickrAuthenticationProvider>(DependencyLifeStyle.Singleton);

            // photo
            dependencyEngine.Register<PhotoController>(controllerLifeStyle);
            dependencyEngine.Register<IPhotoEngine, FlickrPhotoEngine>(DependencyLifeStyle.Singleton);
            dependencyEngine.Register<IFlickrPhotoProvider, FlickrPhotoProvider>(DependencyLifeStyle.Singleton);
            dependencyEngine.Register<IFlickrConverter, FlickrConverter>(DependencyLifeStyle.Singleton);
            dependencyEngine.Register<IFlickrFactory, FlickrFactory>(DependencyLifeStyle.Singleton);
            dependencyEngine.RegisterAndDecorate<IUserEngine, FlickrUserEngine, CachedUserEngine>(DependencyLifeStyle.Singleton);

            // legacy
            dependencyEngine.Register<LegacyController>(controllerLifeStyle);

            // opt-out
            dependencyEngine.Register<SettingsController>(controllerLifeStyle);
            dependencyEngine.Register<IOptoutUserStorePathProvider, OptoutUserStorePathProvider>(DependencyLifeStyle.Singleton);
            dependencyEngine.Register<IOptoutUserStore, OptoutUserStore>(DependencyLifeStyle.Singleton);
            dependencyEngine.RegisterAndDecorate<IOptoutUserService, OptoutUserService, CachedOptoutUserService>(DependencyLifeStyle.Singleton);
            dependencyEngine.Register<IOptoutUserConfiguratorService, OptoutUserConfiguratorService>(DependencyLifeStyle.Singleton);

            // test
            dependencyEngine.Register<TestController>(controllerLifeStyle);

            var dependencyResolver = new EnginedDependencyResolver(dependencyEngine);
            DependencyResolver.SetResolver(dependencyResolver);

            return dependencyEngine;
        }
    }
}