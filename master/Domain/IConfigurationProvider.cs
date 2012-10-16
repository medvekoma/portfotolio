﻿namespace Portfotolio.Domain
{
    public interface IConfigurationProvider
    {
        int GetPhotoPageSize();
        int GetContactsPageSize();
        string GetDefaultUserAlias();
        bool GetIsOAuthEnabled();
    }
}
