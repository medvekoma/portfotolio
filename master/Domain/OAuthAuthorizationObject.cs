namespace Portfotolio.Domain
{
    public class OAuthAuthorizationObject
    {
        public string TokenSecret { get; private set; }
        public string AuthorizationUrl { get; private set; }

        public OAuthAuthorizationObject(string tokenSecret, string authorizationUrl)
        {
            TokenSecret = tokenSecret;
            AuthorizationUrl = authorizationUrl;
        }
    }
}