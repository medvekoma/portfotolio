namespace Portfotolio.Services.Logging
{
    public class NLogLoggerFactory : ILoggerFactory
    {
        public ILogger GetLogger(string name)
        {
            var logger = NLog.LogManager.GetLogger(name);
            return new NLogLogger(logger);
        }
    }
}