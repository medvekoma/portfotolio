namespace Portfotolio.Domain
{
    public interface IPhotoEngine
    {
        DomainPhotos GetPhotosOf(string userId, int page);
        DomainPhotos GetFavoritesOf(string userId, int page);
        DomainPhotos GetSubscriptionsOf(string userId, int page);
        Albums GetAlbumsOf(string userId);
        Album GetAlbum(string albumId);
        AlbumPhotos GetPhotosInAlbum(string albumId, int page);
        ListItems GetGroups(string userId);
        DomainGroup GetGroup(string groupId, int page);
        ListItems GetContacts(string userId, int page);
        DomainPhotos GetRecommendations(string userId, int page);
        DomainPhotos GetInterestingPhotos(int page);
    }
}