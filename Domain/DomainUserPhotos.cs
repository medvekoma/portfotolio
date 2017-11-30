namespace Portfotolio.Domain
{
    public class DomainUserPhotos
    {
        public DomainUser User { get; private set; }
        public DomainPhotos Photos { get; private set; }

        public DomainUserPhotos(DomainUser user, DomainPhotos photos)
        {
            User = user;
            Photos = photos;
        }
    }
}