using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption__Strategy_.IEncryption
{
    internal class CaesarСipher : IEncryption<string>
    {
        private int shift;

        public CaesarСipher()
        {
            shift = Convert.ToInt32(DateTimeOffset.Now.ToUnixTimeSeconds() % 26);
        }

        public string Encrypt(string input)
        {
            string result = string.Empty;
            char symbol;

            foreach (char c in input)
            {
                if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                {
                    symbol = Convert.ToChar(Convert.ToInt32(c) + shift);

                    if ((c <= 'Z' && symbol > 'Z') || (c >= 'a' && symbol > 'z'))
                    {
                        symbol = Convert.ToChar(Convert.ToInt32(symbol) - 26);
                    }
                }
                else
                {
                    symbol = c;
                }

                result += symbol;
            }

            return result;
        }

        public string Decrypt(string input)
        {
            string result = string.Empty;
            char symbol;

            foreach (char c in input)
            {
                if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                {
                    symbol = Convert.ToChar(Convert.ToInt32(c) - shift);

                    if ((c <= 'Z' && symbol < 'A') || (c >= 'a' && symbol < 'a'))
                    {
                        symbol = Convert.ToChar(Convert.ToInt32(symbol) + 26);
                    }
                }
                else
                {
                    symbol = c;
                }

                result += symbol;
            }

            return result;
        }
    }
}
