using Portfotolio.DependencyInjection;
using Portfotolio.Domain;
using Portfotolio.Domain.Configuration;
using Portfotolio.Domain.Persistency;
using Portfotolio.FlickrEngine;
using Portfotolio.Services.Caching;
using Portfotolio.Services.Logging;
using Portfotolio.Site.Controllers;
using Portfotolio.Site.Services;

namespace Portfotolio.Site.DependencyInjection
{
    public static class RegistrationModule
    {
        public static void RegisterComponents(this IDependencyEngine dependencyEngine)
        {
            // Application
            dependencyEngine.Register<IConfigurationProvider, AppSettingConfigurationProvider>(DependencyLifeStyle.Singleton);
            dependencyEngine.Register<IUserSession, AspNetUserSession>(DependencyLifeStyle.Singleton);
            dependencyEngine.Register<ICacheProvider, CacheProvider>(DependencyLifeStyle.Singleton);
            dependencyEngine.Register<ILoggerFactory, NLogLoggerFactory>(DependencyLifeStyle.Singleton);

            // Home
            dependencyEngine.Register<HomeController>(DependencyLifeStyle.PerWebRequest);

            // Authentication
            dependencyEngine.Register<AccountController>(DependencyLifeStyle.PerWebRequest);
            dependencyEngine.Register<IAuthenticationProvider, FlickrAuthenticationProvider>(DependencyLifeStyle.Singleton);

            // Photos
            dependencyEngine.Register<PhotoController>(DependencyLifeStyle.PerWebRequest);
            dependencyEngine.Register<IFlickrFactory, FlickrFactory>(DependencyLifeStyle.Singleton);
            dependencyEngine.Register<IPhotoEngine, FlickrPhotoEngine>(DependencyLifeStyle.Singleton);
            dependencyEngine.RegisterAndDecorate<IUserEngine, FlickrUserEngine, CachedUserEngine>(DependencyLifeStyle.Singleton);
            dependencyEngine.Register<IFlickrPhotoProvider, FlickrPhotoProvider>(DependencyLifeStyle.Singleton);
            dependencyEngine.Register<IFlickrConverter, FlickrConverter>(DependencyLifeStyle.Singleton);

            // Legacy
            dependencyEngine.Register<LegacyController>(DependencyLifeStyle.PerWebRequest);

            // Opt-out
            dependencyEngine.Register<SettingsController>(DependencyLifeStyle.PerWebRequest);
            dependencyEngine.Register<IOptoutUserStorePathProvider, OptoutUserStorePathProvider>(DependencyLifeStyle.Singleton);
            dependencyEngine.Register<IOptoutUserStore, OptoutUserStore>(DependencyLifeStyle.Singleton);
            dependencyEngine.RegisterAndDecorate<IOptoutUserService, OptoutUserService, CachedOptoutUserService>(DependencyLifeStyle.Singleton);
            dependencyEngine.Register<IOptoutUserConfiguratorService, OptoutUserConfiguratorService>(DependencyLifeStyle.Singleton);

            // Test
            dependencyEngine.Register<TestController>(DependencyLifeStyle.PerWebRequest);
        }
    }
}