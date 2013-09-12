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
        IDictionary<String, String> ExtractBasicExifData(string photoId);
        KeyValuePair<String, String> ExtractExifDataByLabel(string photoId, string label);
    }

    public class FlickrExifEngine : IFlickrExifEngine
    {
        private readonly FlickrPhotoProvider _flickrPhotoProvider;

        public FlickrExifEngine(FlickrPhotoProvider flickrPhotoProvider)
        {
            _flickrPhotoProvider = flickrPhotoProvider;
        }

        public IDictionary<String, String> ExtractBasicExifData(string photoId)
        {
            IDictionary<String, String> exifDic = new Dictionary<String, String>();
            var exifData = _flickrPhotoProvider.GetExifDataOf(photoId);
            exifDic.Add(ExtractExifDataByLabel(photoId, "Model", exifData));
            exifDic.Add(ExtractExifDataByLabel(photoId, "Lens", exifData));
            exifDic.Add(ExtractExifDataByLabel(photoId, "Exposure", exifData));
            exifDic.Add(ExtractExifDataByLabel(photoId, "Aperture", exifData));
            exifDic.Add(ExtractExifDataByLabel(photoId, "Focal Length", exifData));
            return exifDic;
        }

        public KeyValuePair<String, String> ExtractExifDataByLabel(string photoId, string label)
        {
            var exifData = _flickrPhotoProvider.GetExifDataOf(photoId);
            return ExtractExifDataByLabel(photoId, label, exifData);
        }

        private KeyValuePair<String, String> ExtractExifDataByLabel(string photoId, string label, ExifTagCollection exifData)
        {
            return new KeyValuePair<string, string>(label,
                                                    exifData.Where(x => x.Label == label)
                                                              .Select(x => x.Raw)
                                                              .FirstOrDefault());
        }
    }
}