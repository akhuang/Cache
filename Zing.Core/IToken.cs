using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zing.Core
{
    public interface IToken
    {
        bool IsCurrent { get; set; }
    }
}
