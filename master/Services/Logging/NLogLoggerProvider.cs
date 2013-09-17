namespace Portfotolio.Services.Logging
{
    public class NLogLoggerProvider : ILoggerProvider
    {
        public ILogger GetLogger(string name)
        {
            var logger = NLog.LogManager.GetLogger(name);
            return new NLogLogger(logger);
        }
    }
}