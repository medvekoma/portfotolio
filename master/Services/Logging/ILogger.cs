using System;

namespace Portfotolio.Services.Logging
{
    public interface ILogger
    {
        void Info(string message, Exception exception = null);
        void Error(string message, Exception exception = null);
        void Warning(string message, Exception exception = null);
    }
}