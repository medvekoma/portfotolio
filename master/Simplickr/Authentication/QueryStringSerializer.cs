using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Simplickr.Authentication
{
    public interface IQueryStringSerializer
    {
        T Deserialize<T>(string queryString)
            where T: new();
    }

    public class QueryStringSerializer : IQueryStringSerializer
    {
        public T Deserialize<T>(string queryString)
            where T: new()
        {
            var result = new T();

            var dictionary = queryString
                .Split('&')
                .Select(item => item.Split('='))
                .Where(item => item.Count() == 2)
                .ToDictionary(item => item[0].Replace("_", ""), item => HttpUtility.UrlDecode(item[1]));

            var objectType = typeof(T);
            foreach (var item in dictionary)
            {
                var propertyInfo = objectType.GetProperty(item.Key, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (propertyInfo != null)
                {
                    var value = Parse(item.Value, propertyInfo.PropertyType);
                    propertyInfo.SetValue(result, value, null);
                }
            }

            return result;
        }

        private static object Parse(string value, Type type)
        {
            if (type == typeof (string))
                return value;
            if (type == typeof (bool))
                return bool.Parse(value);
            if (type == typeof (int))
                return int.Parse(value, CultureInfo.InvariantCulture);

            return Convert.ChangeType(value, type);
        }
    }
}