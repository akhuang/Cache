using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zing.Core
{
    public class Signals : ISignals
    {
        private IDictionary<string, Token> Tokens = new Dictionary<string, Token>();

        public Signals()
        {

        }

        public IToken When(string signal)
        {
            Token token;
            if (!Tokens.TryGetValue(signal, out token))
            {
                token = new Token();
                Tokens.Add(signal, token);
            }
            return token;
        }
    }
}
