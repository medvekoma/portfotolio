using System;
using Portfotolio.Domain.Exceptions;
using Portfotolio.Services.Logging;

namespace Portfotolio.Site.Helpers
{
    public static class LoggingExtensions
    {
        public static void LogException(this ILogger logger, Exception exception)
        {
            var portfotolioException = exception as PortfotolioException;
            if (portfotolioException != null && portfotolioException.IsWarning)
            {
                logger.Warning(exception.Message, exception);
            }
            else
            {
                logger.Error(exception.Message, exception);    
            }
        }
    }
}