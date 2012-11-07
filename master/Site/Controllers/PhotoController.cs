using System.Diagnostics;
using System.Web.Mvc;
using Portfotolio.Domain;
using Portfotolio.Domain.Persistency;
using Portfotolio.Site.Helpers;
using Portfotolio.Site.Models;

namespace Portfotolio.Site.Controllers
{
    [OutputCache(Duration = 30)]
    public class PhotoController : Controller
    {
        private readonly IPhotoEngine _photoEngine;

        public PhotoController(IPhotoEngine photoEngine)
        {
            Debug.WriteLine("<<< Constructing PhotoController");
            _photoEngine = photoEngine;
        }

        public ActionResult Home()
        {
            var homeModel = new HomeModel("", SearchSource.People);
            return View(homeModel);
        }

        [HttpPost]
        public ActionResult Home(HomeModel homeModel)
        {
            return Content(homeModel.SearchText + " > " + homeModel.SearchSource);
        }

        [UserIdentification]
        [BreadCrumb("{userName}")]
        [HidePagesFromSearchEngines]
        public ActionResult Photos(string id, int page = 0)
        {
            var userId = (string) ViewData[DataKeys.UserId];
            if (page > 1)
            {
                ViewData[DataKeys.HideFromSearchEngines] = true;
            }
            var domainPhotos = _photoEngine.GetPhotosOf(userId, page);
            
            return PagingView(domainPhotos);
        }

        [UserIdentification]
        [HideFromSearchEngines]
        [BreadCrumb("favourites of {userName}")]
        public ActionResult Favorites(string id, int page = 0)
        {
            var userId = (string)ViewData[DataKeys.UserId];
            var domainPhotos = _photoEngine.GetFavoritesOf(userId, page);

            return PagingView(domainPhotos);
        }

        [UserIdentification]
        [HideFromSearchEngines]
        [BreadCrumb("subscription feed of {userName}")]
        public ActionResult Subscriptions(string id, int page = 0)
        {
            var userId = (string)ViewData[DataKeys.UserId];
            var domainPhotos = _photoEngine.GetSubscriptionsOf(userId, page);

            return PagingView(domainPhotos);
        }

        [UserIdentification]
        [BreadCrumb("albums of {userName}")]
        [HideFromSearchEngines]
        public ActionResult Albums(string id)
        {
            var userId = (string)ViewData[DataKeys.UserId];
            var albums = _photoEngine.GetAlbumsOf(userId);

            return View(albums);
        }

        [UserIdentification]
        [BreadCrumb("{albumTitle} by {userName}")]
        [HideFromSearchEngines]
        public ActionResult Album(string id, string secondaryId, int page = 0)
        {
            string albumId = secondaryId;

            var albumPhotos = _photoEngine.GetPhotosInAlbum(albumId, page);
            ViewData["albumTitle"] = albumPhotos.Title;
            return PagingView(albumPhotos.Photos);
        }

        [UserIdentification]
        [BreadCrumb("groups of {userName}")]
        [HideFromSearchEngines]
        public ActionResult Groups(string id)
        {
            var userId = (string)ViewData[DataKeys.UserId];
            var domainGroups = _photoEngine.GetGroups(userId);

            return View(domainGroups);
        }

        [BreadCrumb("{groupName} group")]
        [HideFromSearchEngines]
        public ActionResult Group(string id, int page = 0)
        {
            var domainGroup = _photoEngine.GetGroup(id, page);
            ViewData["groupName"] = domainGroup.GroupName;

            return PagingView(domainGroup);
        }

        [UserIdentification]
        [BreadCrumb("contacts of {userName}")]
        [HideFromSearchEngines]
        public ActionResult Contacts(string id, int page = 0)
        {
            var userId = (string)ViewData[DataKeys.UserId];
            var contactList = _photoEngine.GetContacts(userId, page);

            return PagingView(contactList);
        }

        [UserIdentification]
        [BreadCrumb("recommendations for {userName}")]
        [HideFromSearchEngines]
        public ActionResult Recommendations(string id, int page = 0)
        {
            var userId = (string)ViewData[DataKeys.UserId];
            var photos = _photoEngine.GetRecommendations(userId, page);

            return PagingView(photos);
        }

        [BreadCrumb("explored on flickr")]
        [HideFromSearchEngines]
        public ActionResult Interestingness(int page = 0)
        {
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

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (!Request.IsAjaxRequest())
            {
                TempData[DataKeys.ActionUrl] = filterContext.HttpContext.Request.RawUrl;
            }
        }

        #endregion
    }
}
