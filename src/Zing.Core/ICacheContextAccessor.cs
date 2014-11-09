using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zing.Core
{
    public interface ICacheContextAccessor
    {
        IAcquireContext Current { get; set; }
    }

    public class DefaultCacheContextAccessor : ICacheContextAccessor
    {
        [ThreadStatic]
        private static IAcquireContext _threadInstance;

        public static IAcquireContext ThreadInstance
        {
            get { return _threadInstance; }
            set { _threadInstance = value; }
        }

        public IAcquireContext Current
        {
            get { return ThreadInstance; }
            set { ThreadInstance = value; }
        }
    }
}
