using System.Security.Authentication;
using System.Web.Mvc;
using Portfotolio.Site4.Extensions;

namespace Portfotolio.Site4.Attributes
{
    public class AuthenticatedUserFilterAttribute : ActionFilterAttribute
    {
        private readonly string _userAlias;

        public AuthenticatedUserFilterAttribute(string userAlias)
        {
            _userAlias = userAlias;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var authenticationInfo = filterContext.HttpContext.AuthenticationInfo();

            if (!Equals(authenticationInfo.UserAlias, _userAlias))
                throw new AuthenticationException();
        }
    }
}