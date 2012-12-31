using System;
using System.Collections.Generic;
using System.Linq;

namespace Simplickr
{
    public class ParameterMap
    {
        private readonly IDictionary<string, string> _dictionary = new Dictionary<string, string>();
 
        public ParameterMap Add<TValue>(string key, TValue value)
        {
            if (!Equals(value, default(TValue)))
                _dictionary[key] = value.ToString();

            return this;
        }

        public string GetQueryString()
        {
            var items = _dictionary.Select(item => item.Key + '=' + item.Value).ToArray();
            return string.Join("&", items);
        }
    }
}