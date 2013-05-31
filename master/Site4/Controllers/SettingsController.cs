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

        public SettingsController(IUserReaderService userReaderService, IUserWriterService userWriterService, IAuthenticationProvider authenticationProvider, ILoggerFactory loggerFactory)
        {
            _userReaderService = userReaderService;
            _userWriterService = userWriterService;
            _authenticationProvider = authenticationProvider;
            _logger = loggerFactory.GetLogger("Settings");
        }

        public ActionResult Licensing()
        {
            ViewData[DataKeys.BreadCrumb] = "licensing information";

            var model = GetModel();
            //if (!model.IsAuthenticated)
            //    return View("AuthorInfo");

            return View(model);
        }

        [HttpPost]
        public ActionResult Licensing(UserState userState)
        {
            var model = GetModel();
            if (!model.IsAuthenticated || model.UserState == userState)
                return View(model);

            _userWriterService.Configure(model.UserId, userState);

            string message = string.Format("UserState of '{0}' is {1}.", model.UserAlias, userState);
            _logger.Info(message);

            return RedirectToAction("licensing");
        }

        public ActionResult Get()
        {
            return View();
        }

        public ActionResult In(string id)
        {
            _userWriterService.Configure(id, UserState.Optin);

            return RedirectToAction("Get");
        }

        public ActionResult Out(string id)
        {
            _userWriterService.Configure(id, UserState.Optout);

            return RedirectToAction("Get");
        }

        public ActionResult Default(string id)
        {
            _userWriterService.Configure(id, UserState.Default);

            return RedirectToAction("Get");
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
                model.UserState = _userReaderService.GetUserState(authenticationInfo.UserId);
            }
            return model;
        }

        #endregion
    }
}
