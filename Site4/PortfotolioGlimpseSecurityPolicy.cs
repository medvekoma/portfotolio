using System.Linq;
using System.Web.Mvc;
using Glimpse.AspNet.Extensions;
using Glimpse.Core.Extensibility;
using Portfotolio.Domain;
using Portfotolio.Site4.Extensions;

namespace Portfotolio.Site4
{
	public class PortfotolioGlimpseSecurityPolicy : IRuntimePolicy
	{
		public RuntimePolicy Execute(IRuntimePolicyContext policyContext)
		{
			var httpContext = policyContext.GetHttpContext();

			// More information about RuntimePolicies can be found at http://getglimpse.com/Help/Custom-Runtime-Policy
			if (!httpContext.User.Identity.IsAuthenticated)
				return RuntimePolicy.Off;

			if (!httpContext.User.Identity.IsAdmin())
				return RuntimePolicy.Off;

			return RuntimePolicy.On;
		}

		public RuntimeEvent ExecuteOn
		{
			get { return RuntimeEvent.EndRequest; }
		}
	}
}