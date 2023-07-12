using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption__Strategy_.IEncryption
{
    internal class CaesarСipher : IEncryption<string>
    {
        private readonly int shift;

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
                if (IsLetter(c))
                {
                    symbol = Convert.ToChar(Convert.ToInt32(c) + shift);

                    if ((IsUpper(c) && !IsUpper(symbol)) || (!IsUpper(c) && !IsLetter(symbol)))
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

            bool IsUpper(char c)
            {
                return c >= 'A' && c <= 'Z';
            }

            bool IsLetter(char c)
            {
                return IsUpper(c) || (c >= 'a' && c <= 'z');
            }
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
