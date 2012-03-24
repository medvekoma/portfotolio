namespace Portfotolio.Domain
{
    public interface IUserEngine
    {
        DomainUser GetUser(string userIdentifier);
    }
}