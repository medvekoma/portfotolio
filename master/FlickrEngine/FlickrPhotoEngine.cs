using System;
using System.Collections.Generic;
using System.Linq;
using FlickrNet;
using Portfotolio.Domain;
using Portfotolio.Domain.Configuration;
using Portfotolio.Domain.Persistency;
using Portfotolio.Utility.Extensions;

namespace Portfotolio.FlickrEngine
{
    public class FlickrPhotoEngine : IPhotoEngine
    {
        private readonly IFlickrPhotoProvider _flickrPhotoProvider;
        private readonly IFlickrConverter _flickrConverter;
        private readonly IApplicationConfigurationProvider _applicationConfigurationProvider;
        private readonly IUserSession _userSession;
        private readonly IUserService _userService;

        public FlickrPhotoEngine(IFlickrPhotoProvider flickrPhotoProvider, IFlickrConverter flickrConverter,
                                 IApplicationConfigurationProvider applicationConfigurationProvider, IUserSession userSession, IUserService userService)
        {
            _flickrPhotoProvider = flickrPhotoProvider;
            _flickrConverter = flickrConverter;
            _applicationConfigurationProvider = applicationConfigurationProvider;
            _userSession = userSession;
            _userService = userService;
        }

        private DomainPhotos RemoveOptedOutUserPhotos(DomainPhotos domainPhotos)
        {
            var optedOutUserIds = _userService.GetOptoutUserIds();
            var filteredPhotos = domainPhotos.Photos
                .Where(photo => !optedOutUserIds.Contains(photo.AuthorId))
                .ToList();

            return new DomainPhotos(filteredPhotos, domainPhotos.Page, domainPhotos.Pages);
        }

        private bool IsAuthenticatedUser()
        {
            return _userSession.GetAuthenticationInfo().IsAuthenticated;
        }

        private bool IsAuthenticatedUser(string userId)
        {
            return IsAuthenticatedUser() && (_userSession.GetAuthenticationInfo().UserId == userId);
        }

        public DomainPhotos GetPhotosOf(string userId, int page)
        {
            var pageSize = _applicationConfigurationProvider.GetApplicationConfiguration().PhotoPageSize;

            var photoCollection = IsAuthenticatedUser()
                ? _flickrPhotoProvider.GetPhotosOf(userId, page, pageSize)
                : _flickrPhotoProvider.GetPublicPhotosOf(userId, page, pageSize);

            var domainPhotos = _flickrConverter.Convert(photoCollection);
            return RemoveOptedOutUserPhotos(domainPhotos);
        }

        public DomainPhotos GetFavoritesOf(string userId, int page)
        {
            var pageSize = _applicationConfigurationProvider.GetApplicationConfiguration().PhotoPageSize;

            var photoCollection = GetAuthenticationAwareFavoritesOf(userId, page, pageSize);

            var domainPhotos = _flickrConverter.Convert(photoCollection);
            return RemoveOptedOutUserPhotos(domainPhotos);
        }

        private PhotoCollection GetAuthenticationAwareFavoritesOf(string userId, int page, int pageSize)
        {
            var photoCollection = IsAuthenticatedUser()
                                      ? _flickrPhotoProvider.GetFavoritesOf(userId, page, pageSize)
                                      : _flickrPhotoProvider.GetPublicFavoritesOf(userId, page, pageSize);
            return photoCollection;
        }

        public DomainPhotos GetSubscriptionsOf(string userId, int page)
        {
            var pageSize = _applicationConfigurationProvider.GetApplicationConfiguration().PhotoPageSize;

            var isUserAuthenticated = IsAuthenticatedUser(userId);
            var photoCollection = isUserAuthenticated
                ? _flickrPhotoProvider.GetPrivateSubscriptionsOf(userId, page, pageSize)
                : _flickrPhotoProvider.GetSubscriptionsOf(userId, page, pageSize);
            if (!isUserAuthenticated)
            {
                photoCollection.Pages = 1;
            }

            var domainPhotos = _flickrConverter.Convert(photoCollection);
            return RemoveOptedOutUserPhotos(domainPhotos);
        }

        public Albums GetAlbumsOf(string userId)
        {
            PhotosetCollection photosets = _flickrPhotoProvider.GetAlbumsOf(userId);

            return _flickrConverter.Convert(photosets);
        }

        public Album GetAlbum(string albumId)
        {
            var albumInfo = _flickrPhotoProvider.GetAlbumInfo(albumId);
            return _flickrConverter.Convert(albumInfo);
        }

        public AlbumPhotos GetPhotosInAlbum(string albumId, int page)
        {
            var pageSize = _applicationConfigurationProvider.GetApplicationConfiguration().PhotoPageSize;

            var photosets = _flickrPhotoProvider.GetPhotosInAlbum(albumId, page, pageSize);
            var domainPhotos = _flickrConverter.Convert(photosets);

            var albumInfo = _flickrPhotoProvider.GetAlbumInfo(albumId);

            return new AlbumPhotos(albumInfo.Title, albumInfo.Description, albumInfo.OwnerId, domainPhotos);
        }

        public Photoset GetAlbumInfo(string albumId)
        {
            return _flickrPhotoProvider.GetAlbumInfo(albumId);
        }

        public ListItems GetGroups(string userId)
        {
            var groups = _flickrPhotoProvider.GetGroups(userId);
            return _flickrConverter.Convert(groups);
        }

        public DomainGroup GetGroup(string groupId, int page)
        {
            var pageSize = _applicationConfigurationProvider.GetApplicationConfiguration().PhotoPageSize;

            var groupInfo = _flickrPhotoProvider.GetGroupInfo(groupId);
            var photos = _flickrPhotoProvider.GetPhotosInGroup(groupId, page, pageSize);
            var domainPhotos = _flickrConverter.Convert(photos);
            domainPhotos = RemoveOptedOutUserPhotos(domainPhotos);

            return new DomainGroup(groupInfo.GroupName, domainPhotos);
        }

        public ListItems GetContacts(string userId, int page)
        {
            var pageSize = _applicationConfigurationProvider.GetApplicationConfiguration().ContactsPageSize;

            var contacts = _flickrPhotoProvider.GetContacts(userId, page, pageSize);
            var listElements = _flickrConverter.Convert(contacts);

            return listElements;
        }

        private string[] GetRecommendedUserIds(string userId)
        {
            var pageSize = _applicationConfigurationProvider.GetApplicationConfiguration().PhotoPageSize;
            var favoritePhotos = GetAuthenticationAwareFavoritesOf(userId, 1, pageSize);
            var recommendedUserIds = favoritePhotos
                .Select(photo => photo.UserId)
                .Distinct()
                .Where(authorId => authorId != userId)
                .Randomize()
                .ToArray();

            if (!recommendedUserIds.Any())
            {
                var photos = _flickrPhotoProvider.GetInterestingPhotos(1, pageSize);
                recommendedUserIds = photos
                .Select(photo => photo.UserId)
                .Distinct()
                .Where(authorId => authorId != userId)
                .Randomize()
                .ToArray();
            }

            return recommendedUserIds;
        }

        public DomainPhotos GetRecommendations(string userId, int page)
        {
            if (page == 0) page = 1;

            string[] recommendedUserIds = null;
            if (page != 1)
                recommendedUserIds = _userSession.GetRecommendedUserIds(userId);

            if (recommendedUserIds == null)
            {
                recommendedUserIds = GetRecommendedUserIds(userId);
                _userSession.SetRecommendedUserIds(userId, recommendedUserIds);
            }

            if (page > recommendedUserIds.Length)
                return new DomainPhotos(new List<DomainPhoto>(), page, page);

            var pageSize = _applicationConfigurationProvider.GetApplicationConfiguration().PhotoPageSize;
            var photos = GetAuthenticationAwareFavoritesOf(recommendedUserIds[page - 1], 1, pageSize);
            var domainPhotos = _flickrConverter.Convert(photos).Photos
                .Where(photo => photo.AuthorId != userId)
                .ToList();
            var recommendations = domainPhotos.Count > 0 
                ? new DomainPhotos(domainPhotos, page, recommendedUserIds.Length) 
                : GetRecommendations(userId, page + 1);
            return RemoveOptedOutUserPhotos(recommendations);
        }

        public DomainPhotos GetInterestingPhotos(int page)
        {
            var pageSize = _applicationConfigurationProvider.GetApplicationConfiguration().PhotoPageSize;
            var photos = _flickrPhotoProvider.GetInterestingPhotos(page, pageSize);
            var domainPhotos = _flickrConverter.Convert(photos);
            return RemoveOptedOutUserPhotos(domainPhotos);
        }

        private static readonly string[] PromotedGroupIds = new[]
            {
                "986821@N21", "778206@N20", "75016977@N00", 
                "1115578@N22", "884850@N25", "96366707@N00", 
                "1102959@N22", "978551@N22",
                // "1498626@N22"
            };

        public string GetPromotedGroupId()
        {
            var index = DateTime.UtcNow.DayOfYear % PromotedGroupIds.Length;

            return PromotedGroupIds[index];
        }
    }
}
