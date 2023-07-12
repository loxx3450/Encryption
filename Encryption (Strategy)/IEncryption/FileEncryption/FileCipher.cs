using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption__Strategy_.IEncryption
{
    public class FileCipher : IEncryption<FileInfo>
    {
        private readonly VigenereCipher method;
        private readonly FileInfo output;

        public FileCipher(string key, string outputPath)
        {
            this.method = new VigenereCipher(key.ToLower());

            this.output = new FileInfo(outputPath);
        }

        public FileInfo Encrypt(FileInfo input)
        {
            string text = File.ReadAllText(input.FullName);

            text = method.Encrypt(text);

            File.WriteAllText(output.FullName, text);

            return output;
        }

        public FileInfo Decrypt(FileInfo input)
        {
            string text = File.ReadAllText(input.FullName);

            text = method.Decrypt(text);

            File.WriteAllText(output.FullName, text);

            return output;
        }
    }
}
