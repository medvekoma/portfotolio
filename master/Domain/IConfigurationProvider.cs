namespace Portfotolio.Domain
{
    public interface IConfigurationProvider
    {
        int GetPhotoPageSize();
        int GetContactsPageSize();
        bool GetIsOAuthEnabled();
    }
}
