namespace Simplickr.Authentication
{
    public interface IOAuthService
    {
        OAuthRequestToken GetRequestToken(string callbackUrl);
        string GetAccessToken(string token, string tokenSecret, string verifier);
    }

    public class OAuthService : IOAuthService
    {
        private readonly IOAuthUrlService _oAuthUrlService;
        private readonly IHttpClient _httpClient;
        private readonly IOAuthResponseProcessor _oAuthResponseProcessor;

        public OAuthService(IOAuthUrlService oAuthUrlService, IHttpClient httpClient, IOAuthResponseProcessor oAuthResponseProcessor)
        {
            _oAuthUrlService = oAuthUrlService;
            _httpClient = httpClient;
            _oAuthResponseProcessor = oAuthResponseProcessor;
        }

        public OAuthRequestToken GetRequestToken(string callbackUrl)
        {
            var requestTokenUrl = _oAuthUrlService.GetRequestTokenUrl(callbackUrl);
            var response = _httpClient.Get(requestTokenUrl);
            var requestToken = _oAuthResponseProcessor.ProcessResponse(response);

            return requestToken;
        }

        public string GetAccessToken(string token, string tokenSecret, string verifier)
        {
            string accessTokenUrl = _oAuthUrlService.GetAccessTokenUrl(token, tokenSecret, verifier);
            string accessTokenString = _httpClient.Get(accessTokenUrl);
            return accessTokenString;
            // _oAuthResponseProcessor.GetAccessToken(accessTokenString);
        }
    }
}