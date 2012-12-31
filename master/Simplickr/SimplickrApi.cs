using System;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Simplickr.Response;

namespace Simplickr
{
    public interface ISimplickrApi
    {
        FlickrPhotosResponse GetPublicPhotos(GetPublicPhotosRequest request);
    }

    public class SimplickrApi : ISimplickrApi
    {
        private readonly ISimplickrInitializer _simplickrInitializer;
        private readonly ISimplickrFormatter _simplickrFormatter;
        private readonly ISimplickrInvoker _simplickrInvoker;

        public SimplickrApi(ISimplickrInitializer simplickrInitializer, ISimplickrFormatter simplickrFormatter, ISimplickrInvoker simplickrInvoker)
        {
            _simplickrInitializer = simplickrInitializer;
            _simplickrFormatter = simplickrFormatter;
            _simplickrInvoker = simplickrInvoker;
        }

        public FlickrPhotosResponse GetPublicPhotos(GetPublicPhotosRequest request)
        {
            request.ParameterMap.Add("method", "flickr.people.getPublicPhotos");
            _simplickrInitializer.Initialize(request);
            _simplickrFormatter.SetResponseFormat(request);

            var responseString = _simplickrInvoker.Invoke(request);

            Console.WriteLine(responseString);
            var flickrPhotosResponse = JsonConvert.DeserializeObject<FlickrPhotosResponse>(responseString);

            return flickrPhotosResponse;
        }
    }
}
