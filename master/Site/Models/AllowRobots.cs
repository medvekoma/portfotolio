using System;

namespace Portfotolio.Site.Models
{
    [Flags]
    public enum AllowRobots
    {
        None = 0,
        Index = 1,
        Follow = 2,
        Archive = 4,
    }
}