using System;

namespace Portfotolio.Site.Services.Models
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