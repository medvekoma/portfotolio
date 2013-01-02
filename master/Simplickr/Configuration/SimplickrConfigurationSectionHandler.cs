using System;
using System.Configuration;
using System.Xml;

namespace Simplickr.Configuration
{
    public class SimplickrConfigurationSectionHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            if (section == null || section.Attributes == null)
                throw new ArgumentNullException("section");

            var apiKeyAttribute = section.Attributes["apiKey"];
            var secretAttribute = section.Attributes["secret"];

            if (apiKeyAttribute == null)
                throw new ConfigurationErrorsException("apiKey missing");

            if (secretAttribute == null)
                throw new ConfigurationErrorsException("secret missing");

            return new SimplickrConfig(apiKeyAttribute.Value, secretAttribute.Value);
        }
    }
}