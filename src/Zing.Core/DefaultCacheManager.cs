using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zing.Core
{
    public class DefaultCacheManager : ICacheManager
    {
        private readonly Type _component;
        private readonly ICacheHolder _cacheHolder;

        public DefaultCacheManager(Type component, ICacheHolder cacheHolder)
        {
            _component = component;
            _cacheHolder = cacheHolder;
        }

        public TResult Get<TKey, TResult>(TKey key, Func<AcquireContext<TKey>, TResult> acquire)
        {
            return _cacheHolder.GetCache<TKey, TResult>(_component).Get(key, acquire);
        }

    }
}
