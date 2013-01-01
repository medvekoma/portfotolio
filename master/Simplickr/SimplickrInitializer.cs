using Simplickr.Parameters;

namespace Simplickr
{
    public interface ISimplickrInitializer
    {
        void SetApiKey(IRequestParameters requestParameters);
    }

    public class SimplickrInitializer : ISimplickrInitializer
    {
        public void SetApiKey(IRequestParameters requestParameters)
        {
            requestParameters.ParameterMap.Add("api_key", "1a44395543c1473fb19d39467fb4c827");
        }
    }
}