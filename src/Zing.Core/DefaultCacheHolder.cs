using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zing.Core
{
    public class DefaultCacheHolder : ICacheHolder
    {
        private ICacheContextAccessor _cacheContextAccessor;
        private readonly ConcurrentDictionary<CacheKey, object> _caches = new ConcurrentDictionary<CacheKey, object>();
        public DefaultCacheHolder(ICacheContextAccessor cacheContextAccessor)
        {
            _cacheContextAccessor = cacheContextAccessor;
        }

        class CacheKey : Tuple<Type, Type, Type>
        {
            public CacheKey(Type component, Type key, Type result)
                : base(component, key, result)
            {

            }
        }

        public ICache<TKey, TResult> GetCache<TKey, TResult>(Type component)
        {
            CacheKey cacheKey = new CacheKey(component, typeof(TKey), typeof(TResult));

            return (ICache<TKey, TResult>)_caches.GetOrAdd(cacheKey, k => new Cache<TKey, TResult>(_cacheContextAccessor));
        }
    }
}
