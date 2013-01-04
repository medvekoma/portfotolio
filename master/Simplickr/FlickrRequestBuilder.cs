using Simplickr.Configuration;
using Simplickr.Formatters;
using Simplickr.Parameters;

namespace Simplickr
{
    public interface IFlickrRequestBuilder
    {
        IFlickrRequest Build(string methodName, IRequestParameters parameters, bool sign = false);
    }

    public class FlickrRequestBuilder : IFlickrRequestBuilder
    {
        private readonly ISimplickrFormatter _simplickrFormatter;
        private readonly ISimplickrConfigurationProvider _simplickrConfigurationProvider;
        private readonly IFlickrSignatureGenerator _flickrSignatureGenerator;

        public FlickrRequestBuilder(ISimplickrFormatter simplickrFormatter, ISimplickrConfigurationProvider simplickrConfigurationProvider, IFlickrSignatureGenerator flickrSignatureGenerator)
        {
            _simplickrFormatter = simplickrFormatter;
            _simplickrConfigurationProvider = simplickrConfigurationProvider;
            _flickrSignatureGenerator = flickrSignatureGenerator;
        }

        public IFlickrRequest Build(string methodName, IRequestParameters parameters, bool sign)
        {
            parameters.ParameterMap["method"] = methodName;
            parameters.ParameterMap["api_key"] = _simplickrConfigurationProvider.GetConfig().ApiKey;
            _simplickrFormatter.SetResponseFormat(parameters);

            if (sign)
            {
                _flickrSignatureGenerator.AddSignature(parameters);
            }

            return new FlickrRequest(parameters.ParameterMap);
        }
    }
}