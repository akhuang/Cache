using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zing.Core
{
    public interface ICacheManager
    {
        T Get<T>(Func<T> func);
    }
}
