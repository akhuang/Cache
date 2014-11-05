using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zing.Core
{
    public class Signals : ISignals
    {
        readonly IDictionary<object, Token> _tokens = new Dictionary<object, Token>();

        public IToken When(string signal)
        {
            Token token;
            if (!_tokens.TryGetValue(signal, out token))
            {
                token = new Token();
                _tokens.Add(signal, token);
            }
            return token;
        }

        public void Trigger(string signal)
        {
            Token token;
            if (_tokens.TryGetValue(signal, out token))
            {
                _tokens.Remove(signal);
                token.Trigger();
            }
        }
    }
}
