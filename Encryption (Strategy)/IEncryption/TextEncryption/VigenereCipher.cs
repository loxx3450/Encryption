using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption__Strategy_.IEncryption
{
    internal class VigenereCipher : IEncryption<string>
    {
        private char[] alphabet;
        private string key;
        private static int keyLength = 5;

        public VigenereCipher()
        {
            alphabet = new char[26];

            for(int index = 0, symbol = 97; symbol <= 122; ++symbol, ++index)
            {
                alphabet[index] = Convert.ToChar(symbol);
            }

            Rand.RandStr rand = new();
            key = rand.Rand(keyLength).ToLower();
        }

        public VigenereCipher(string key)
        {
            alphabet = new char[26];

            for (int index = 0, symbol = 97; symbol <= 122; ++symbol, ++index)
            {
                alphabet[index] = Convert.ToChar(symbol);
            }

            this.key = key;
        }

        public string Encrypt(string input)
        {
            string result = string.Empty;
            string cipher = string.Empty;

            for (int i = 0; i < input.Length; ++i)
            {
                cipher += key[i % 4];
            }

            for (int i = 0; i < input.Length; ++i)
            {
                char symbol;

                if (input[i] >= 'a' && input[i] <= 'z')
                {
                    symbol = alphabet[(cipher[i] - 'a' + input[i] - 'a') % 26];
                }
                else if (input[i] >= 'A' && input[i] <= 'Z')
                {
                    symbol = Convert.ToChar(Convert.ToInt32(input[i]) + 32);
                    symbol = alphabet[(cipher[i] - 'a' + symbol - 'a') % 26];
                    symbol = Convert.ToChar(Convert.ToInt32(symbol) - 32);
                }
                else
                {
                    symbol = input[i];
                }

                result += symbol;
            }

            return result;
        }

        public string Decrypt(string input)
        {
            string result = string.Empty;
            string cipher = string.Empty;

            for (int i = 0; i < input.Length; ++i)
            {
                cipher += key[i % 4];
            }

            int index;

            char symbol;

            for (int i = 0; i < input.Length; ++i)
            {
                if ((input[i] < 'a' || input[i] > 'z') && (input[i] < 'A' || input[i] > 'Z'))
                {
                    result += input[i];
                    continue;
                }

                index = getIndex(i);

                symbol = alphabet[index % 26];

                if (input[i] <= 'Z')
                {
                    symbol = Convert.ToChar(Convert.ToInt32(symbol - 32));
                }

                result += symbol;
            }

            return result;

            int getIndex(int i)
            {
                int index;
                char symbol;

                if (input[i] <= 'Z')
                {
                    symbol = Convert.ToChar(Convert.ToInt32(input[i]) + 32);
                    index = symbol - 'a' - (cipher[i] - 'a');
                }
                else { index = input[i] - 'a' - (cipher[i] - 'a'); }

                return index < 0 ? index + 26 : index;
            }
        }
    }
}
