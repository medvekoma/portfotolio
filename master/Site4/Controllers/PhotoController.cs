using System.Web.Mvc;
using Portfotolio.Domain;
using Portfotolio.Domain.Persistency;
using Portfotolio.Site.Services.Models;
using Portfotolio.Site4.Attributes;

namespace Portfotolio.Site4.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IPhotoEngine _photoEngine;

        public PhotoController(IPhotoEngine photoEngine)
        {
            _photoEngine = photoEngine;
        }

        [HideFromSearchEngines(AllowRobots.Follow)]
        public ActionResult Interestingness(int page = 0)
        {
            ViewData[DataKeys.BreadCrumb] = "explored on flickr";

            var photos = _photoEngine.GetInterestingPhotos(page);

            return PagingView(photos);
        }

        [UserIdentification]
        public ActionResult Photos(string id, int page = 0)
        {
            ViewData[DataKeys.BreadCrumb] = ViewData[DataKeys.UserName];
            var userId = (string)ViewData[DataKeys.UserId];
            var domainPhotos = _photoEngine.GetPhotosOf(userId, page);

            return PagingView(domainPhotos);
        }

        #region helpers

        private ActionResult PagingView(object model)
        {
            if (Request.IsAjaxRequest())
                return PartialView("Page", model);

            return View("PagingView", model);
        }

        #endregion
    }
}
