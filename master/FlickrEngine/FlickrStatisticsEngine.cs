using System.Collections.Generic;
using System.Linq;
using Portfotolio.Domain;

namespace Portfotolio.FlickrEngine
{
    public interface IFlickrStatisticsEngine
    {
        List<Statistic> GetStatisticsOf(string userId);
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

        public List<Statistic> GetStatisticsOf(string userId)
        {
            var publicPhotoIDs = _flickrPhotoProvider.GetPublicPhotoIDsOf(userId);
            var statistics = new List<Statistic> {GetUsageStatisticsByLabel(publicPhotoIDs, "Model")};
            return statistics;
        }

        private Statistic GetUsageStatisticsByLabel(IEnumerable<string> publicPhotoIDs, string label)
        {
            var exifList = (from id in publicPhotoIDs
                           select _flickrExifEngine.ExtractExifDataByLabel(_flickrPhotoProvider.GetExifDataOf(id), label)).ToList();
            var usageList = (from exif in exifList
                             group exif by exif.Value
                             into gr
                             orderby gr.Count() descending 
                             select new KeyValuePair<string, int>(gr.Key, gr.Count()));
            var stats = usageList.ToList();
            var statictic = new Statistic(label, stats);
            return statictic;
        }
    }
}