using System;
using System.Collections.Generic;
using System.Linq;
using FlickrNet;
using Portfotolio.Domain.Exceptions;

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

        GroupInfoCollection GetGroups(string userId);
        PhotoCollection GetPhotosInGroup(string groupId, int page, int pageSize);
        GroupFullInfo GetGroupInfo(string groupId);
        
        PhotoCollection GetInterestingPhotos(int page, int pageSize);

        ExifTagCollection GetExifDataOf(string photoId);

        IEnumerable<string> GetPublicPhotoIDsOf(string userId);

        bool IsAcceptedUserName(string userName);
    }

    public class FlickrPhotoProvider : IFlickrPhotoProvider
    {
        private readonly IFlickrFactory _flickrFactory;
        private const PhotoSearchExtras PhotoSearchExtrasWithPathAlias = PhotoSearchExtras.OwnerName | PhotoSearchExtras.PathAlias | PhotoSearchExtras.AllUrls | PhotoSearchExtras.License;
 


        public FlickrPhotoProvider(IFlickrFactory flickrFactory)
        {
            _flickrFactory = flickrFactory;
        }

        public FoundUser GetUserByPathAlias(string userAlias)
        {
            var url = "flickr.com/photos/" + userAlias;

            try
            {
                return _flickrFactory.GetFlickr().UrlsLookupUser(url);
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
                return _flickrFactory.GetFlickr().PeopleGetInfo(userId);
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
                _flickrFactory.GetFlickr().PeopleFindByUserName(userName);
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

        public PhotoCollection GetPhotosOf(string userId, int page, int pageSize)
        {
            try
            {
                return _flickrFactory.GetFlickr().PeopleGetPhotos(userId, SafetyLevel.None, 
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
                return _flickrFactory.GetFlickr().PeopleGetPublicPhotos(userId, page, pageSize, SafetyLevel.None, PhotoSearchExtras.All);
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

        public IEnumerable<string> GetPublicPhotoIDsOf(string userId)
        {
            try
            {
                var photos = _flickrFactory.GetFlickr().PeopleGetPublicPhotos(userId);
                var IDs = (from photo in photos
                                select photo.PhotoId).ToList();
                return IDs;
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

        public ExifTagCollection GetExifDataOf(string photoId)
        {
            return _flickrFactory.GetFlickr().PhotosGetExif(photoId);
        }

        public PhotoCollection GetFavoritesOf(string userId, int page, int pageSize)
        {
            try
            {
                return _flickrFactory.GetFlickr().FavoritesGetList(userId, DateTime.MinValue, DateTime.MinValue, PhotoSearchExtrasWithPathAlias, page, pageSize);
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
                return _flickrFactory.GetFlickr().FavoritesGetPublicList(userId, DateTime.MinValue, DateTime.MinValue, PhotoSearchExtrasWithPathAlias, page, pageSize);
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
                return _flickrFactory.GetFlickr().PhotosSearch(photoSearchOptions);
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
                return _flickrFactory.GetFlickr().PhotosGetContactsPublicPhotos(userId, 50, PhotoSearchExtrasWithPathAlias);
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
                return _flickrFactory.GetFlickr().PhotosetsGetList(userId);
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
                return _flickrFactory.GetFlickr().PhotosetsGetPhotos(albumId, PhotoSearchExtrasWithPathAlias, page, pageSize);
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
                return _flickrFactory.GetFlickr().PhotosetsGetInfo(albumId);
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

        public GroupInfoCollection GetGroups(string userId)
        {
            try
            {
                return _flickrFactory.GetFlickr().PeopleGetPublicGroups(userId);
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
                return _flickrFactory.GetFlickr().GroupsPoolsGetPhotos(groupId, null, null, PhotoSearchExtrasWithPathAlias, page, pageSize);
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
                return _flickrFactory.GetFlickr().GroupsGetInfo(groupId);
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
                return _flickrFactory.GetFlickr().ContactsGetList(page, pageSize);
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
                return _flickrFactory.GetFlickr().ContactsGetPublicList(userId, page, pageSize);
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
                return _flickrFactory.GetFlickr().InterestingnessGetList(PhotoSearchExtrasWithPathAlias, page, pageSize);
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