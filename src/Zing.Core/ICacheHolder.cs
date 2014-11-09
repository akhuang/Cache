using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zing.Core
{
    public interface ICacheHolder
    {
        ICache<TKey, TResult> GetCache<TKey, TResult>(Type component);
    }
}
