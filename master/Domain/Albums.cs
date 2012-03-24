using System.Collections.Generic;

namespace Portfotolio.Domain
{
    public class Albums
    {
        public IList<Album> Items { get; private set; }

        public Albums(IList<Album> items)
        {
            Items = items;
        }
    }
}