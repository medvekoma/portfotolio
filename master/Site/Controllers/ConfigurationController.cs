using System.Collections.Generic;
using System.Security.Authentication;
using System.Web.Mvc;
using Portfotolio.Domain;
using Portfotolio.Domain.Configuration;
using Portfotolio.Site.Models;

namespace Portfotolio.Site.Controllers
{
    public class ConfigurationController : Controller
    {
        private readonly IOptoutUserService _optoutUserService;
        private readonly IOptoutUserConfiguratorService _optoutUserConfiguratorService;
        private readonly IAuthenticationProvider _authenticationProvider;

        public ConfigurationController(IOptoutUserService optoutUserService, IOptoutUserConfiguratorService optoutUserConfiguratorService, IAuthenticationProvider authenticationProvider)
        {
            _optoutUserService = optoutUserService;
            _optoutUserConfiguratorService = optoutUserConfiguratorService;
            _authenticationProvider = authenticationProvider;
        }

        public ActionResult Display()
        {
            return View();
        }

        public ActionResult OptOut()
        {
            var authenticationInfo = _authenticationProvider.GetAuthenticationInfo();
            if (!authenticationInfo.IsAuthenticated)
                return View("OutOutLoginView");

            var optedOutUserIds = _optoutUserService.GetOptedOutUserIds();
            var isOptedOut = optedOutUserIds.Contains(authenticationInfo.UserId);
            var optOutModel = new OptOutModel(authenticationInfo.UserName, isOptedOut);

            return View("OptOutChangeView", optOutModel);
        }

        [HttpPost]
        public ActionResult OptOut(bool isOptedOut)
        {
            var authenticationInfo = _authenticationProvider.GetAuthenticationInfo();
            if (!authenticationInfo.IsAuthenticated)
                throw new AuthenticationException("Not logged in!");

            var userId = authenticationInfo.UserId;
            if (isOptedOut)
                _optoutUserConfiguratorService.RemoveOptoutUser(userId);
            else
                _optoutUserConfiguratorService.AddOptoutUser(userId);

            isOptedOut = _optoutUserService.GetOptedOutUserIds().Contains(userId);
            var optOutModel = new OptOutModel(authenticationInfo.UserName, isOptedOut);

            return View("OptOutChangeView", optOutModel);
        }

        public ActionResult Get()
        {
            string optedoutUsersFlat = GetOptedoutUsersFlat();

            return Content(optedoutUsersFlat);
        }

        private string GetOptedoutUsersFlat()
        {
            var optedOutUserIds = _optoutUserService.GetOptedOutUserIds() ?? new SortedSet<string>();
            return string.Join(",", optedOutUserIds);
        }

        public ActionResult Add(string id)
        {
            _optoutUserConfiguratorService.AddOptoutUser(id);

            string optedoutUsersFlat = GetOptedoutUsersFlat();

            return Content(optedoutUsersFlat);
        }

        public ActionResult Remove(string id)
        {
            _optoutUserConfiguratorService.RemoveOptoutUser(id);

            string optedoutUsersFlat = GetOptedoutUsersFlat();

            return Content(optedoutUsersFlat);
        }
    }
}
