using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zing.Core
{
    public interface IAcquireContext
    {
        Action<IToken> Monitor { get; }
    }
    public class AcquireContext<TKey> : IAcquireContext
    {
        public AcquireContext(TKey key, Action<IToken> monitor)
        {
            Key = key;
            Monitor = monitor;
        }
        public TKey Key
        {
            get;
            private set;
        }
        public Action<IToken> Monitor
        {
            get;
            private set;
        }
    }
}
