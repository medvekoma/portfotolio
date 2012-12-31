namespace Simplickr
{
    public interface ISimplickrFormatter
    {
        void SetResponseFormat(ISimplickrRequest request);
    }

    public class SimplickrFormatter : ISimplickrFormatter
    {
        public void SetResponseFormat(ISimplickrRequest request)
        {
            request.ParameterMap
                   .Add("format", "json")
                   .Add("nojsoncallback", "1");
        }
    }
}