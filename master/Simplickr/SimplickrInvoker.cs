using System.IO;
using System.Net;

namespace Simplickr
{
    public interface ISimplickrInvoker
    {
        string Invoke(ISimplickrRequest request);
    }

    public class SimplickrInvoker : ISimplickrInvoker
    {
        public string Invoke(ISimplickrRequest request)
        {
            var queryString = request.ParameterMap.GetQueryString();
            var url = "http://api.flickr.com/services/rest?" + queryString;

            using (var webClient = new WebClient())
            using (var stream = webClient.OpenRead(url))
            {
                if (stream == null)
                    return null;
                using (var streamReader = new StreamReader(stream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
    }
}