using System.Collections.Generic;
using System.Linq;
using FlickrNet;
using Portfotolio.Domain;
using Portfotolio.Domain.Persistency;
using Portfotolio.Utility;

namespace Portfotolio.FlickrEngine
{
    public class FlickrPhotoEngine : IPhotoEngine
    {
        private readonly IFlickrPhotoProvider _flickrPhotoProvider;
        private readonly IFlickrConverter _flickrConverter;
        private readonly IConfigurationProvider _configurationProvider;
        private readonly IUserSession _userSession;
        private readonly string[] _optedOutUserIds;

        public FlickrPhotoEngine(IFlickrPhotoProvider flickrPhotoProvider, IFlickrConverter flickrConverter,
                                 IConfigurationProvider configurationProvider, IUserSession userSession)
        {
            _flickrPhotoProvider = flickrPhotoProvider;
            _flickrConverter = flickrConverter;
            _configurationProvider = configurationProvider;
            _userSession = userSession;

            _optedOutUserIds = _configurationProvider.GetOptedOutUserIds();
        }

        private DomainPhotos RemoveOptedOutUserPhotos(DomainPhotos domainPhotos)
        {
            var filteredPhotos = domainPhotos.Photos
                .Where(photo => !_optedOutUserIds.Contains(photo.AuthorId))
                .ToList();

            return new DomainPhotos(filteredPhotos, domainPhotos.Page, domainPhotos.Pages);
        }

        public DomainPhotos GetPhotosOf(string userId, int page)
        {
            var pageSize = _configurationProvider.GetPhotoPageSize();

            var photoCollection = _flickrPhotoProvider.IsAuthenticatedUser()
                ? _flickrPhotoProvider.GetPhotosOf(userId, page, pageSize)
                : _flickrPhotoProvider.GetPublicPhotosOf(userId, page, pageSize);

            var domainPhotos = _flickrConverter.Convert(photoCollection);
            return RemoveOptedOutUserPhotos(domainPhotos);
        }

        public DomainPhotos GetFavoritesOf(string userId, int page)
        {
            var pageSize = _configurationProvider.GetPhotoPageSize();

            var photoCollection = GetAuthenticationAwareFavoritesOf(userId, page, pageSize);

            var domainPhotos = _flickrConverter.Convert(photoCollection);
            return RemoveOptedOutUserPhotos(domainPhotos);
        }

        private PhotoCollection GetAuthenticationAwareFavoritesOf(string userId, int page, int pageSize)
        {
            var photoCollection = _flickrPhotoProvider.IsAuthenticatedUser()
                                      ? _flickrPhotoProvider.GetFavoritesOf(userId, page, pageSize)
                                      : _flickrPhotoProvider.GetPublicFavoritesOf(userId, page, pageSize);
            return photoCollection;
        }

        public DomainPhotos GetSubscriptionsOf(string userId, int page)
        {
            var pageSize = _configurationProvider.GetPhotoPageSize();

            var isAuthenticatedUser = _flickrPhotoProvider.IsAuthenticatedUser(userId);
            var photoCollection = isAuthenticatedUser
                ? _flickrPhotoProvider.GetPrivateSubscriptionsOf(userId, page, pageSize)
                : _flickrPhotoProvider.GetSubscriptionsOf(userId, page, pageSize);
            if (!isAuthenticatedUser)
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
            var pageSize = _configurationProvider.GetPhotoPageSize();

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
            var pageSize = _configurationProvider.GetPhotoPageSize();

            var groupInfo = _flickrPhotoProvider.GetGroupInfo(groupId);
            var photos = _flickrPhotoProvider.GetPhotosInGroup(groupId, page, pageSize);
            var domainPhotos = _flickrConverter.Convert(photos);
            domainPhotos = RemoveOptedOutUserPhotos(domainPhotos);

            return new DomainGroup(groupInfo.GroupName, domainPhotos);
        }

        public ListItems GetContacts(string userId, int page)
        {
            var pageSize = _configurationProvider.GetContactsPageSize();

            var contacts = _flickrPhotoProvider.GetContacts(userId, page, pageSize);
            var listElements = _flickrConverter.Convert(contacts);

            return listElements;
        }

        private string[] GetRecommendedUserIds(string userId)
        {
            var pageSize = _configurationProvider.GetPhotoPageSize();
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

            var pageSize = _configurationProvider.GetPhotoPageSize();
            var photos = GetAuthenticationAwareFavoritesOf(recommendedUserIds[page - 1], 1, pageSize);
            var domainPhotos = _flickrConverter.Convert(photos).Photos
                .Where(photo => photo.AuthorId != userId)
                .ToList();
            var recommendations = domainPhotos.Count > 0 
                ? new DomainPhotos(domainPhotos, page, recommendedUserIds.Length) 
                : GetRecommendations(userId, page + 1);
            return RemoveOptedOutUserPhotos(recommendations);
        }
    }
}
