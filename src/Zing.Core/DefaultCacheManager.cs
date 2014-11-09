using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zing.Core
{
    public class DefaultCacheManager : ICacheManager
    {
        private readonly ConcurrentDictionary<string, Object> _entries;

        public DefaultCacheManager()
        {
            _entries = new ConcurrentDictionary<string, object>();
        }

        public TResult Get<TKey, TResult>(TKey key, Func<TResult> func)
        {
            var model = func();
            _entries.GetOrAdd(key, model);
            return model;
        }
    }
}
