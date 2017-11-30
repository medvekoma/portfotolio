using System;
using NLog;

namespace Portfotolio.Services.Logging
{
    public class NLogLogger : ILogger
    {
        private readonly Logger _logger;

        public NLogLogger(Logger logger)
        {
            _logger = logger;
        }

        public void Info(string message, Exception exception = null)
        {
            _logger.Info(exception, message);
        }

        public void Error(string message, Exception exception = null)
        {
            _logger.Error(exception, message);
        }

        public void Warning(string message, Exception exception = null)
        {
            _logger.Warn(exception, message);
        }
    }
}