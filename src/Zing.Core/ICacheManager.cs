using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zing.Core
{
    public interface ICacheManager
    {
        TResult Get<TKey, TResult>(TKey key, Func<TResult> func);
    }
}
