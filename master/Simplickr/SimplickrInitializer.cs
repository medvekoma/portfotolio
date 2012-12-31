namespace Simplickr
{
    public interface ISimplickrInitializer
    {
        void Initialize(ISimplickrRequest simplickrRequest);
    }

    public class SimplickrInitializer : ISimplickrInitializer
    {
        public void Initialize(ISimplickrRequest simplickrRequest)
        {
            simplickrRequest.ParameterMap.Add("api_key", "1a44395543c1473fb19d39467fb4c827");
        }
    }
}