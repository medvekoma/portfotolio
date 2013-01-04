using System;
using Simplickr.Formatters;
using Simplickr.Parameters;

namespace Simplickr
{
    public interface IFlickrApiInvoker
    {
        TResponse Invoke<TResponse>(string methodName, IRequestParameters parameters, bool sign = false);
    }

    public class FlickrApiInvoker : IFlickrApiInvoker
    {
        private readonly IFlickrRequestBuilder _flickrRequestBuilder;
        private readonly IHttpClient _httpClient;
        private readonly ISimplickrFormatter _simplickrFormatter;

        public FlickrApiInvoker(IFlickrRequestBuilder flickrRequestBuilder, IHttpClient httpClient, ISimplickrFormatter simplickrFormatter)
        {
            _flickrRequestBuilder = flickrRequestBuilder;
            _httpClient = httpClient;
            _simplickrFormatter = simplickrFormatter;
        }

        public TResponse Invoke<TResponse>(string methodName, IRequestParameters parameters, bool sign)
        {
            IFlickrRequest flickrRequest = _flickrRequestBuilder.Build(methodName, parameters, sign);
            string url = flickrRequest.GetUrl();

            Console.WriteLine(url);
            string response = _httpClient.Get(url);

            Console.WriteLine(response);

            return _simplickrFormatter.Deserialize<TResponse>(response);
        }
    }
}