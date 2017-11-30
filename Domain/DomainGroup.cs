namespace Portfotolio.Domain
{
    public class DomainGroup
    {
        public string GroupName { get; private set; }
        public DomainPhotos Photos { get; private set; }

        public DomainGroup(string groupName, DomainPhotos photos)
        {
            GroupName = groupName;
            Photos = photos;
        }
    }
}