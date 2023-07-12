using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption__Strategy_.IEncryption
{
    internal class VigenereCipher : IEncryption<string>
    {
        private readonly char[] alphabet;
        private readonly string key;
        private static readonly int keyLength = 5;
        private static readonly int alphabetLength = 26;

        public VigenereCipher()
        {
            alphabet = new char[alphabetLength];

            for(int index = 0, symbol = 97; symbol <= 122; ++symbol, ++index)
            {
                alphabet[index] = Convert.ToChar(symbol);
            }

            Rand.RandStr rand = new();
            this.key = rand.Rand(keyLength).ToLower();
        }

        public VigenereCipher(string key)
        {
            alphabet = new char[alphabetLength];

            for (int index = 0, symbol = 97; symbol <= 122; ++symbol, ++index)
            {
                alphabet[index] = Convert.ToChar(symbol);
            }

            this.key = key;
        }

        public string Encrypt(string input)
        {
            string result = string.Empty;
            char cipher, symbol;

            for (int i = 0; i < input.Length; ++i)
            {
                cipher = key[i % keyLength];

                if (IsLower(input[i]))
                {
                    symbol = alphabet[(cipher - 'a' + input[i] - 'a') % alphabetLength];
                }
                else if (IsUpper(input[i]))
                {
                    symbol = Convert.ToChar(Convert.ToInt32(input[i]) + 32);
                    symbol = alphabet[(cipher - 'a' + symbol - 'a') % alphabetLength];
                    symbol = Convert.ToChar(Convert.ToInt32(symbol) - 32);
                }
                else
                {
                    symbol = input[i];
                }

                result += symbol;
            }

            return result;

            bool IsUpper(char c)
            {
                return c >= 'A' && c <= 'Z';
            }

            bool IsLower(char c)
            {
                return c >= 'a' && c <= 'z';
            }
        }

        public string Decrypt(string input)
        {
            string result = string.Empty;
            char cipher, symbol;

            for (int i = 0; i < input.Length; ++i)
            {
                cipher = key[i % keyLength];

                if (!IsLetter(input[i]))
                {
                    result += input[i];
                    continue;
                }

                symbol = alphabet[getIndex(i) % 26];

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

                if (input[i] <= 'Z')
                {
                    symbol = Convert.ToChar(Convert.ToInt32(input[i]) + 32);
                    index = symbol - 'a' - (cipher - 'a');
                }
                else { index = input[i] - 'a' - (cipher - 'a'); }

                return index < 0 ? index + 26 : index;
            }

            bool IsLetter(char c)
            {
                return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
            }
        }
    }
}
