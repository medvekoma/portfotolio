using System.Collections.Generic;

namespace Portfotolio.Domain.Configuration
{
    public interface IUserService
    {
        HashSet<string> GetOptoutUserIds();
        HashSet<string> GetOptinUserIds();
    }
}