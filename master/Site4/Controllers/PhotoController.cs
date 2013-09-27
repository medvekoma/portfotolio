﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Portfotolio.Domain;
using Portfotolio.Domain.Exceptions;
using Portfotolio.Domain.Persistency;
using Portfotolio.FlickrEngine;
using Portfotolio.Site.Services.Models;
using Portfotolio.Site4.Attributes;

namespace Portfotolio.Site4.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IPhotoEngine _photoEngine;
        private readonly IFlickrStatisticsEngine _statisticsEngine;
        private readonly IFlickrExifEngine _exifEngine;

        public PhotoController(IPhotoEngine photoEngine, IFlickrStatisticsEngine statisticsEngine, IFlickrExifEngine exifEngine)
        {
            _photoEngine = photoEngine;
            _statisticsEngine = statisticsEngine;
            _exifEngine = exifEngine;
        }

        [HideFromSearchEngines(AllowRobots.Follow)]
        public ActionResult Interestingness(int page = 0)
        {
            ViewData[DataKeys.BreadCrumb] = "explored on flickr";

            var photos = _photoEngine.GetInterestingPhotos(page);

            return PagingView(photos);
        }

        [HideFromSearchEngines(AllowRobots.Follow)]
        public ActionResult Promotion(int page = 0)
        {
            var groupId = _photoEngine.GetPromotedGroupId();
            var group = _photoEngine.GetGroup(groupId, page);
            ViewData["groupName"] = group.GroupName;
            ViewData[DataKeys.BreadCrumb] = "Today's group: " + ViewData["groupName"];

            return PagingView(group);
        }

        [UserIdentification]
        [RedirectToUserAlias, RejectOptedOutUsers]
        [DisplayLicensingInfo]
        public ActionResult Photos(string id, int page = 0)
        {
            ViewData[DataKeys.BreadCrumb] = ViewData[DataKeys.UserName];
            var userId = (string)ViewData[DataKeys.UserId];
            var domainPhotos = _photoEngine.GetPhotosOf(userId, page);

            return PagingView(domainPhotos);
        }

        [RedirectToUserAlias, RejectOptedOutUsers]
        [DisplayLicensingInfo]
        public ActionResult BasicExifData(string photoId)
        {
            var exifDic = _exifEngine.ExtractBasicExifData(photoId);

            return PartialView("ExifList", exifDic);
        }

        [UserIdentification]
        [RedirectToUserAlias, RejectOptedOutUsers]
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
        [RedirectToUserAlias, RejectOptedOutUsers]
        [HideFromSearchEngines(AllowRobots.None)]
        public ActionResult Subscriptions(string id, int page = 0)
        {
            ViewData[DataKeys.BreadCrumb] = "subscription feed of " + ViewData[DataKeys.UserName];

            var userId = (string)ViewData[DataKeys.UserId];
            var domainPhotos = _photoEngine.GetSubscriptionsOf(userId, page);

            return PagingView(domainPhotos);
        }

        [UserIdentification]
        [RedirectToUserAlias, RejectOptedOutUsers]
        [HideFromSearchEngines(AllowRobots.None)]
        [OutputCache(Duration = 60, VaryByParam = "*")]
        public ActionResult Statistics(string id, string label)
        {
            if (label == null)
            {
                ViewData[DataKeys.BreadCrumb] = "Last 100 Photos - Statistics of " + ViewData[DataKeys.UserName];
                return View();
            }
            
            var userId = (string)ViewData[DataKeys.UserId];
            var statistic = _statisticsEngine.GetStatisticsOf(userId, label);
            //var statistic = new Statistic("test",
            //                              new List<KeyValuePair<string, int>>
            //                                  {
            //                                      new KeyValuePair<string, int>(String.Empty, 1),
            //                                      new KeyValuePair<string, int>("item2", 2)
            //                                  });
            return Json(statistic, JsonRequestBehavior.AllowGet);
        }

        [UserIdentification]
        [RedirectToUserAlias, RejectOptedOutUsers]
        [HideFromSearchEngines(AllowRobots.None)]
        public ActionResult Albums(string id)
        {
            ViewData[DataKeys.BreadCrumb] = "albums of " + ViewData[DataKeys.UserName];

            var userId = (string)ViewData[DataKeys.UserId];
            var albums = _photoEngine.GetAlbumsOf(userId);

            return View(albums);
        }

        [UserIdentification]
        [RedirectToUserAlias, RejectOptedOutUsers]
        [HideFromSearchEngines(AllowRobots.None)]
        [DisplayLicensingInfo]
        public ActionResult Album(string id, string secondaryId, int page = 0)
        {
            string albumId = secondaryId;

            var albumPhotos = _photoEngine.GetPhotosInAlbum(albumId, page);
            ViewData["albumTitle"] = albumPhotos.Title;

            ViewData[DataKeys.BreadCrumb] = ViewData["albumTitle"] + " by " + ViewData[DataKeys.UserName];

            return PagingView(albumPhotos.Photos);
        }

        [UserIdentification]
        [RedirectToUserAlias, RejectOptedOutUsers]
        [HideFromSearchEngines(AllowRobots.None)]
        public ActionResult Groups(string id)
        {
            ViewData[DataKeys.BreadCrumb] = "groups of " + ViewData[DataKeys.UserName];

            var userId = (string)ViewData[DataKeys.UserId];
            var domainGroups = _photoEngine.GetGroups(userId);

            return View(domainGroups);
        }

        [HideFromSearchEngines(AllowRobots.None)]
        public ActionResult Group(string id, int page = 0)
        {
            var domainGroup = _photoEngine.GetGroup(id, page);
            ViewData["groupName"] = domainGroup.GroupName;
            ViewData[DataKeys.BreadCrumb] = ViewData["groupName"] + " group";

            return PagingView(domainGroup);
        }

        [UserIdentification]
        [RedirectToUserAlias, RejectOptedOutUsers]
        [HideFromSearchEngines(AllowRobots.None)]
        public ActionResult GroupWithPhotos(string id, string groupId)
        {
            var domainGroup = _photoEngine.GetGroup(groupId, 0);

            return Json(domainGroup, JsonRequestBehavior.AllowGet);
        }

        [UserIdentification]
        [RedirectToUserAlias, RejectOptedOutUsers]
        [HideFromSearchEngines(AllowRobots.Follow)]
        public ActionResult Contacts(string id, int page = 0)
        {
            ViewData[DataKeys.BreadCrumb] = "contacts of " + ViewData[DataKeys.UserName];

            var userId = (string)ViewData[DataKeys.UserId];
            var contactList = _photoEngine.GetContacts(userId, page);

            return PagingView(contactList);
        }

        [UserIdentification]
        [RedirectToUserAlias, RejectOptedOutUsers]
        [HideFromSearchEngines(AllowRobots.None)]
        public ActionResult Recommendations(string id, int page = 0)
        {
            ViewData[DataKeys.BreadCrumb] = "recommendations for " + ViewData[DataKeys.UserName];

            var userId = (string)ViewData[DataKeys.UserId];
            var photos = _photoEngine.GetRecommendations(userId, page);

            return PagingView(photos);
        }

        protected override void HandleUnknownAction(string actionName)
        {
            throw new IncorrectUrlException();
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
