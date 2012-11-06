using System.Web;
using System.Web.Mvc;
using Portfotolio.Domain;
using Portfotolio.Domain.Configuration;
using Portfotolio.Domain.Persistency;
using Portfotolio.FlickrEngine;
using Portfotolio.Services.Caching;
using Portfotolio.Services.Logging;
using Portfotolio.Site.Controllers;
using Portfotolio.Site.Services;
using Portfotolio.Utility.DependencyInjection;

namespace Portfotolio.Site.DependencyInjection
{
    public static class RegistrationModule
    {
        public static void RegisterComponents(this IDependencyEngine dependencyEngine)
        {
            // MVC
            dependencyEngine.Register<IControllerActivator, ControllerActivator>(DependencyLifeStyle.Singleton);

            // Application
            dependencyEngine.Register<IConfigurationProvider, AppSettingConfigurationProvider>(DependencyLifeStyle.Singleton);
            dependencyEngine.Register<IUserSession, AspNetUserSession>(DependencyLifeStyle.Singleton);
            dependencyEngine.Register<ICacheProvider, CacheProvider>(DependencyLifeStyle.Singleton);
            dependencyEngine.Register<ILoggerFactory, NLogLoggerFactory>(DependencyLifeStyle.Singleton);

            // Home
            dependencyEngine.Register<HomeController>(DependencyLifeStyle.Transient);

            // Authentication
            dependencyEngine.Register<AccountController>(DependencyLifeStyle.Transient);
            dependencyEngine.Register<IAuthenticationProvider, FlickrAuthenticationProvider>(DependencyLifeStyle.Singleton);
            dependencyEngine.Register<IOAuthProvider, FlickrOAuthProvider>(DependencyLifeStyle.Singleton);

            // Photos
            dependencyEngine.Register<PhotoController>(DependencyLifeStyle.Transient);
            dependencyEngine.Register<IPhotoEngine, FlickrPhotoEngine>(DependencyLifeStyle.Singleton);
            dependencyEngine.RegisterAndDecorate<IUserEngine, FlickrUserEngine, CachedUserEngine>(DependencyLifeStyle.Singleton);
            dependencyEngine.Register<IFlickrPhotoProvider, FlickrPhotoProvider>(DependencyLifeStyle.PerWebRequest);
            dependencyEngine.Register<IFlickrConverter, FlickrConverter>(DependencyLifeStyle.Singleton);

            // Legacy
            dependencyEngine.Register<LegacyController>(DependencyLifeStyle.Transient);

            // Opt-out
            dependencyEngine.Register<ConfigurationController>(DependencyLifeStyle.Transient);
            dependencyEngine.Register<IOptoutUserStorePathProvider, OptoutUserStorePathProvider>(DependencyLifeStyle.Singleton);
            dependencyEngine.Register<IOptoutUserStore, OptoutUserStore>(DependencyLifeStyle.Singleton);
            dependencyEngine.RegisterAndDecorate<IOptoutUserService, OptoutUserService, CachedOptoutUserService>(DependencyLifeStyle.Singleton);
            dependencyEngine.Register<IOptoutUserConfiguratorService, OptoutUserConfiguratorService>(DependencyLifeStyle.Singleton);
        }
    }
}