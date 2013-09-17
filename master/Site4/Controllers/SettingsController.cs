using System.Threading;
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
        private readonly IUserReaderService _userReaderService;
        private readonly IUserWriterService _userWriterService;
        private readonly IAuthenticationProvider _authenticationProvider;
        private readonly ILogger _logger;

        public SettingsController(IUserReaderService userReaderService, IUserWriterService userWriterService, IAuthenticationProvider authenticationProvider, ILoggerProvider loggerProvider)
        {
            _userReaderService = userReaderService;
            _userWriterService = userWriterService;
            _authenticationProvider = authenticationProvider;
            _logger = loggerProvider.GetLogger("Settings");
        }

        public ActionResult Licensing()
        {
            ViewData[DataKeys.BreadCrumb] = "licensing information";

            var model = GetSettingsModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Licensing(UserState userState)
        {
            var model = GetSettingsModel();
            if (!model.IsInitialized || model.UserState == userState)
                return View(model);

            _userWriterService.Configure(model.UserId, userState);

            string message = string.Format("UserState of '{0}' is {1}.", model.UserAlias, userState);
            _logger.Info(message);

            return RedirectToAction("licensing");
        }

        [UserIdentification]
        [AdministratorsOnly]
        public ActionResult Admin(string id)
        {
            ViewData[DataKeys.BreadCrumb] = "external licensing information";

            var model = GetExternalSettingsModel();

            return View("licensing", model);
        }

        [HttpPost]
        [AdministratorsOnly]
        public ActionResult Admin(string userId, UserState userState)
        {
            _userWriterService.Configure(userId, userState);

            string message = string.Format("UserState of '{0}' is {1}.", userId, userState);
            _logger.Info(message);

            return RedirectToAction("admin");
        }

        #region helpers

        private SettingsModel GetSettingsModel()
        {
            var model = new SettingsModel();
            var authenticationInfo = _authenticationProvider.GetAuthenticationInfo();
            if (authenticationInfo.IsAuthenticated)
            {
                model.IsInitialized = true;
                model.UserAlias = authenticationInfo.UserAlias;
                model.UserId = authenticationInfo.UserId;
                model.UserName = authenticationInfo.UserName;
                model.UserState = _userReaderService.GetUserState(authenticationInfo.UserId);
            }
            return model;
        }

        private SettingsModel GetExternalSettingsModel()
        {
            var userId = ViewData[DataKeys.UserId] as string;
            var userAlias = ViewData[DataKeys.UserAlias] as string;
            var userName = ViewData[DataKeys.UserName] as string;
            return new SettingsModel
                {
                    IsInitialized = true,
                    UserId = userId,
                    UserAlias = userAlias,
                    UserName = userName,
                    UserState = _userReaderService.GetUserState(userId)
                };

        }

        #endregion
    }
}
