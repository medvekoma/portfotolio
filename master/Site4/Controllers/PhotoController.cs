using System.Web.Mvc;
using Portfotolio.Domain;
using Portfotolio.Domain.Persistency;

namespace Portfotolio.Site4.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IPhotoEngine _photoEngine;

        public PhotoController(IPhotoEngine photoEngine)
        {
            _photoEngine = photoEngine;
        }

        // [HideFromSearchEngines(AllowRobots.Follow)]
        public ActionResult Interestingness(int page = 0)
        {
            ViewData[DataKeys.BreadCrumb] = "explored on flickr";

            var photos = _photoEngine.GetInterestingPhotos(page);

            return PagingView(photos);
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
