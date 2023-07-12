using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption__Strategy_.Encryptor
{
    using Encryption__Strategy_.IEncryption;

    abstract class Encryptor<T>
    {
        protected IEncryption<T> strategy;

        public Encryptor(IEncryption<T> strategy)
        {
            this.strategy = strategy;   
        }

        public T Encrypt(T input)
        {
            return strategy.Encrypt(input);
        }

        public T Decrypt(T input)
        {
            return strategy.Decrypt(input);
        }
    }
}
