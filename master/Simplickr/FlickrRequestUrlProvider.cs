using Simplickr.Configuration;
using Simplickr.Formatters;
using Simplickr.Parameters;

namespace Simplickr
{
    public interface IFlickrRequestUrlProvider
    {
        string GetUrl(string methodName, IRequestParameters parameters);
    }

    public class FlickrRequestUrlProvider : IFlickrRequestUrlProvider
    {
        private readonly ISimplickrFormatter _simplickrFormatter;
        private readonly ISimplickrConfigurationProvider _simplickrConfigurationProvider;

        public FlickrRequestUrlProvider(ISimplickrFormatter simplickrFormatter, ISimplickrConfigurationProvider simplickrConfigurationProvider)
        {
            _simplickrFormatter = simplickrFormatter;
            _simplickrConfigurationProvider = simplickrConfigurationProvider;
        }

        public string GetUrl(string methodName, IRequestParameters parameters)
        {
            parameters.ParameterMap.Set("api_key", _simplickrConfigurationProvider.GetConfig().ApiKey);
            parameters.ParameterMap.Set("method", methodName);
            _simplickrFormatter.SetResponseFormat(parameters);

            return GetUrl(parameters.ParameterMap);
        }

        public string GetUrl(ParameterMap parameterMap)
        {
            return "http://api.flickr.com/services/rest?" + parameterMap.GetQueryString();
        }
    }
}