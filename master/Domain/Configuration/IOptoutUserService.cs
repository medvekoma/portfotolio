using System.Collections.Generic;

namespace Portfotolio.Domain.Configuration
{
    public interface IOptoutUserService
    {
        SortedSet<string> GetOptedOutUserIds();
    }
}