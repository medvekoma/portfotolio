using System.IO;
using System.Net;

namespace Simplickr
{
    public interface IHttpClient
    {
        string Get(string url);
    }

    public class HttpClient : IHttpClient
    {
        public string Get(string url)
        {
            using (var webClient = new WebClient())
            using (var stream = webClient.OpenRead(url))
            {
                if (stream == null)
                    return null;
                using (var streamReader = new StreamReader(stream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
    }
}