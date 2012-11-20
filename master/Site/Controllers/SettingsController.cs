using System.Collections.Generic;
using System.Web.Mvc;
using Portfotolio.Domain;
using Portfotolio.Domain.Configuration;
using Portfotolio.Domain.Persistency;
using Portfotolio.Site.Helpers;
using Portfotolio.Site.Models;

namespace Portfotolio.Site.Controllers
{
    [HideFromSearchEngines(AllowRobots.None)]
    public class SettingsController : Controller
    {
        private readonly IOptoutUserService _optoutUserService;
        private readonly IOptoutUserConfiguratorService _optoutUserConfiguratorService;
        private readonly IAuthenticationProvider _authenticationProvider;

        public SettingsController(IOptoutUserService optoutUserService, IOptoutUserConfiguratorService optoutUserConfiguratorService, IAuthenticationProvider authenticationProvider)
        {
            _optoutUserService = optoutUserService;
            _optoutUserConfiguratorService = optoutUserConfiguratorService;
            _authenticationProvider = authenticationProvider;
        }

        [RememberActionUrl]
        public ActionResult Show()
        {
            ViewData[DataKeys.BreadCrumb] = "settings";

            var model = GetModel();
            if (!model.IsAuthenticated)
                return View("ShowUnauthenticated");

            return View(model);
        }

        [HttpPost]
        public ActionResult Show(bool isOptedOut)
        {
            var model = GetModel();
            if (!model.IsAuthenticated || model.IsOptedOut == isOptedOut)
                return View(model);

            if (isOptedOut)
                _optoutUserConfiguratorService.AddOptoutUser(model.UserId);
            else
                _optoutUserConfiguratorService.RemoveOptoutUser(model.UserId);

            return RedirectToAction("Show");
        }

        public ActionResult Get()
        {
            string optedoutUsersFlat = GetOptedoutUsersFlat();

            return Content(optedoutUsersFlat);
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

        #region helpers
        
        private ConfigurationModel GetModel()
        {
            var model = new ConfigurationModel();
            var authenticationInfo = _authenticationProvider.GetAuthenticationInfo();
            if (authenticationInfo.IsAuthenticated)
            {
                model.IsAuthenticated = true;
                model.UserName = authenticationInfo.UserName;
                model.UserId = authenticationInfo.UserId;
                var optedOutUserIds = _optoutUserService.GetOptedOutUserIds();
                model.IsOptedOut = optedOutUserIds.Contains(authenticationInfo.UserId);
            }
            return model;
        }

        private string GetOptedoutUsersFlat()
        {
            var optedOutUserIds = _optoutUserService.GetOptedOutUserIds() ?? new HashSet<string>();
            return string.Join(",", optedOutUserIds);
        }

        #endregion
    }
}
