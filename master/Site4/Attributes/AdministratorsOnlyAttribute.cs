using System.Security.Authentication;
using System.Web.Mvc;
using Portfotolio.Domain;
using Portfotolio.Site4.Extensions;
using System.Linq;

namespace Portfotolio.Site4.Attributes
{
    public class AdministratorsOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var authenticationInfo = filterContext.HttpContext.AuthenticationInfo();
            var authenticatedUserAlias = authenticationInfo.UserAlias;

            var configurationProvider = DependencyResolver.Current.GetService<IApplicationConfigurationProvider>();
            var administratorAliases = configurationProvider.GetApplicationConfiguration().AdministratorAliases;

            if (!administratorAliases.Contains(authenticatedUserAlias))
                throw new AuthenticationException();
        }
    }
}