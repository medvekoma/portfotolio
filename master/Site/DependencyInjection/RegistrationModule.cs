using System.Web;
using System.Web.Mvc;
using Portfotolio.Domain;
using Portfotolio.Domain.Persistency;
using Portfotolio.FlickrEngine;
using Portfotolio.Site.Controllers;
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
            dependencyEngine.Register<ICache, AspNetCache>(DependencyLifeStyle.Singleton);

            // Home
            dependencyEngine.Register<HomeController>(DependencyLifeStyle.PerWebRequest);

            // Authentication
            dependencyEngine.Register<AccountController>(DependencyLifeStyle.PerWebRequest);
            dependencyEngine.Register<IAuthenticationProvider, FlickrAuthenticationProvider>(DependencyLifeStyle.Singleton);

            // Photos
            dependencyEngine.Register<PhotoController>(DependencyLifeStyle.PerWebRequest);
            dependencyEngine.Register<IPhotoEngine, FlickrPhotoEngine>(DependencyLifeStyle.PerWebRequest);
            dependencyEngine.RegisterAndDecorate<IUserEngine, FlickrUserEngine, CachedUserEngine>(DependencyLifeStyle.PerWebRequest);
            dependencyEngine.Register<IFlickrPhotoProvider, FlickrPhotoProvider>(DependencyLifeStyle.PerWebRequest);
            dependencyEngine.Register<IFlickrConverter, FlickrConverter>(DependencyLifeStyle.PerWebRequest);

            // Legacy
            dependencyEngine.Register<LegacyController>(DependencyLifeStyle.PerWebRequest);
        }
    }
}