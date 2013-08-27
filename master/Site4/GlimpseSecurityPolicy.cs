//using System.Linq;
//using System.Web.Mvc;
//using Glimpse.AspNet.Extensions;
//using Glimpse.Core.Extensibility;
//using Portfotolio.Domain;
//using Portfotolio.Site4.Extensions;

//namespace Portfotolio.Site4
//{
//	public class GlimpseSecurityPolicy:IRuntimePolicy
//	{
//		public RuntimePolicy Execute(IRuntimePolicyContext policyContext)
//		{
//			var httpContext = policyContext.GetHttpContext();
//			var authenticationInfo = httpContext.AuthenticationInfo();
//			var authenticatedUserAlias = authenticationInfo.UserAlias;

//			var configurationProvider = DependencyResolver.Current.GetService<IApplicationConfigurationProvider>();
//			var administratorAliases = configurationProvider.GetApplicationConfiguration().AdministratorAliases;

//			// More information about RuntimePolicies can be found at http://getglimpse.com/Help/Custom-Runtime-Policy
//			if (!administratorAliases.Contains(authenticatedUserAlias))
//				return RuntimePolicy.Off;

//			return RuntimePolicy.On;
//		}

//		public RuntimeEvent ExecuteOn
//		{
//			get { return RuntimeEvent.EndRequest; }
//		}
//	}
//}
