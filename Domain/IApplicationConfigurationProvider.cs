﻿namespace Portfotolio.Domain
{
    public sealed class ApplicationConfiguration
    {
        public int PhotoPageSize { get; private set; }
        public int ContactsPageSize { get; private set; }
        public bool IsOAuthEnabled { get; private set; }
        public string[] AdministratorAliases { get; private set; }
        public string[] OmniViewerAliases { get; set; }
        public string ContactUsLink { get; private set; }

        public ApplicationConfiguration(int photoPageSize, int contactsPageSize, bool isOAuthEnabled, string[] administratorAliases, string[] omniViewerAliases, string contactUsLink)
        {
            PhotoPageSize = photoPageSize;
            ContactsPageSize = contactsPageSize;
            IsOAuthEnabled = isOAuthEnabled;
            AdministratorAliases = administratorAliases;
            OmniViewerAliases = omniViewerAliases;
            ContactUsLink = contactUsLink;
        }
    }

    public interface IApplicationConfigurationProvider
    {
        ApplicationConfiguration GetApplicationConfiguration();
    }
}
