namespace Simplickr.Authentication
{
    public interface IOAuthService
    {
        OAuthRequestToken GetRequestToken(string callbackUrl);
        OAuthAccessToken GetAccessToken(string token, string tokenSecret, string verifier);
    }

    public class OAuthService : IOAuthService
    {
        private readonly IOAuthUrlService _oAuthUrlService;
        private readonly IHttpClient _httpClient;
        private readonly IQueryStringSerializer _queryStringSerializer;

        public OAuthService(IOAuthUrlService oAuthUrlService, IHttpClient httpClient, IQueryStringSerializer queryStringSerializer)
        {
            _oAuthUrlService = oAuthUrlService;
            _httpClient = httpClient;
            _queryStringSerializer = queryStringSerializer;
        }

        public OAuthRequestToken GetRequestToken(string callbackUrl)
        {
            var requestTokenUrl = _oAuthUrlService.GetRequestTokenUrl(callbackUrl);
            var response = _httpClient.Get(requestTokenUrl);
            var requestToken = _queryStringSerializer.Deserialize<OAuthRequestToken>(response);

            return requestToken;
        }

        public OAuthAccessToken GetAccessToken(string token, string tokenSecret, string verifier)
        {
            string accessTokenUrl = _oAuthUrlService.GetAccessTokenUrl(token, tokenSecret, verifier);
            string accessTokenString = _httpClient.Get(accessTokenUrl);
            return _queryStringSerializer.Deserialize<OAuthAccessToken>(accessTokenString);
        }
    }
}