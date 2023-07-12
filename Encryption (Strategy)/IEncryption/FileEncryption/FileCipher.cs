using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption__Strategy_.IEncryption
{
    public class FileCipher : IEncryption<FileInfo>
    {
        private string key;
        private VigenereCipher method;
        private FileInfo output;

        public FileCipher(string key, string outputPath)
        {
            this.key = key;

            method = new VigenereCipher(key.ToLower());

            output = new FileInfo(outputPath);
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
