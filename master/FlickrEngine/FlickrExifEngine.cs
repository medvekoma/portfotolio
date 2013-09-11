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
        DomainPhoto ConvertPhotoToDomainPhoto(Photo photo);
        IDictionary<String, String> ExtractBasicExifData(string photoId);
        KeyValuePair<String, String> ExtractExifDataByLabel(ExifTagCollection exifData, string label);
    }

    public class FlickrExifEngine : IFlickrExifEngine
    {
        private readonly FlickrPhotoProvider _flickrPhotoProvider;

        public FlickrExifEngine(FlickrPhotoProvider flickrPhotoProvider)
        {
            _flickrPhotoProvider = flickrPhotoProvider;
        }

        public DomainPhoto ConvertPhotoToDomainPhoto(Photo photo)
        {
            var domainPhoto = Mapper.Map<DomainPhoto>(photo);
            //var exifData = ExtractBasicExifData(domainPhoto.PhotoId);
            //domainPhoto.ExifData = exifData;
            return domainPhoto;
        }

        public IDictionary<String, String> ExtractBasicExifData(string photoId)
        {
            IDictionary<String, String> exifDic = new Dictionary<String, String>();
            var exifData = _flickrPhotoProvider.GetExifDataOf(photoId);
            exifDic.Add(ExtractExifDataByLabel(exifData, "Model"));
            exifDic.Add(ExtractExifDataByLabel(exifData, "Lens"));
            exifDic.Add(ExtractExifDataByLabel(exifData, "Exposure"));
            exifDic.Add(ExtractExifDataByLabel(exifData, "Aperture"));
            exifDic.Add(ExtractExifDataByLabel(exifData, "Focal Length"));
            return exifDic;
        }

        public KeyValuePair<String, String> ExtractExifDataByLabel(ExifTagCollection exifData, string label)
        {
            return new KeyValuePair<string, string>(label,
                                                    exifData.Where(x => x.Label == label)
                                                              .Select(x => x.Raw)
                                                              .FirstOrDefault());
        }
    }
}