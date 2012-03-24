using System;
using NLog;
using Portfotolio.Domain.Exceptions;

namespace Portfotolio.Site.Helpers
{
    public static class LoggingExtensions
    {
        public static void LogException(this Logger logger, Exception ex)
        {
            if (ex is OptedOutUserException)
            {
                logger.DebugException(ex.Message, ex);
                return;
            }

            if (ex is PortfotolioException)
            {
                logger.InfoException(ex.Message, ex);
                return;
            }
            
            logger.ErrorException(ex.Message, ex);
        }
    }
}