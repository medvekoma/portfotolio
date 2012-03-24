using System;
using NLog;
using Portfotolio.Domain.Exceptions;

namespace Portfotolio.Site.Helpers
{
    public static class LoggingExtensions
    {
        public static void LogException(this Logger logger, Exception ex)
        {
            var portfotolioException = ex as PortfotolioException;
            var logLevel = portfotolioException != null && portfotolioException.IsWarning
                               ? LogLevel.Debug
                               : LogLevel.Error;

            logger.LogException(logLevel, ex.Message, ex);
        }
    }
}