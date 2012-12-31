using Newtonsoft.Json;

namespace Simplickr.Response
{
    public class FlickrPhotosResponse
    {
        public FlickrPhotos Photos { get; set; }

        public string Stat { get; set; }
    }
}