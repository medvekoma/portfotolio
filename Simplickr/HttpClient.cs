using System;
using System.IO;
using System.Net;
using System.Web;

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
            WebResponse response;
            var webRequest = WebRequest.Create(url);
            try
            {
                response = webRequest.GetResponse();
            }
            catch (WebException e)
            {
                response = e.Response;
            }
            using (var stream = response.GetResponseStream())
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