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
        private readonly IFlickrRequestBuilder _flickrRequestBuilder;
        private readonly IHttpClient _httpClient;
        private readonly ISimplickrFormatter _simplickrFormatter;

        public FlickrApiInvoker(IFlickrRequestBuilder flickrRequestBuilder, IHttpClient httpClient, ISimplickrFormatter simplickrFormatter)
        {
            _flickrRequestBuilder = flickrRequestBuilder;
            _httpClient = httpClient;
            _simplickrFormatter = simplickrFormatter;
        }

        public TResponse Invoke<TResponse>(string methodName, IRequestParameters parameters)
        {
            IFlickrRequest flickrRequest = _flickrRequestBuilder.Build(methodName, parameters);
            string url = flickrRequest.GetUrl();
            string response = _httpClient.Get(url);
            return _simplickrFormatter.Deserialize<TResponse>(response);
        }
    }
}