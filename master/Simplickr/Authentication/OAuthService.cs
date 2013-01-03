namespace Simplickr.Authentication
{
    public interface IOAuthService
    {
        OAuthResponse GetRequestToken(string callbackUrl);
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

        public OAuthResponse GetRequestToken(string callbackUrl)
        {
            var requestTokenUrl = _oAuthUrlService.GetRequestTokenUrl(callbackUrl);
            var response = _httpClient.Get(requestTokenUrl);
            var oAuthResponse = _oAuthResponseProcessor.ProcessResponse(response);

            return oAuthResponse;
        }
    }
}