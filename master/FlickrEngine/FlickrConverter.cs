using System;
using System.Collections.Generic;
using System.Linq;
using FlickrNet;
using Portfotolio.Domain;
using AutoMapper;

namespace Portfotolio.FlickrEngine
{
    public class FlickrConverter : IFlickrConverter
    {
        private readonly IFlickrExifEngine _flickrExifEngine;

        public FlickrConverter(FlickrExifEngine flickrExifEngine)
        {
            _flickrExifEngine = flickrExifEngine;
        }

        public DomainPhotos Convert(PhotoCollection photoCollection)
        {
            if (photoCollection == null)
                return null;
            
                var domainPhotos = photoCollection
                .Select(photo => _flickrExifEngine.ConvertPhotoToDomainPhoto(photo))
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
                                     photo.Title, photo.WebUrl,
                                     photo.Medium640Url, photo.Medium640Width ?? 640, photo.Medium640Width ?? 640,
                                     photo.IsLicensed(), photo.DateTaken, photo.Views))
                .ToList();


            return new DomainPhotos(domainPhotos, photosetPhotos.Page, photosetPhotos.Pages);
        }

        public ListItems Convert(GroupInfoCollection groups)
        {
            var items = groups
                .Select(group => Mapper.Map<ListItem>(group))
                .ToArray();

            return new ListItems(items, 1, 1);
        }

        public ListItems Convert(ContactCollection contacts)
        {
            var items = contacts
                .Select(contact => Mapper.Map<ListItem>(contact))
                .ToArray();
            return new ListItems(items, contacts.Page, contacts.Pages); 
        }

        public Album Convert(Photoset set)
        {
            return new Album(set.OwnerId, set.PhotosetId, set.Title, set.PhotosetSmallUrl, set.NumberOfPhotos);
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