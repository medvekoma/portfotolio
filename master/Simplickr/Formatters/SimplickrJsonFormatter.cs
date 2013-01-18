using Newtonsoft.Json;
using Simplickr.Parameters;

namespace Simplickr.Formatters
{
    public class SimplickrJsonFormatter : ISimplickrFormatter
    {
        public void SetResponseFormat(IRequestParameters requestParameters)
        {
            requestParameters.ParameterMap
                   .Set("format", "json")
                   .Set("nojsoncallback", "1");
        }

        public TResponse Deserialize<TResponse>(string response)
        {
            return JsonConvert.DeserializeObject<TResponse>(response);
        }
    }
}