using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Portfotolio.Domain
{
    public class DomainPhotos
    {
        public IList<DomainPhoto> Photos { get; private set; }
        public int Page { get; private set; }
        public int Pages { get; private set; }

        public DomainPhotos(IList<DomainPhoto> photos, int page, int pages)
        {
            Photos = photos;
            Page = page;
            Pages = pages;
        }
    }
}
