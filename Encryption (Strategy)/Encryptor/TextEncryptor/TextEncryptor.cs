using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption__Strategy_.Encryptor.TextEncryptor
{
    using Encryption__Strategy_.IEncryption;

    internal class TextEncryptor : Encryptor<string>
    {
        public TextEncryptor(IEncryption<string> strategy)
            : base(strategy)
        { }

        public void SetStrategy(IEncryption<string> strategy)
        {
            this.strategy = strategy;
        }
    }
    
}
