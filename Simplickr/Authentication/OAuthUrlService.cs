using Simplickr.Configuration;

namespace Simplickr.Authentication
{
    public interface IOAuthUrlService
    {
        string GetRequestTokenUrl(string callbackUrl);
        string GetUserAuthorizationUrl(string token);
        string GetAccessTokenUrl(string token, string tokenSecret, string verifier);
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
            var simplickrConfig = _simplickrConfigurationProvider.GetConfig();
            string consumerKey = simplickrConfig.ApiKey;
            string consumerSecret = simplickrConfig.Secret;

            var oAuthUrlProvider = new OAuthUrlProvider()
                .Url(requestTokenBaseUrl)
                .ConsumerKey(consumerKey)
                .ConsumerSecret(consumerSecret)
                .Callback(callbackUrl);

            string requestTokenUrl = oAuthUrlProvider.GetSignedUrl();

            return requestTokenUrl;
        }

        public string GetUserAuthorizationUrl(string token)
        {
            const string userAuthorizationUrl = "http://www.flickr.com/services/oauth/authorize";

            return userAuthorizationUrl + "?oauth_token=" + token + "&perms=read";
        }

        public string GetAccessTokenUrl(string token, string tokenSecret, string verifier)
        {
            const string accessTokenBaseUrl = "http://www.flickr.com/services/oauth/access_token";
            var simplickrConfig = _simplickrConfigurationProvider.GetConfig();
            string consumerKey = simplickrConfig.ApiKey;
            string consumerSecret = simplickrConfig.Secret;

            var oAuthUrlProvider = new OAuthUrlProvider()
                .Url(accessTokenBaseUrl)
                .ConsumerKey(consumerKey)
                .ConsumerSecret(consumerSecret)
                .Token(token)
                .TokenSecret(tokenSecret)
                .Verifier(verifier);

            string accessTokenUrl = oAuthUrlProvider.GetSignedUrl();

            return accessTokenUrl;
        }
    }
}