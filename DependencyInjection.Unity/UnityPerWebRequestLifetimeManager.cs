using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.Practices.Unity;

namespace Portfotolio.DependencyInjection.Unity
{
    public sealed class UnityPerWebRequestLifetimeManager : LifetimeManager
    {
        private HttpContextBase _httpContext;

        //Need this constructor for Unit Test
        public UnityPerWebRequestLifetimeManager(HttpContextBase httpContext)
        {
            _httpContext = httpContext;
        }

        public UnityPerWebRequestLifetimeManager()
            : this(new HttpContextWrapper(HttpContext.Current))
        {
        }

        private IDictionary<UnityPerWebRequestLifetimeManager, object> BackingStore
        {
            get
            {
                _httpContext = (HttpContext.Current != null) ? new HttpContextWrapper(HttpContext.Current) : _httpContext;

                return UnityPerWebRequestLifetimeModule.GetInstances(_httpContext);
            }
        }

        private object Value
        {
            get
            {
                IDictionary<UnityPerWebRequestLifetimeManager, object> backingStore = BackingStore;

                return backingStore.ContainsKey(this) ? backingStore[this] : null;
            }
            set
            {
                IDictionary<UnityPerWebRequestLifetimeManager, object> backingStore = BackingStore;

                if (backingStore.ContainsKey(this))
                {
                    object oldValue = backingStore[this];

                    if (!ReferenceEquals(value, oldValue))
                    {
                        var disposable = oldValue as IDisposable;

                        if (disposable != null)
                        {
                            disposable.Dispose();
                        }

                        if (value == null)
                        {
                            backingStore.Remove(this);
                        }
                        else
                        {
                            backingStore[this] = value;
                        }
                    }
                }
                else
                {
                    if (value != null)
                    {
                        backingStore.Add(this, value);
                    }
                }
            }
        }

        public override object GetValue()
        {
            return Value;
        }

        public override void SetValue(object newValue)
        {
            Value = newValue;
        }

        public override void RemoveValue()
        {
            Value = null;
        }
    }
}