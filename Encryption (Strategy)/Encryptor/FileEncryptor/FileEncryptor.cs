using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption__Strategy_.Encryptor.FileEncryptor
{
    using Encryption__Strategy_.IEncryption;

    internal class FileEncryptor : Encryptor<FileInfo>
    {
        public FileEncryptor(string key, string outputPath)
            : base(new FileCipher(key, outputPath))
        {}
    }
}
