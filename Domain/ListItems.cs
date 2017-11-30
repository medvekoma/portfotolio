using System.Collections.Generic;

namespace Portfotolio.Domain
{
    public class ListItems
    {
        public IEnumerable<ListItem> Items { get; private set; }
        public int Page { get; private set; }
        public int Pages { get; private set; }

        public ListItems(IEnumerable<ListItem> items, int page, int pages)
        {
            Items = items;
            Page = page;
            Pages = pages;
        }
    }
}