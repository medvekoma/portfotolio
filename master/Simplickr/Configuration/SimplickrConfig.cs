namespace Simplickr.Configuration
{
    public class SimplickrConfig
    {
        public string ApiKey { get; private set; }
        public string Secret { get; private set; }

        public SimplickrConfig(string apiKey, string secret)
        {
            ApiKey = apiKey;
            Secret = secret;
        }
    }
}