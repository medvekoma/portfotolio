using Microsoft.Extensions.DependencyInjection;
using Portfotolio.Domain;
using Portfotolio.Domain.Configuration;
using Portfotolio.Domain.Persistency;
using Portfotolio.FlickrEngine;
using Portfotolio.Services.Caching;
using Portfotolio.Services.Logging;
using Portfotolio.Site.Services;
using Portfotolio.Site4.Controllers;

namespace Portfotolio.Site4
{
    public static class DependencyEngineConfigurator
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            // application
            services.AddSingleton<IApplicationConfigurationProvider, ApplicationConfigurationProvider>();
            services.Decorate<IApplicationConfigurationProvider, CachedApplicationConfigurationProvider>();
            services.AddSingleton<IUserSession, AspNetUserSession>();
            services.AddSingleton<ICacheProvider, CacheProvider>();
            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton<IHttpContextProvider, HttpContextProvider>();
            services.AddSingleton<IAuthenticationStorage, FormsAuthenticationStorage>();

            // home
            services.AddTransient<HomeController>();

            // authentication
            services.AddTransient<AccountController>();
            services.AddTransient<IAuthenticationProvider, FlickrAuthenticationProvider>();

            // photo
            services.AddTransient<PhotoController>();
            services.AddTransient<IPhotoEngine, FlickrPhotoEngine>();
            services.AddTransient<IFlickrPhotoProvider, FlickrPhotoProvider>();
            services.AddTransient<IFlickrConverter, FlickrConverter>();
            services.AddTransient<IFlickrFactory, FlickrFactory>();
            services.AddTransient<IUserEngine, FlickrUserEngine>();
            services.Decorate<IUserEngine, CachedUserEngine>();

            // opt-out checker
            services.AddTransient<IUserService, UserService>();
            services.Decorate<IUserService, CachedUserService>();
            services.AddTransient<IUserReaderService, UserReaderService>();

            // legacy
            services.AddTransient<LegacyController>();

            // opt-out
            services.AddTransient<SettingsController>();
            services.AddTransient<IUserStorePathProvider, UserStorePathProvider>();
            services.AddTransient<IUserStore, UserStore>();
            services.AddTransient<IUserStoreService, UserStoreService>();
            services.AddTransient<IUserWriterService, UserWriterService>();

            // test
            services.AddTransient<TestController>();
        }
    }
}