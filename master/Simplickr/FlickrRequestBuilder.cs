using Simplickr.Configuration;
using Simplickr.Formatters;
using Simplickr.Parameters;

namespace Simplickr
{
    public interface IFlickrRequestBuilder
    {
        IFlickrRequest Build(string methodName, IRequestParameters parameters);
    }

    public class FlickrRequestBuilder : IFlickrRequestBuilder
    {
        private readonly ISimplickrFormatter _simplickrFormatter;
        private readonly ISimplickrConfigurationProvider _simplickrConfigurationProvider;

        public FlickrRequestBuilder(ISimplickrFormatter simplickrFormatter, ISimplickrConfigurationProvider simplickrConfigurationProvider)
        {
            _simplickrFormatter = simplickrFormatter;
            _simplickrConfigurationProvider = simplickrConfigurationProvider;
        }

        public IFlickrRequest Build(string methodName, IRequestParameters parameters)
        {
            parameters.ParameterMap["method"] = methodName;
            parameters.ParameterMap["api_key"] = _simplickrConfigurationProvider.GetConfig().ApiKey;
            _simplickrFormatter.SetResponseFormat(parameters);

            return new FlickrRequest(parameters.ParameterMap);
        }
    }
}