using System.Web.Mvc;
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
        private readonly IOAuthProvider _oAuthProvider;
        private readonly bool _isOAuthEnabled;

        public AccountController(IAuthenticationProvider authenticationProvider, IOAuthProvider oAuthProvider, IConfigurationProvider configurationProvider)
        {
            _authenticationProvider = authenticationProvider;
            _oAuthProvider = oAuthProvider;
            _isOAuthEnabled = configurationProvider.GetIsOAuthEnabled();
        }

        public ActionResult Login()
        {
            if (!_isOAuthEnabled)
            {
                var loginUrl = _authenticationProvider.GetLoginUrl();
                return Redirect(loginUrl);
            }
            string scheme = Request.Url != null
                                ? Request.Url.Scheme
                                : "http";
            string callbackUrl = Url.Action("Authorize", "Account", null, scheme);
            var authorizationObject = _oAuthProvider.GetAuthorizationObject(callbackUrl);

            TempData[DataKeys.OAuthTokenSecret] = authorizationObject.TokenSecret;
            return Redirect(authorizationObject.AuthorizationUrl);
        }

        public ActionResult Logout()
        {
            var authenticationInfo = _isOAuthEnabled
                ? _oAuthProvider.GetAuthenticationInfo()
                : _authenticationProvider.GetAuthenticationInfo();
            if (_isOAuthEnabled)
                _oAuthProvider.Logout();
            else
                _authenticationProvider.Logout();
            Logger.Info(UserHasLoggedOutMessage, authenticationInfo.UserName);
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

        // ReSharper disable InconsistentNaming
        public ActionResult Authorize(string oauth_token, string oauth_verifier)
        // ReSharper restore InconsistentNaming
        {
            var oAuthTokenSecret = TempData[DataKeys.OAuthTokenSecret] as string;
            var authenticationInfo = _oAuthProvider.Authenticate(oauth_token, oAuthTokenSecret, oauth_verifier);
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
