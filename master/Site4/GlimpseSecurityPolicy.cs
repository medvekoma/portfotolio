using System.Linq;
using System.Web.Mvc;
using Glimpse.AspNet.Extensions;
using Glimpse.Core.Extensibility;
using Portfotolio.Domain;

namespace Portfotolio.Site4
{
	public class GlimpseSecurityPolicy : IRuntimePolicy
	{
		public RuntimePolicy Execute(IRuntimePolicyContext policyContext)
		{
			var httpContext = policyContext.GetHttpContext();

			// More information about RuntimePolicies can be found at http://getglimpse.com/Help/Custom-Runtime-Policy
			if (!httpContext.User.Identity.IsAuthenticated)
				return RuntimePolicy.Off;

			var applicationConfigurationProvider = DependencyResolver.Current.GetService<IApplicationConfigurationProvider>();
			var administratorAliases = applicationConfigurationProvider.GetApplicationConfiguration().AdministratorAliases;
			if (!(administratorAliases.Contains(httpContext.User.Identity.Name)))
				return RuntimePolicy.Off;

			return RuntimePolicy.On;
		}

		public RuntimeEvent ExecuteOn
		{
			get { return RuntimeEvent.EndRequest; }
		}
	}
}
