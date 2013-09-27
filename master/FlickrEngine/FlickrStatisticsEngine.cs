﻿using System.Collections.Generic;
using System.Linq;
using Portfotolio.Domain;

namespace Portfotolio.FlickrEngine
{
    public interface IFlickrStatisticsEngine
    {
        Statistic GetStatisticsOf(string userId, string label);
    }

    public class FlickrStatisticsEngine : IFlickrStatisticsEngine
    {
        private readonly IFlickrPhotoProvider _flickrPhotoProvider;
        private readonly IFlickrExifEngine _flickrExifEngine;
        
        public FlickrStatisticsEngine(IFlickrPhotoProvider flickrPhotoProvider, IFlickrExifEngine flickrExifEngine)
        {
            _flickrPhotoProvider = flickrPhotoProvider;
            _flickrExifEngine = flickrExifEngine;
        }

        public Statistic GetStatisticsOf(string userId, string label)
        {
            var publicPhotoIDs = _flickrPhotoProvider.GetPublicPhotoIDsOf(userId);
            var statistics = CalculateUsageStatisticsByLabel(publicPhotoIDs, label);
            return statistics;
        }

        private Statistic CalculateUsageStatisticsByLabel(IEnumerable<string> publicPhotoIDs, string label)
        {
            var exifList = from id in publicPhotoIDs
                           select _flickrExifEngine.ExtractExifDataByLabel(id, label);
            var usageList = from exif in exifList
                            group exif by exif.Value
                            into gr
                            orderby gr.Count() descending 
                            select new KeyValuePair<string, int>(gr.Key, gr.Count());
            var statistic = new Statistic(label, usageList.ToList());
            return statistic;
        }
    }
}