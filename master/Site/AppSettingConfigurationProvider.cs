using System;
using System.Configuration;
using System.Globalization;
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

        private static int ConvertToInt(string stringValue, int defaultValue)
        {
            int intValue;
            if (!Int32.TryParse(stringValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out intValue))
                return defaultValue;
            return intValue;
        }
    }
}