using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zing.Core
{
    public class Token : IToken
    {
        public Token()
        {
            IsCurrent = true;
        }

        public bool IsCurrent
        {
            get;
            private set;
        }

        public void Trigger()
        {
            IsCurrent = false;
        }
    }
}
