using System;
using FlickrNet;
using Portfotolio.Domain.Exceptions;
using Portfotolio.Domain.Persistency;

namespace Portfotolio.FlickrEngine
{
    public interface IFlickrPhotoProvider
    {
        FoundUser GetUserByPathAlias(string userAlias);
        Person GetUserByUserId(string userId);

        PhotoCollection GetPhotosOf(string userId, int page, int pageSize);
        PhotoCollection GetPublicPhotosOf(string userId, int page, int pageSize);

        PhotoCollection GetFavoritesOf(string userId, int page, int pageSize);
        PhotoCollection GetPublicFavoritesOf(string userId, int page, int pageSize);

        PhotoCollection GetSubscriptionsOf(string userId, int page, int pageSize);
        PhotoCollection GetPrivateSubscriptionsOf(string userId, int page, int pageSize);

        ContactCollection GetContacts(string userId, int page, int pageSize);
        ContactCollection GetPrivateContacts(string userId, int page, int pageSize);

        Photoset GetAlbumInfo(string albumId);
        PhotosetCollection GetAlbumsOf(string userId);
        PhotosetPhotoCollection GetPhotosInAlbum(string albumId, int page, int pageSize);

        PublicGroupInfoCollection GetGroups(string userId);
        PhotoCollection GetPhotosInGroup(string groupId, int page, int pageSize);
        GroupFullInfo GetGroupInfo(string groupId);
        
        PhotoCollection GetInterestingPhotos(int page, int pageSize);
        bool IsAcceptedUserName(string userName);
        bool IsAuthenticatedUser();
        bool IsAuthenticatedUser(string userId);
    }

    public class FlickrPhotoProvider : IFlickrPhotoProvider
    {
        private readonly Flickr _flickr;
        private const PhotoSearchExtras PhotoSearchExtrasWithPathAlias = PhotoSearchExtras.OwnerName | PhotoSearchExtras.PathAlias | PhotoSearchExtras.AllUrls;
        private readonly string _authenticatedUserId;

        public FlickrPhotoProvider(IUserSession userSession)
        {
            _flickr = new Flickr();
            var authenticationInfo = userSession.GetAuthenticationInfo();
            if (authenticationInfo.IsAuthenticated)
            {
                _flickr.OAuthAccessToken = authenticationInfo.Token;
                _flickr.OAuthAccessTokenSecret = authenticationInfo.TokenSecret;
                _authenticatedUserId = authenticationInfo.UserId;
            }
        }

        public FoundUser GetUserByPathAlias(string userAlias)
        {
            var url = "flickr.com/photos/" + userAlias;

            try
            {
                return _flickr.UrlsLookupUser(url);
            }
            catch (FlickrApiException flickrApiException)
            {
                switch (flickrApiException.Code)
                {
                    case 1:
                        throw new AuthorNotFoundException(userAlias, flickrApiException);
                    case 105:
                        throw new FlickrServiceUnavailableException(flickrApiException);
                    default:
                        throw;
                }
                
            }
        }

        public Person GetUserByUserId(string userId)
        {
            try
            {
                return _flickr.PeopleGetInfo(userId);
            }
            catch (FlickrApiException flickrApiException)
            {
                switch (flickrApiException.Code)
                {
                    case 1:
                        throw new AuthorNotFoundException(userId, flickrApiException);
                    case 105:
                        throw new FlickrServiceUnavailableException(flickrApiException);
                    default:
                        throw;
                }
            }
        }

        public bool IsAcceptedUserName(string userName)
        {
            try
            {
                _flickr.PeopleFindByUserName(userName);
                return true;
            }
            catch (FlickrApiException flickrApiException)
            {
                switch (flickrApiException.Code)
                {
                    case 1:
                        return false;
                    case 105:
                        throw new FlickrServiceUnavailableException(flickrApiException);
                    default:
                        throw;
                }
            }
        }

        public bool IsAuthenticatedUser()
        {
            return _flickr.IsAuthenticated;
        }

        public bool IsAuthenticatedUser(string userId)
        {
            return IsAuthenticatedUser() && (_authenticatedUserId == userId);
        }

        public PhotoCollection GetPhotosOf(string userId, int page, int pageSize)
        {
            try
            {
                return _flickr.PeopleGetPhotos(userId, SafetyLevel.None, 
                    DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, 
                    ContentTypeSearch.All, PrivacyFilter.None, PhotoSearchExtrasWithPathAlias, page, pageSize);
            }
            catch (FlickrApiException flickrApiException)
            {
                switch (flickrApiException.Code)
                {
                    case 2:
                        throw new AuthorNotFoundException(userId, flickrApiException);
                    case 105:
                        throw new FlickrServiceUnavailableException(flickrApiException);
                    default:
                        throw;
                }
            }
        }

        public PhotoCollection GetPublicPhotosOf(string userId, int page, int pageSize)
        {
            try
            {
                return _flickr.PeopleGetPublicPhotos(userId, page, pageSize, SafetyLevel.None, PhotoSearchExtrasWithPathAlias);
            }
            catch (FlickrApiException flickrApiException)
            {
                switch (flickrApiException.Code)
                {
                    case 1:
                        throw new AuthorNotFoundException(userId, flickrApiException);
                    case 105:
                        throw new FlickrServiceUnavailableException(flickrApiException);
                    default:
                        throw;
                }
            }
        }

        public PhotoCollection GetFavoritesOf(string userId, int page, int pageSize)
        {
            try
            {
                return _flickr.FavoritesGetList(userId, DateTime.MinValue, DateTime.MinValue, PhotoSearchExtrasWithPathAlias, page, pageSize);
            }
            catch (FlickrApiException flickrApiException)
            {
                switch (flickrApiException.Code)
                {
                    case 1:
                        throw new AuthorNotFoundException(userId, flickrApiException);
                    case 105:
                        throw new FlickrServiceUnavailableException(flickrApiException);
                    default:
                        throw;
                }
            }
        }

        public PhotoCollection GetPublicFavoritesOf(string userId, int page, int pageSize)
        {
            try
            {
                return _flickr.FavoritesGetPublicList(userId, DateTime.MinValue, DateTime.MinValue, PhotoSearchExtrasWithPathAlias, page, pageSize);
            }
            catch (FlickrApiException flickrApiException)
            {
                switch (flickrApiException.Code)
                {
                    case 1:
                        throw new AuthorNotFoundException(userId, flickrApiException);
                    case 105:
                        throw new FlickrServiceUnavailableException(flickrApiException);
                    default:
                        throw;
                }
            }
        }

        public PhotoCollection GetPrivateSubscriptionsOf(string userId, int page, int pageSize)
        {
            var aWeekAgo = DateTime.Now.AddDays(-7);
            var photoSearchOptions = new PhotoSearchOptions(userId)
            {
                Contacts = ContactSearch.AllContacts,
                Page = page,
                PerPage = pageSize,
                Extras = PhotoSearchExtrasWithPathAlias,
                MinUploadDate = aWeekAgo,
            };
            try
            {
                return _flickr.PhotosSearch(photoSearchOptions);
            }
            catch (FlickrApiException flickrApiException)
            {
                switch (flickrApiException.Code)
                {
                    case 2:
                        throw new AuthorNotFoundException(userId, flickrApiException);
                    case 105:
                        throw new FlickrServiceUnavailableException(flickrApiException);
                    default:
                        throw;
                }
            }
        }

        public PhotoCollection GetSubscriptionsOf(string userId, int page, int pageSize)
        {
            try
            {
                return _flickr.PhotosGetContactsPublicPhotos(userId, 50, PhotoSearchExtrasWithPathAlias);
            }
            catch (FlickrApiException flickrApiException)
            {
                switch (flickrApiException.Code)
                {
                    case 1:
                        throw new AuthorNotFoundException(userId, flickrApiException);
                    case 105:
                        throw new FlickrServiceUnavailableException(flickrApiException);
                    default:
                        throw;
                }
            }
        }

        public PhotosetCollection GetAlbumsOf(string userId)
        {
            try
            {
                return _flickr.PhotosetsGetList(userId);
            }
            catch (FlickrApiException flickrApiException)
            {
                switch (flickrApiException.Code)
                {
                    case 1:
                        throw new AuthorNotFoundException(userId, flickrApiException);
                    case 105:
                        throw new FlickrServiceUnavailableException(flickrApiException);
                    default:
                        throw;
                }
            }
        }

        public PhotosetPhotoCollection GetPhotosInAlbum(string albumId, int page, int pageSize)
        {
            try
            {
                return _flickr.PhotosetsGetPhotos(albumId, PhotoSearchExtrasWithPathAlias, page, pageSize);
            }
            catch (FlickrApiException flickrApiException)
            {
                switch (flickrApiException.Code)
                {
                    case 1:
                        throw new AlbumNotFoundException(albumId);
                    case 105:
                        throw new FlickrServiceUnavailableException(flickrApiException);
                    default:
                        throw;
                }
            }
        }

        public Photoset GetAlbumInfo(string albumId)
        {
            try
            {
                return _flickr.PhotosetsGetInfo(albumId);
            }
            catch (FlickrApiException flickrApiException)
            {
                switch (flickrApiException.Code)
                {
                    case 1:
                        throw new AlbumNotFoundException(albumId, flickrApiException);
                    case 105:
                        throw new FlickrServiceUnavailableException(flickrApiException);
                    default:
                        throw;
                }
            }
        }

        public PublicGroupInfoCollection GetGroups(string userId)
        {
            try
            {
                return _flickr.PeopleGetPublicGroups(userId);
            }
            catch (FlickrApiException flickrApiException)
            {
                switch (flickrApiException.Code)
                {
                    case 1:
                        throw new AuthorNotFoundException(userId, flickrApiException);
                    case 105:
                        throw new FlickrServiceUnavailableException(flickrApiException);
                    default:
                        throw;
                }
            }
        }

        public PhotoCollection GetPhotosInGroup(string groupId, int page, int pageSize)
        {
            try
            {
                return _flickr.GroupsPoolsGetPhotos(groupId, null, null, PhotoSearchExtrasWithPathAlias, page, pageSize);
            }
            catch (FlickrApiException flickrApiException)
            {
                switch (flickrApiException.Code)
                {
                    case 1:
                        throw new GroupNotFoundException(groupId);
                    case 2:
                        throw new NotEnoughPermissionsException(groupId);
                    default:
                        throw;
                }
            }
        }

        public GroupFullInfo GetGroupInfo(string groupId)
        {
            try
            {
                return _flickr.GroupsGetInfo(groupId);
            }
            catch (FlickrApiException flickrApiException)
            {
                switch (flickrApiException.Code)
                {
                    case 1:
                        throw new GroupNotFoundException(groupId);
                    default:
                        throw;
                }
            }
        }

        public ContactCollection GetPrivateContacts(string userId, int page, int pageSize)
        {
            try
            {
                return _flickr.ContactsGetList(page, pageSize);
            }
            catch (FlickrApiException flickrApiException)
            {
                switch (flickrApiException.Code)
                {
                    case 99:
                        throw new NotEnoughPermissionsException(userId, flickrApiException);
                    case 105:
                        throw new FlickrServiceUnavailableException(flickrApiException);
                    default:
                        throw;
                }
            }
        }

        public ContactCollection GetContacts(string userId, int page, int pageSize)
        {
            try
            {
                return _flickr.ContactsGetPublicList(userId, page, pageSize);
            }
            catch (FlickrApiException flickrApiException)
            {
                switch (flickrApiException.Code)
                {
                    case 1:
                        throw new AuthorNotFoundException(userId, flickrApiException);
                    case 105:
                        throw new FlickrServiceUnavailableException(flickrApiException);
                    default:
                        throw;
                }
            }
        }

        public PhotoCollection GetInterestingPhotos(int page, int pageSize)
        {
            try
            {
                return _flickr.InterestingnessGetList(PhotoSearchExtrasWithPathAlias, page, pageSize);
            }
            catch (FlickrApiException flickrApiException)
            {
                switch (flickrApiException.Code)
                {
                    case 105:
                        throw new FlickrServiceUnavailableException(flickrApiException);
                    default:
                        throw;
                }
            }
        }
    }
}