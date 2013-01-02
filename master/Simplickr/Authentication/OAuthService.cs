using System;
using System.Text;
using System.Web;
using OAuth;
using Simplickr.Configuration;

namespace Simplickr.Authentication
{
    public interface IOAuthService
    {
        string GetRequestTokenUrl();
    }

    public class OAuthService : IOAuthService
    {
        private readonly ISimplickrConfigurationProvider _simplickrConfigurationProvider;

        public OAuthService(ISimplickrConfigurationProvider simplickrConfigurationProvider)
        {
            _simplickrConfigurationProvider = simplickrConfigurationProvider;
        }

        public string GetRequestTokenUrl()
        {
            const string requestTokenBaseUrl = "http://www.flickr.com/services/oauth/request_token";
            var requestTokenBaseUri = new Uri(requestTokenBaseUrl);
            var simplickrConfig = _simplickrConfigurationProvider.GetConfig();
            string consumerKey = simplickrConfig.ApiKey;
            string consumerSecret = simplickrConfig.Secret;

            var oAuthBase = new OAuthBase();
            string nonce = oAuthBase.GenerateNonce();
            string timeStamp = oAuthBase.GenerateTimeStamp();
            string normalizedUrl;
            string normalizedRequestParameters;
            string signature = oAuthBase.GenerateSignature(requestTokenBaseUri,
                consumerKey, consumerSecret,
                string.Empty, string.Empty,
                "GET", timeStamp, nonce,
                OAuthBase.SignatureTypes.HMACSHA1,
                out normalizedUrl, out normalizedRequestParameters);

            signature = HttpUtility.UrlEncode(signature);

            string requestTokenUrl = normalizedUrl
                + "?" + normalizedRequestParameters
                + "&oauth_signature=" + signature;

            return requestTokenUrl;
        }
    }
}