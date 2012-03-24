namespace Portfotolio.Domain
{
    public class AlbumPhotos
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string OwnerId { get; private set; }
        public DomainPhotos Photos { get; private set; }

        public AlbumPhotos(string title, string description, string ownerId, DomainPhotos photos)
        {
            Title = title;
            Description = description;
            OwnerId = ownerId;
            Photos = photos;
        }
    }
}