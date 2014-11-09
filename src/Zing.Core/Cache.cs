using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zing.Core
{
    public class Cache<TKey, TResult> : ICache<TKey, TResult>
    {
        private readonly ICacheContextAccessor _cacheContextAccessor;
        private readonly ConcurrentDictionary<TKey, CacheEntry> _entries;

        public Cache(ICacheContextAccessor cacheContextAccessor)
        {
            _cacheContextAccessor = cacheContextAccessor;
            _entries = new ConcurrentDictionary<TKey, CacheEntry>();
        }

        public TResult Get(TKey key, Func<AcquireContext<TKey>, TResult> acquire)
        {
            var entry = _entries.AddOrUpdate(key,
                k => AddEntry(k, acquire),
                (k, currentEntry) => UpdateEntry(currentEntry, key, acquire));

            return entry.Result;
        }
        private CacheEntry AddEntry(TKey k, Func<AcquireContext<TKey>, TResult> acquire)
        {
            var entry = CreateEntry(k, acquire);
            PropagateTokens(entry);
            return entry;
        }

        private CacheEntry UpdateEntry(CacheEntry currentEntry, TKey k, Func<AcquireContext<TKey>, TResult> acquire)
        {
            var entry = (currentEntry.Tokens.Any(t => t != null && !t.IsCurrent)) ? CreateEntry(k, acquire) : currentEntry;
            PropagateTokens(entry);
            return entry;
        }

        private void PropagateTokens(CacheEntry entry)
        {
            // Bubble up volatile tokens to parent context
            if (_cacheContextAccessor.Current != null)
            {
                foreach (var token in entry.Tokens)
                    _cacheContextAccessor.Current.Monitor(token);
            }
        }


        private CacheEntry CreateEntry(TKey k, Func<AcquireContext<TKey>, TResult> acquire)
        {
            var entry = new CacheEntry();
            var context = new AcquireContext<TKey>(k, entry.AddToken);

            IAcquireContext parentContext = null;
            try
            {
                // Push context
                parentContext = _cacheContextAccessor.Current;
                _cacheContextAccessor.Current = context;

                entry.Result = acquire(context);
            }
            finally
            {
                // Pop context
                _cacheContextAccessor.Current = parentContext;
            }
            entry.CompactTokens();
            return entry;
        }

        class CacheEntry
        {
            private IList<IToken> _tokens;
            public TResult Result { get; set; }

            public IEnumerable<IToken> Tokens
            {
                get
                {
                    return _tokens ?? Enumerable.Empty<IToken>();
                }
            }

            public void AddToken(IToken token)
            {
                if (_tokens == null)
                {
                    _tokens = new List<IToken>();
                }

                _tokens.Add(token);
            }
            public void CompactTokens()
            {
                if (_tokens != null)
                {
                    _tokens = _tokens.Distinct().ToArray();
                }
            }
        }
    }
}
