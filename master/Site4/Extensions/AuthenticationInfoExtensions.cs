using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using Portfotolio.Domain;

namespace Portfotolio.Site4.Extensions
{
	public static class AuthenticationInfoExtensions
	{
		private static bool IsAdmin(string userAlias)
		{
			var applicationConfigurationProvider = DependencyResolver.Current.GetService<IApplicationConfigurationProvider>();
			var administratorAliases = applicationConfigurationProvider.GetApplicationConfiguration().AdministratorAliases;

			return administratorAliases.Contains(userAlias);
		}

		public static bool IsAdmin(this AuthenticationInfo authenticationInfo)
		{
			return IsAdmin(authenticationInfo.UserAlias);
		}

		public static bool IsAdmin(this IIdentity identity)
		{
			return IsAdmin(identity.Name);
		}
	}
}