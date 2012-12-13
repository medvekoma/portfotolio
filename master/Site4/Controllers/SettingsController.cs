using System.Collections.Generic;
using System.Web.Mvc;
using Portfotolio.Domain;
using Portfotolio.Domain.Configuration;
using Portfotolio.Domain.Persistency;
using Portfotolio.Services.Logging;
using Portfotolio.Site.Services.Models;
using Portfotolio.Site4.Attributes;

namespace Portfotolio.Site4.Controllers
{
    [HideFromSearchEngines(AllowRobots.None)]
    public class SettingsController : Controller
    {
        private readonly IOptoutUserService _optoutUserService;
        private readonly IOptoutUserConfiguratorService _optoutUserConfiguratorService;
        private readonly IAuthenticationProvider _authenticationProvider;
        private readonly ILogger _logger;

        public SettingsController(IOptoutUserService optoutUserService, IOptoutUserConfiguratorService optoutUserConfiguratorService, IAuthenticationProvider authenticationProvider, ILoggerFactory loggerFactory)
        {
            _optoutUserService = optoutUserService;
            _optoutUserConfiguratorService = optoutUserConfiguratorService;
            _authenticationProvider = authenticationProvider;
            _logger = loggerFactory.GetLogger("Settings");
        }

        // temporary action, can be deleted once it is not cached
        public ActionResult Show()
        {
            return RedirectToAction("optout");
        }

        public ActionResult OptOut()
        {
            ViewData[DataKeys.BreadCrumb] = "opt out";

            var model = GetModel();
            if (!model.IsAuthenticated)
                return View("OptOutInfo");

            return View(model);
        }

        [HttpPost]
        public ActionResult OptOut(bool isOptedOut)
        {
            var model = GetModel();
            if (!model.IsAuthenticated || model.IsOptedOut == isOptedOut)
                return View(model);

            if (isOptedOut)
                _optoutUserConfiguratorService.AddOptoutUser(model.UserId);
            else
                _optoutUserConfiguratorService.RemoveOptoutUser(model.UserId);

            string message = string.Format("{0} is opted {1}.", model.UserAlias, isOptedOut ? "out" : "in");
            _logger.Info(message);

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
                model.UserAlias = authenticationInfo.UserAlias;
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
