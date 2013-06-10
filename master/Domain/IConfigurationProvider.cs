namespace Portfotolio.Domain
{
    public interface IConfigurationProvider
    {
        int GetPhotoPageSize();
        int GetContactsPageSize();
        bool GetIsOAuthEnabled();
        string[] GetAdministratorAliases();
        string GetContactUsLink();
    }
}
