using System;
using Portfotolio.Domain.Exceptions;
using Portfotolio.Services.Logging;

namespace Portfotolio.Site.Services.Logging
{
    public static class LoggingExtensions
    {
        public static void LogException(this ILogger logger, Exception exception)
        {
            if (ShouldLogAsWarning(exception))
            {
                logger.Warning(GetMessage(exception), exception);
            }
            else
            {
                logger.Error(GetMessage(exception), exception);    
            }
        }

        public static void LogException(this NLog.Logger logger, Exception exception)
        {
            if (ShouldLogAsWarning(exception))
            {
                logger.WarnException(GetMessage(exception), exception);
            }
            else
            {
                logger.ErrorException(GetMessage(exception), exception);
            }
        }

        private static string GetMessage(Exception exception)
        {
            return exception != null
                       ? exception.Message
                       : "(no exception)";
        }

        private static bool ShouldLogAsWarning(Exception exception)
        {
            var portfotolioException = exception as PortfotolioException;
            var shouldLogAsWarning = portfotolioException != null && portfotolioException.IsWarning;
            return shouldLogAsWarning;
        }
    }
}