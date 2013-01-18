using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Simplickr
{
    public class ParameterMap : IEnumerable<KeyValuePair<string,string>>
    {
        private readonly SortedDictionary<string, string> _dictionary = new SortedDictionary<string, string>();

        public ParameterMap Set<TValue>(string key, TValue value)
        {
            _dictionary[key] = value.ToString();
            return this;
        }

        public string Get(string key)
        {
            return _dictionary.ContainsKey(key)
                       ? _dictionary[key]
                       : null;
        }

        public string GetQueryString()
        {
            var items = _dictionary.Select(item => item.Key + '=' + item.Value).ToArray();
            return string.Join("&", items);
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}