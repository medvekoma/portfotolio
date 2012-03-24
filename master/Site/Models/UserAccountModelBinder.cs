using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;

namespace Portfotolio.Site.Models
{
    public class UserAccountModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            IValueProvider valueProvider = bindingContext.ValueProvider;
            ValueProviderResult accountTypeResult = valueProvider.GetValue("AccountType");
            ValueProviderResult accountNameResult = valueProvider.GetValue("AccountName");
            string accountName = (string) accountNameResult.ConvertTo(typeof (string));
            var accountType = (AccountType) accountTypeResult.ConvertTo(typeof (AccountType));
            switch (accountType)
            {
                case AccountType.Flickr:
                    return new FlickrAccount(accountName);
                case AccountType.Picasa:
                    return new PicasaAccount(accountName);
                case AccountType.SmugMug:
                    return new SmugMugAccount(accountName);
                default:
                    return null;
            }
        }
    }
}