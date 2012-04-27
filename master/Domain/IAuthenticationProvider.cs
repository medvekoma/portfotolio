namespace Portfotolio.Domain
{
    public interface IAuthenticationProvider
    {
        string GetLoginUrl();
        void Logout();
        AuthenticationInfo Authenticate(object parameter);
        AuthenticationInfo GetAuthenticationInfo();
    }
}