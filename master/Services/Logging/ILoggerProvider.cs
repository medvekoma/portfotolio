namespace Portfotolio.Services.Logging
{
    public interface ILoggerProvider
    {
        ILogger GetLogger(string name);
    }
}