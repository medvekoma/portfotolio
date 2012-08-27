using System.Collections.Generic;
using System.Web.Mvc;
using Portfotolio.Domain.Configuration;

namespace Portfotolio.Site.Controllers
{
    public class ConfigurationController : Controller
    {
        private readonly IOptoutUserService _optoutUserService;
        private readonly IOptoutUserConfiguratorService _optoutUserConfiguratorService;

        public ConfigurationController(IOptoutUserService optoutUserService, IOptoutUserConfiguratorService optoutUserConfiguratorService)
        {
            _optoutUserService = optoutUserService;
            _optoutUserConfiguratorService = optoutUserConfiguratorService;
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
