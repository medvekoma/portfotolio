using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FlickrNet;
using Portfotolio.Domain;

namespace Portfotolio.FlickrEngine
{
    public interface IFlickrExifEngine
    {
        DomainPhoto ConvertPhotoToDomainPhotoWithExif(Photo photo);
        Dictionary<String, String> ExtractExifData(string photoId);
    }

    public class FlickrExifEngine : IFlickrExifEngine
    {
        private readonly FlickrPhotoProvider _flickrPhotoProvider;

        public FlickrExifEngine(FlickrPhotoProvider flickrPhotoProvider)
        {
            _flickrPhotoProvider = flickrPhotoProvider;
        }

        public DomainPhoto ConvertPhotoToDomainPhotoWithExif(Photo photo)
        {
            var domainPhoto = Mapper.Map<DomainPhoto>(photo);
            var exifData = ExtractExifData(domainPhoto.PhotoId);
            domainPhoto.ExifData = exifData;
            return domainPhoto;
        }

        public Dictionary<String, String> ExtractExifData(string photoId)
        {
            var exifData = _flickrPhotoProvider.GetExifDataOf(photoId);
            var exifDic = new Dictionary<String, String>();
            AddExifDataToExifDictionaryByTag(exifDic, exifData, "Model");
            AddExifDataToExifDictionaryByTag(exifDic, exifData, "Lens");
            AddExifDataToExifDictionaryByTag(exifDic, exifData, "Exposure");
            AddExifDataToExifDictionaryByTag(exifDic, exifData, "Aperture");
            AddExifDataToExifDictionaryByTag(exifDic, exifData, "Focal Length");
            return exifDic;
        }

        private void AddExifDataToExifDictionaryByTag(Dictionary<String, String> dictionary, ExifTagCollection collection, string label)
        {
            dictionary.Add(label, collection.Where(x => x.Label == label).Select(x => x.Raw).FirstOrDefault());
        }
    }
}