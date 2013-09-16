using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FlickrNet;
using Portfotolio.Domain;
using Portfotolio.Domain.Persistency;

namespace Portfotolio.FlickrEngine
{
    public interface IFlickrExifEngine
    {
        IDictionary<String, String> ExtractBasicExifData(string photoId);
        KeyValuePair<String, String> ExtractExifDataByLabel(string photoId, string label);
    }

    public class FlickrExifEngine : IFlickrExifEngine
    {
        private const string ExifSessionKeyPrefix = "exif_";
        private readonly FlickrPhotoProvider _flickrPhotoProvider;
        private readonly IUserSession _userSession;

        public FlickrExifEngine(FlickrPhotoProvider flickrPhotoProvider, IUserSession userSession)
        {
            _flickrPhotoProvider = flickrPhotoProvider;
            _userSession = userSession;
        }

        public IDictionary<String, String> ExtractBasicExifData(string photoId)
        {
            IDictionary<String, String> exifDic = new Dictionary<String, String>();
            var exifData = _flickrPhotoProvider.GetExifDataOf(photoId);
            exifDic.Add(ExtractExifDataByLabel("Model", exifData));
            exifDic.Add(ExtractExifDataByLabel("Lens", exifData));
            exifDic.Add(ExtractExifDataByLabel("Exposure", exifData));
            exifDic.Add(ExtractExifDataByLabel("Aperture", exifData));
            exifDic.Add(ExtractExifDataByLabel("Focal Length", exifData));
            return exifDic;
        }

        public KeyValuePair<String, String> ExtractExifDataByLabel(string photoId, string label)
        {
            var exifData = _userSession.GetValue<ExifTagCollection>(ExifSessionKeyPrefix + photoId);
            if (exifData == null)
            {
                exifData = _flickrPhotoProvider.GetExifDataOf(photoId);
                _userSession.SetValue(ExifSessionKeyPrefix + photoId, exifData);
            }
            return ExtractExifDataByLabel(label, exifData);
        }

        private KeyValuePair<String, String> ExtractExifDataByLabel(string label, ExifTagCollection exifData)
        {
            return new KeyValuePair<string, string>(label,
                                                    exifData.Where(x => x.Label == label)
                                                              .Select(x => x.Raw)
                                                              .FirstOrDefault());
        }
    }
}