using System;
using System.Collections.Generic;
using System.Linq;

namespace Simplickr
{
    public class ParameterMap : Dictionary<string, string>
    {
        public ParameterMap Add<TValue>(string key, TValue value)
        {
            if (!Equals(value, default(TValue)))
                this[key] = value.ToString();

            return this;
        }
    }
}