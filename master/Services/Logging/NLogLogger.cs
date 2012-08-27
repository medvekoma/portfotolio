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
            _logger.InfoException(message, exception);
        }

        public void Error(string message, Exception exception = null)
        {
            _logger.ErrorException(message, exception);
        }

        public void Warning(string message, Exception exception = null)
        {
            _logger.WarnException(message, exception);
        }
    }
}