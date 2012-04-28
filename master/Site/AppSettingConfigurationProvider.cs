using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using Portfotolio.Domain;

namespace Portfotolio.Site
{
    public class AppSettingConfigurationProvider : IConfigurationProvider
    {
        public int GetPhotoPageSize()
        {
            string photoPageSizeString = ConfigurationManager.AppSettings["PhotoPageSize"];
            return ConvertToInt(photoPageSizeString, 30);
        }

        public int GetContactsPageSize()
        {
            string contactsPageSizeString = ConfigurationManager.AppSettings["ContactsPageSize"];
            return ConvertToInt(contactsPageSizeString, 100);
        }

        public string GetDefaultUserAlias()
        {
            return ConfigurationManager.AppSettings["DefaultUserAlias"];
        }

        private string[] _optedOutUserIds = null;

        public string[] GetOptedOutUserIds()
        {
            if (_optedOutUserIds == null)
            {
                var value = ConfigurationManager.AppSettings["OptedOutUserIds"];
                if (value == null)
                    return new string[0];

                _optedOutUserIds = value.Split(',');
            }
            return _optedOutUserIds;
        }

        public bool GetIsOAuthEnabled()
        {
            var value = ConfigurationManager.AppSettings["IsOAuthEnabled"];
            return ConvertToBool(value);
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
            var trueValues = new[] {"true", "1"};
            bool isTrue = trueValues.Any(item => item.Equals(stringValue, StringComparison.InvariantCultureIgnoreCase));
            return isTrue;
        }
    }
}
