using System.Linq;
using FlickrNet;
using Portfotolio.Domain;

namespace Portfotolio.FlickrEngine
{
    public class FlickrConverter : IFlickrConverter
    {
        public DomainPhotos Convert(PhotoCollection photoCollection)
        {
            if (photoCollection == null)
                return null;

            var domainPhotos = photoCollection
                .Select(photo => new DomainPhoto(
                                     photo.PhotoId, photo.UserId, photo.OwnerName, string.IsNullOrEmpty(photo.PathAlias) ? photo.UserId : photo.PathAlias, 
                                     Convert(photo.License), photo.Title, photo.WebUrl + "lightbox/",
                                     photo.SmallUrl, photo.MediumUrl, photo.LargeUrl, photo.SmallWidth ?? 240, photo.SmallHeight ?? 240))
                .ToList();
            return new DomainPhotos(domainPhotos, photoCollection.Page, photoCollection.Pages);
        }

        public DomainPhotos Convert(PhotosetPhotoCollection photosetPhotos)
        {
            if (photosetPhotos == null)
                return null;

            var domainPhotos = photosetPhotos
                .Select(photo => new DomainPhoto(
                                     photo.PhotoId, photo.UserId, photo.OwnerName, string.IsNullOrEmpty(photo.PathAlias) ? photo.UserId : photo.PathAlias, 
                                     Convert(photo.License), photo.Title, photo.WebUrl,
                                     photo.SmallUrl, photo.MediumUrl, photo.LargeUrl, photo.SmallWidth ?? 240, photo.SmallHeight ?? 240))
                .ToList();
            return new DomainPhotos(domainPhotos, photosetPhotos.Page, photosetPhotos.Pages);
        }

        public ListItems Convert(GroupInfoCollection groups)
        {
            var items = groups
                .Select(group => new ListItem(group.GroupId, group.GroupName))
                .ToArray();

            return new ListItems(items, 1, 1);
        }

        public ListItems Convert(ContactCollection contacts)
        {
            var items = contacts
                .Select(contact => new ListItem(
                                       string.IsNullOrEmpty(contact.PathAlias) ? contact.UserId : contact.PathAlias,
                                       contact.UserName,
                                       contact.BuddyIconUrl))
                .ToArray();
            return new ListItems(items, contacts.Page, contacts.Pages); 
        }

        public Album Convert(Photoset set)
        {
            return new Album(set.OwnerId, set.PhotosetId, set.Title, set.PhotosetSmallUrl);
        }

        public Albums Convert(PhotosetCollection photosets)
        {
            var items = photosets
                .Select(Convert)
                .ToArray();

            return new Albums(items);
        }

        public DomainLicense Convert(LicenseType licenseType)
        {
            switch (licenseType)
            {
                case LicenseType.AllRightsReserved:
                    return DomainLicense.AllRightsReserved;
                case LicenseType.AttributionCC:
                case LicenseType.AttributionNoDerivativesCC:
                case LicenseType.AttributionNoncommercialCC:
                case LicenseType.AttributionNoncommercialNoDerivativesCC:
                case LicenseType.AttributionNoncommercialShareAlikeCC:
                case LicenseType.AttributionShareAlikeCC:
                    return DomainLicense.CreativeCommons;
            }
            return DomainLicense.Other;
        }
    }
}