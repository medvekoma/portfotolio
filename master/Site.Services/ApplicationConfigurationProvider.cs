using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using Portfotolio.Domain;

namespace Portfotolio.Site.Services
{
    public class ApplicationConfigurationProvider : IApplicationConfigurationProvider
    {
        public ApplicationConfiguration GetApplicationConfiguration()
        {
            return new ApplicationConfiguration(
                GetPhotoPageSize(),
                GetContactsPageSize(),
                GetIsOAuthEnabled(),
                GetAdministratorAliases(),
                GetContactUsLink()
                );
        }

        private static int GetPhotoPageSize()
        {
            string photoPageSizeString = ConfigurationManager.AppSettings["PhotoPageSize"];
            return ConvertToInt(photoPageSizeString, 30);
        }

        private static int GetContactsPageSize()
        {
            string contactsPageSizeString = ConfigurationManager.AppSettings["ContactsPageSize"];
            return ConvertToInt(contactsPageSizeString, 100);
        }

        private static bool GetIsOAuthEnabled()
        {
            var value = ConfigurationManager.AppSettings["IsOAuthEnabled"];
            return ConvertToBool(value);
        }

        private static string[] GetAdministratorAliases()
        {
            var value = ConfigurationManager.AppSettings["AdministratorAliases"];
            return value.Split(',');
        }

        private static string GetContactUsLink()
        {
            return ConfigurationManager.AppSettings["ContactUsLink"];
        }

        private static int ConvertToInt(string stringValue, int defaultValue)
        {
            int intValue;
            if (!Int32.TryParse(stringValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out intValue))
                return defaultValue;
            return intValue;
        }

        private static bool ConvertToBool(string stringValue)
        {
            var trueValues = new[] { "true", "1" };
            bool isTrue = trueValues.Any(item => item.Equals(stringValue, StringComparison.InvariantCultureIgnoreCase));
            return isTrue;
        }
    }
}
