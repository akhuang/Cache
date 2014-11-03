using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zing.Core
{
    public class DefaultCacheManager : ICacheManager
    {
        public T Get<T>(Func<T> func)
        {
            throw new NotImplementedException();
        }
    }
}
