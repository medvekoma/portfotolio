using System;
using Simplickr.Formatters;
using Simplickr.Parameters;

namespace Simplickr
{
    public interface IFlickrApiInvoker
    {
        TResponse Invoke<TResponse>(string methodName, IRequestParameters parameters);
    }

    public class FlickrApiInvoker : IFlickrApiInvoker
    {
        private readonly IFlickrRequestUrlProvider _flickrRequestUrlProvider;
        private readonly IHttpClient _httpClient;
        private readonly ISimplickrFormatter _simplickrFormatter;

        public FlickrApiInvoker(IFlickrRequestUrlProvider flickrRequestUrlProvider, IHttpClient httpClient, ISimplickrFormatter simplickrFormatter)
        {
            _flickrRequestUrlProvider = flickrRequestUrlProvider;
            _httpClient = httpClient;
            _simplickrFormatter = simplickrFormatter;
        }

        public TResponse Invoke<TResponse>(string methodName, IRequestParameters parameters)
        {
            string url = _flickrRequestUrlProvider.GetUrl(methodName, parameters);
            Console.WriteLine(url);

            string response = _httpClient.Get(url);
            Console.WriteLine(response);

            return _simplickrFormatter.Deserialize<TResponse>(response);
        }
    }
}