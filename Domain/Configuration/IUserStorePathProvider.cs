namespace Portfotolio.Domain.Configuration
{
    public interface IUserStorePathProvider
    {
        string GetOptoutFileName();
        string GetOptinFileName();
    }
}