using Simplickr.Parameters;

namespace Simplickr.Formatters
{
    public interface ISimplickrFormatter
    {
        void SetResponseFormat(IRequestParameters requestParameters);
        TResponse Deserialize<TResponse>(string response);
    }
}