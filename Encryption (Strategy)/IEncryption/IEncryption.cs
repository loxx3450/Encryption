using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption__Strategy_.IEncryption
{
    internal interface IEncryption<T>
    {
        T Encrypt(T input);

        T Decrypt(T input);
    }
}
