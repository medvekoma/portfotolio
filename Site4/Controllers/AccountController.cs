using Microsoft.AspNetCore.Mvc;
using Portfotolio.Domain;
using Portfotolio.Domain.Persistency;
using Portfotolio.Services.Logging;

namespace Portfotolio.Site4.Controllers
{
    public class AccountController : ControllerBase
    {
        private const string UserHasLoggedOutMessage = "User '{0}' has logged out.";
        private const string UserHasLoggedInMessage = "User '{0}' has logged in.";

        private readonly IAuthenticationProvider _authenticationProvider;
        private readonly ILogger _logger;

        public AccountController(IAuthenticationProvider authenticationProvider, ILoggerFactory loggerFactory)
        {
            _authenticationProvider = authenticationProvider;
            _logger = loggerFactory.GetLogger("Authentication");
        }

        public IActionResult Login()
        {
            RememberReferrerUrl();

            var loginUrl = _authenticationProvider.GetLoginUrl();
            return Redirect(loginUrl);
        }

        private void RememberReferrerUrl()
        {
            var urlReferrer = Request.Headers["Referer"].ToString();
            TempData[DataKeys.ActionUrl] = urlReferrer;
        }

        public IActionResult Logout()
        {
            RememberReferrerUrl();

            var authenticationInfo = _authenticationProvider.GetAuthenticationInfo();
            _authenticationProvider.Logout();
            _logger.Info(string.Format(UserHasLoggedOutMessage, authenticationInfo.UserAlias));

            return RedirectToLastPage();
        }

        public IActionResult Authenticate(string frob)
        {
            var authenticationInfo = _authenticationProvider.Authenticate(frob);
            if (authenticationInfo.IsAuthenticated)
            {
                _logger.Info(string.Format(UserHasLoggedInMessage, authenticationInfo.UserAlias));
            }
            return RedirectToLastPage();
        }

        private IActionResult RedirectToLastPage()
        {
            var actionUrl = TempData[DataKeys.ActionUrl] as string;
            if (!string.IsNullOrEmpty(actionUrl))
                return Redirect(actionUrl);

            return RedirectToAction("interestingness", "photo");
        }
    }
}
