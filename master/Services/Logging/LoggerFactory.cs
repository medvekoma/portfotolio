namespace Portfotolio.Services.Logging
{
    public class LoggerFactory : ILoggerFactory
    {
        public ILogger GetLogger(string name)
        {
            return new NLogLoggerFactory().GetLogger(name);
        }
    }
}