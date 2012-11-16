using System.Collections.Generic;

namespace Portfotolio.Domain.Configuration
{
    public interface IOptoutUserService
    {
        HashSet<string> GetOptedOutUserIds();
    }
}