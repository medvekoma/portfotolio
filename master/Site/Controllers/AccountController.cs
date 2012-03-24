using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using NLog;
using Portfotolio.Domain;
using Portfotolio.Domain.Persistency;

namespace Portfotolio.Site.Controllers
{
    public class AccountController : Controller
    {
        private static readonly Logger Logger = LogManager.GetLogger("Authentication");
        private const string UserHasLoggedOutMessage = "User '{0}' has logged out.";
        private const string UserHasLoggedInMessage = "User '{0}' has logged in.";

        private readonly IAuthenticationProvider _authenticationProvider;

        public AccountController(IAuthenticationProvider authenticationProvider)
        {
            _authenticationProvider = authenticationProvider;
        }

        public ActionResult Login()
        {
            string loginUrl = _authenticationProvider.GetLoginUrl();
            return Redirect(loginUrl);
        }

        public ActionResult Logout()
        {
            var authenticationInfo = _authenticationProvider.GetAuthenticationInfo();
            Logger.Info(UserHasLoggedOutMessage, authenticationInfo.UserName);
            _authenticationProvider.Logout();
            return RedirectToLastPage();
        }

        public ActionResult Authenticate(string frob)
        {
            var authenticationInfo = _authenticationProvider.Authenticate(frob);
            if (authenticationInfo.IsAuthenticated)
            {
                Logger.Info(UserHasLoggedInMessage, authenticationInfo.UserName);
            }
            return RedirectToLastPage();
        }

        private RedirectResult RedirectToLastPage()
        {
            var actionUrl = TempData[DataKeys.ActionUrl] as string;
            return Redirect(actionUrl);
        }
    }
}
