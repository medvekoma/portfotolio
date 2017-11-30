using System;

namespace Portfotolio.Site.Services.Models
{
    public class TestModel
    {
        public object StartedOn { get; set; }
        public object SessionCount { get; set; }
        public long ElementsInCache { get; set; }
        public long GcTotalMemory { get; set; }
        public long WorkingSet { get; set; }
        public long PrivateMemory { get; set; }
    }
}