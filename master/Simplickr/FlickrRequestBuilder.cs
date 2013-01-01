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
        private readonly ISimplickrInitializer _simplickrInitializer;

        public FlickrRequestBuilder(ISimplickrFormatter simplickrFormatter, ISimplickrInitializer simplickrInitializer)
        {
            _simplickrFormatter = simplickrFormatter;
            _simplickrInitializer = simplickrInitializer;
        }

        public IFlickrRequest Build(string methodName, IRequestParameters parameters)
        {
            parameters.ParameterMap["method"] = methodName;
            _simplickrFormatter.SetResponseFormat(parameters);
            _simplickrInitializer.SetApiKey(parameters);

            return new FlickrRequest(parameters.ParameterMap);
        }
    }
}