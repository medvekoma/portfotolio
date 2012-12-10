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

            const DependencyLifeStyle controllerLifeStyle = DependencyLifeStyle.Transient;
            const DependencyLifeStyle photoServiceLifeStyle = DependencyLifeStyle.Transient;
            const DependencyLifeStyle optOutServiceLifeStyle = DependencyLifeStyle.Transient;
            const DependencyLifeStyle authenticationServiceLifeStyle = DependencyLifeStyle.Transient;
            const DependencyLifeStyle applicationLifeStyle = DependencyLifeStyle.Singleton;

            // application
            dependencyEngine.Register<IConfigurationProvider, AppSettingConfigurationProvider>(applicationLifeStyle);
            dependencyEngine.Register<IUserSession, AspNetUserSession>(applicationLifeStyle);
            dependencyEngine.Register<ICacheProvider, CacheProvider>(applicationLifeStyle);
            dependencyEngine.Register<ILoggerFactory, NLogLoggerFactory>(applicationLifeStyle);

            // home
            dependencyEngine.Register<HomeController>(controllerLifeStyle);

            // authentication
            dependencyEngine.Register<AccountController>(controllerLifeStyle);
            dependencyEngine.Register<IAuthenticationProvider, FlickrAuthenticationProvider>(authenticationServiceLifeStyle);

            // photo
            dependencyEngine.Register<PhotoController>(controllerLifeStyle);
            dependencyEngine.Register<IPhotoEngine, FlickrPhotoEngine>(photoServiceLifeStyle);
            dependencyEngine.Register<IFlickrPhotoProvider, FlickrPhotoProvider>(photoServiceLifeStyle);
            dependencyEngine.Register<IFlickrConverter, FlickrConverter>(photoServiceLifeStyle);
            dependencyEngine.Register<IFlickrFactory, FlickrFactory>(photoServiceLifeStyle);
            dependencyEngine.RegisterAndDecorate<IUserEngine, FlickrUserEngine, CachedUserEngine>(photoServiceLifeStyle);

            // opt-out checker
            dependencyEngine.RegisterAndDecorate<IOptoutUserService, OptoutUserService, CachedOptoutUserService>(optOutServiceLifeStyle);

            // legacy
            dependencyEngine.Register<LegacyController>(controllerLifeStyle);

            // opt-out
            dependencyEngine.Register<SettingsController>(controllerLifeStyle);
            dependencyEngine.Register<IOptoutUserStorePathProvider, OptoutUserStorePathProvider>(optOutServiceLifeStyle);
            dependencyEngine.Register<IOptoutUserStore, OptoutUserStore>(optOutServiceLifeStyle);
            dependencyEngine.Register<IOptoutUserConfiguratorService, OptoutUserConfiguratorService>(optOutServiceLifeStyle);

            // test
            dependencyEngine.Register<TestController>(controllerLifeStyle);

            var dependencyResolver = new EnginedDependencyResolver(dependencyEngine);
            DependencyResolver.SetResolver(dependencyResolver);

            return dependencyEngine;
        }
    }
}