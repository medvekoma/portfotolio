using System.Collections.Generic;

namespace Portfotolio.Site.Models
{
    public class ArrayModel
    {
        public ArrayModel(IEnumerable<string> stringArray, string stringValue)
        {
            StringArray = stringArray;
            StringValue = stringValue;
        }

        public IEnumerable<string> StringArray { get; private set; }
        public string StringValue { get; private set; }
    }
}