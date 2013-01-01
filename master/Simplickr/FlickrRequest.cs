using System.Linq;

namespace Simplickr
{
    public interface IFlickrRequest
    {
        string GetUrl();
    }

    public class FlickrRequest : IFlickrRequest
    {
        private readonly ParameterMap _parameterMap;

        public FlickrRequest(ParameterMap parameterMap)
        {
            _parameterMap = parameterMap;
        }

        private string GetQueryString()
        {
            var items = _parameterMap.Select(item => item.Key + '=' + item.Value).ToArray();
            return string.Join("&", items);
        }

        public string GetUrl()
        {
            return "http://api.flickr.com/services/rest?" + GetQueryString();
        }
    }
}