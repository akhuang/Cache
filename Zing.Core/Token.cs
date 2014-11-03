using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zing.Core
{
    public class Token : IToken
    {
        public IDictionary<string, IToken> Tokens;
        public Token()
        {
            IsCurrent = true;
            Tokens = new Dictionary<string, IToken>();
        }



        public bool IsCurrent
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Trigger()
        {
            IsCurrent = false;
        }
    }
}
