using System;
using System.Text;
using System.Web;
using OAuth;
using Simplickr.Configuration;

namespace Simplickr.Authentication
{
    public interface IOAuthUrlService
    {
        string GetRequestTokenUrl(string callbackUrl);
    }

    public class OAuthUrlService : IOAuthUrlService
    {
        private readonly ISimplickrConfigurationProvider _simplickrConfigurationProvider;

        public OAuthUrlService(ISimplickrConfigurationProvider simplickrConfigurationProvider)
        {
            _simplickrConfigurationProvider = simplickrConfigurationProvider;
        }

        public string GetRequestTokenUrl(string callbackUrl)
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
                "GET", callbackUrl,
                timeStamp, nonce,
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