using NLog;

namespace Portfotolio.Services.Logging
{
    public class NLogLoggerFactory : ILoggerFactory
    {
        public ILogger GetLogger(string name)
        {
            var logger = LogManager.GetLogger(name);
            return new NLogLogger(logger);
        }
    }
}