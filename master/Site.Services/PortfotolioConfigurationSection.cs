using System;
using System.Collections.Generic;
using System.Configuration;
using Portfotolio.Utility.Extensions;
using System.Linq;
using System.Text;

namespace Portfotolio.Site.Services
{
    public class PortfotolioConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("photoPageSize", DefaultValue = 30, IsRequired = false)]
        public int PhotoPageSize
        {
            get { return this["photoPageSize"].ConvertTo<int>(); }
            set { this["photoPageSize"] = value; }
        }
    }
}
