using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;
using Portfotolio.Domain;
using Portfotolio.Domain.Persistency;

namespace Portfotolio.Site.Controllers
{
    public class OAuthController : Controller
    {
        private static readonly Logger Logger = LogManager.GetLogger("OAuth");
        private const string UserHasLoggedOutMessage = "User '{0}' has logged out.";
        private const string UserHasLoggedInMessage = "User '{0}' has logged in.";

        private readonly IOAuthProvider _oAuthProvider;

        public OAuthController(IOAuthProvider oAuthProvider)
        {
            _oAuthProvider = oAuthProvider;
        }

        public ActionResult Login()
        {
            string callbackUrl = Url.Action("Authorize", "OAuth", null, Request.Url.Scheme);
            var authorizationObject = _oAuthProvider.GetAuthorizationObject(callbackUrl);

            TempData[DataKeys.OAuthTokenSecret] = authorizationObject.TokenSecret;
            return Redirect(authorizationObject.AuthorizationUrl);
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

        public ActionResult Logout()
        {
            var authenticationInfo = _oAuthProvider.GetAuthenticationInfo();
            Logger.Info(UserHasLoggedOutMessage, authenticationInfo.UserName);
            _oAuthProvider.Logout();
            return RedirectToLastPage();
        }

        private RedirectResult RedirectToLastPage()
        {
            var actionUrl = TempData[DataKeys.ActionUrl] as string;
            return Redirect(actionUrl);
        }
    }
}
