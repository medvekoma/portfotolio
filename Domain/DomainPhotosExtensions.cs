using System.Linq;

namespace Portfotolio.Domain
{
    public static class DomainPhotosExtensions
    {
        public static DomainPhotos RemoveCopyrightedPhotos(this DomainPhotos domainPhotos)
        {
            var filteredPhotos = domainPhotos.Photos
                                             .Where(photo => photo.IsLicensed)
                                             .ToList();

            return new DomainPhotos(filteredPhotos, domainPhotos.Page, domainPhotos.Pages);
        }
    }
}