using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace FlickrWrapper
{
    public class FlickrApi
    {
        private readonly string _apiKey;

        public FlickrApi(string apiKey)
        {
            _apiKey = apiKey;
        }

        public string PeopleGetPublicPhotos(string userId, 
            string safeSearch = null, string extras = null, 
            int? perPage = null, int? page = null)
        {
            string url = "http://api.flickr.com/services/rest?method=flickr.people.getPublicPhotos&";
            var parameters = new Dictionary<string, string>
                {
                    {"format", "json"}, 
                    {"nojsoncallback", "1"},
                    {"api_key", _apiKey}, 
                    {"user_id", userId}
                };
            if (safeSearch != null)
                parameters.Add("safe_search", safeSearch);
            if (extras != null)
                parameters.Add("extras", extras);
            if (perPage != null)
                parameters.Add("per_page", perPage.Value.ToString(CultureInfo.InvariantCulture));
            if (page != null)
                parameters.Add("page", page.Value.ToString(CultureInfo.InvariantCulture));

            var parameterArray = parameters.Select(item => item.Key + "=" + item.Value).ToArray();
            url += string.Join("&", parameterArray);

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
