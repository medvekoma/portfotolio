﻿using System.Web.Mvc;
using Portfotolio.DependencyInjection;
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
    public static class DependencyEngineConfigurator
    {
        public static IDependencyEngine Setup(IDependencyEngine dependencyEngine)
        {
            const DependencyLifeStyle applicationLifeStyle = DependencyLifeStyle.Singleton;
            const DependencyLifeStyle controllerLifeStyle = DependencyLifeStyle.Transient;
            const DependencyLifeStyle photoServiceLifeStyle = DependencyLifeStyle.Transient;
            const DependencyLifeStyle optOutServiceLifeStyle = DependencyLifeStyle.Transient;
            const DependencyLifeStyle authenticationServiceLifeStyle = DependencyLifeStyle.Transient;

            // application
            dependencyEngine.Register<IConfigurationProvider, AppSettingConfigurationProvider>(applicationLifeStyle);
            dependencyEngine.Register<IUserSession, AspNetUserSession>(applicationLifeStyle);
            dependencyEngine.Register<ICacheProvider, CacheProvider>(applicationLifeStyle);
            dependencyEngine.Register<ILoggerProvider, NLogLoggerProvider>(applicationLifeStyle);

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
            dependencyEngine.RegisterAndDecorate<IUserService, UserService, CachedUserService>(optOutServiceLifeStyle);
            dependencyEngine.Register<IUserReaderService, UserReaderService>(optOutServiceLifeStyle);

            // legacy
            dependencyEngine.Register<LegacyController>(controllerLifeStyle);

            // opt-out
            dependencyEngine.Register<SettingsController>(controllerLifeStyle);
            dependencyEngine.Register<IUserStorePathProvider, UserStorePathProvider>(optOutServiceLifeStyle);
            dependencyEngine.Register<IUserStore, UserStore>(optOutServiceLifeStyle);
            dependencyEngine.Register<IUserStoreService, UserStoreService>(optOutServiceLifeStyle);
            dependencyEngine.Register<IUserWriterService, UserWriterService>(optOutServiceLifeStyle);

            // test
            dependencyEngine.Register<TestController>(controllerLifeStyle);

            var dependencyResolver = new EnginedDependencyResolver(dependencyEngine);
            DependencyResolver.SetResolver(dependencyResolver);

            return dependencyEngine;
        }
    }
}