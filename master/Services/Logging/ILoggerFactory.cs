namespace Portfotolio.Services.Logging
{
    public interface ILoggerFactory
    {
        ILogger GetLogger(string name);
    }
}