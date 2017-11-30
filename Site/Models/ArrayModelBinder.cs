using System.Collections.Generic;
using System.Web.Mvc;

namespace Portfotolio.Site.Models
{
    public class ArrayModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult arrayValueResult = bindingContext.ValueProvider.GetValue("arrayValue[]");
            ValueProviderResult stringValueResult = bindingContext.ValueProvider.GetValue("stringValue");

            var stringArray = (IEnumerable<string>) arrayValueResult.RawValue;
            var stringValue = stringValueResult.AttemptedValue;

            return new ArrayModel(stringArray, stringValue);
        }
    }
}