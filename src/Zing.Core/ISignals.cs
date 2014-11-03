using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zing.Core
{
    public interface ISignals
    {
        IToken When(string signal);
        void Trigger(string signal);
    }
}
