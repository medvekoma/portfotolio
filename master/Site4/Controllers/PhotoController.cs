using System.Web.Mvc;
using Portfotolio.Domain;
using Portfotolio.Domain.Persistency;
using Portfotolio.Site.Services.Models;
using Portfotolio.Site4.Attributes;

namespace Portfotolio.Site4.Controllers
{
    [RememberActionUrl]
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

        [UserIdentification]
        [HideFromSearchEngines(AllowRobots.Follow)]
        // [BreadCrumb("favourites of {userName}")]
        public ActionResult Favorites(string id, int page = 0)
        {
            ViewData[DataKeys.BreadCrumb] = "favourites of " + ViewData[DataKeys.UserName];

            var userId = (string)ViewData[DataKeys.UserId];
            var domainPhotos = _photoEngine.GetFavoritesOf(userId, page);

            return PagingView(domainPhotos);
        }

        [UserIdentification]
        [HideFromSearchEngines(AllowRobots.Follow)]
        // [BreadCrumb("subscription feed of {userName}")]
        public ActionResult Subscriptions(string id, int page = 0)
        {
            ViewData[DataKeys.BreadCrumb] = "subscription feed of " + ViewData[DataKeys.UserName];

            var userId = (string)ViewData[DataKeys.UserId];
            var domainPhotos = _photoEngine.GetSubscriptionsOf(userId, page);

            return PagingView(domainPhotos);
        }

        [UserIdentification]
        // [BreadCrumb("albums of {userName}")]
        [HideFromSearchEngines(AllowRobots.Follow)]
        public ActionResult Albums(string id)
        {
            ViewData[DataKeys.BreadCrumb] = "albums of " + ViewData[DataKeys.UserName];

            var userId = (string)ViewData[DataKeys.UserId];
            var albums = _photoEngine.GetAlbumsOf(userId);

            return View(albums);
        }

        [UserIdentification]
        // [BreadCrumb("{albumTitle} by {userName}")]
        [HideFromSearchEngines(AllowRobots.Follow)]
        public ActionResult Album(string id, string secondaryId, int page = 0)
        {
            string albumId = secondaryId;

            var albumPhotos = _photoEngine.GetPhotosInAlbum(albumId, page);
            ViewData["albumTitle"] = albumPhotos.Title;

            ViewData[DataKeys.BreadCrumb] = ViewData["albumTitle"] + " by " + ViewData[DataKeys.UserName];

            return PagingView(albumPhotos.Photos);
        }

        [UserIdentification]
        // [BreadCrumb("groups of {userName}")]
        [HideFromSearchEngines(AllowRobots.Follow)]
        public ActionResult Groups(string id)
        {
            ViewData[DataKeys.BreadCrumb] = "groups of " + ViewData[DataKeys.UserName];

            var userId = (string)ViewData[DataKeys.UserId];
            var domainGroups = _photoEngine.GetGroups(userId);

            return View(domainGroups);
        }

        // [BreadCrumb("{groupName} group")]
        [HideFromSearchEngines(AllowRobots.Follow)]
        public ActionResult Group(string id, int page = 0)
        {
            var domainGroup = _photoEngine.GetGroup(id, page);
            ViewData["groupName"] = domainGroup.GroupName;
            ViewData[DataKeys.BreadCrumb] = ViewData["groupName"] + " group";

            return PagingView(domainGroup);
        }

        [UserIdentification]
        // [BreadCrumb("contacts of {userName}")]
        [HideFromSearchEngines(AllowRobots.Follow)]
        public ActionResult Contacts(string id, int page = 0)
        {
            ViewData[DataKeys.BreadCrumb] = "contacts of " + ViewData[DataKeys.UserName];

            var userId = (string)ViewData[DataKeys.UserId];
            var contactList = _photoEngine.GetContacts(userId, page);

            return PagingView(contactList);
        }

        [UserIdentification]
        // [BreadCrumb("recommendations for {userName}")]
        [HideFromSearchEngines(AllowRobots.None)]
        public ActionResult Recommendations(string id, int page = 0)
        {
            ViewData[DataKeys.BreadCrumb] = "recommendations for " + ViewData[DataKeys.UserName];

            var userId = (string)ViewData[DataKeys.UserId];
            var photos = _photoEngine.GetRecommendations(userId, page);

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
