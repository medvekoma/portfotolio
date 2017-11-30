namespace Portfotolio.Domain
{
    public interface IOAuthProvider
    {
        OAuthAuthorizationObject GetAuthorizationObject(string callbackUrl);
        void Logout();
        AuthenticationInfo Authenticate(string oauthToken, string oauthTokenSecret, string verifier);
        AuthenticationInfo GetAuthenticationInfo();
    }
}