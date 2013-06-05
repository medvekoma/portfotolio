using System.Security.Policy;
using System.Web.Mvc;
using Portfotolio.Domain;
using Portfotolio.Domain.Persistency;

namespace Portfotolio.Site.Controllers
{
    public class _OAuthControllerPoc : Controller
    {
        private readonly IOAuthProvider _oAuthProvider;
        // _isOAuthEnabled = configurationProvider.GetIsOAuthEnabled();

        public _OAuthControllerPoc(IOAuthProvider oAuthProvider)
        {
            _oAuthProvider = oAuthProvider;
        }

        private ActionResult OAuthLogin()
        {
            string scheme = Request.Url != null
                                ? Request.Url.Scheme
                                : "http";
            string callbackUrl = Url.Action("Authorize", "Account", null, scheme);
            var authorizationObject = _oAuthProvider.GetAuthorizationObject(callbackUrl);

            TempData[DataKeys.OAuthTokenSecret] = authorizationObject.TokenSecret;
            return Redirect(authorizationObject.AuthorizationUrl);
        }

        
        private ActionResult OAuthLogout()
        {
            var authenticationInfo = _oAuthProvider.GetAuthenticationInfo()
            _oAuthProvider.Logout();
            _logger.Info(string.Format(UserHasLoggedOutMessage, authenticationInfo.UserName));
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
                _logger.Info(string.Format(UserHasLoggedInMessage, authenticationInfo.UserName));
            }
            return RedirectToLastPage();
        }
    }
}