using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using Portfotolio.Domain;

namespace Portfotolio.Site4.Extensions
{
    using System;

    public static class AuthenticationInfoExtensions
    {
        private static readonly IApplicationConfigurationProvider _ApplicationConfigurationProvider = 
            DependencyResolver.Current.GetService<IApplicationConfigurationProvider>();
        
        private static bool IsInList(string userAlias, Func<ApplicationConfiguration, string[]> listProvider)
        {
            if (string.IsNullOrEmpty(userAlias)) 
                return false;
            
            var aliases = listProvider(_ApplicationConfigurationProvider.GetApplicationConfiguration());
            if (aliases.Length == 1 && aliases[0] == "*") 
                return true;

            return aliases.Contains(userAlias);
        }

        public static bool IsAdmin(this AuthenticationInfo authenticationInfo)
        {
            return IsInList(authenticationInfo.UserAlias, config => config.AdministratorAliases);
        }

        public static bool IsOmniViewer(this AuthenticationInfo authenticationInfo)
        {
            return IsInList(authenticationInfo.UserAlias, config => config.OmniViewerAliases);
        }

        public static bool IsAdmin(this IIdentity identity)
        {
            return IsInList(identity.Name, config => config.AdministratorAliases);
        }
    }
}