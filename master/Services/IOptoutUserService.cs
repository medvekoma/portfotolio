using System.Collections.Generic;

namespace Services
{
    public interface IOptoutUserService
    {
        SortedSet<string> GetOptedOutUserIds();
    }
}