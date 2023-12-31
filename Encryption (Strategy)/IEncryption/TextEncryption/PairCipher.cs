﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption__Strategy_.IEncryption
{
    internal class PairCipher : IEncryption<string>
    {
        private readonly char[] alphabet1;
        private readonly char[] alphabet2;
        private static readonly int size = 26;

        private void FillArrays()
        {
            Random random = new();

            int index;

            for (int i = 97; i <= 122; ++i)
            {
                do
                {
                    index = random.Next(alphabet1.Length);
                }
                while (alphabet1[index] != '\0');

                alphabet1[index] = Convert.ToChar(i);

                do
                {
                    index = random.Next(alphabet2.Length);
                }
                while (alphabet2[index] != '\0');

                alphabet2[index] = Convert.ToChar(i);
            }
        }

        public PairCipher()
        {
            alphabet1 = new char[size];
            alphabet2 = new char[size];
            
            this.FillArrays();
        }

        public string Encrypt(string input)
        {
            string result = string.Empty;
            char symbol;

            foreach (char c in input)
            {
                if (IsUpper(c))
                {
                    symbol = Convert.ToChar(Convert.ToInt32(c) + 32);
                }
                else { symbol = c; }

                if (IsLetter(c))
                { 
                    for (int i = 0; i < alphabet1.Length; ++i)
                    {
                        if (symbol == alphabet1[i])
                        {
                            if (IsUpper(c))
                            {
                                symbol = Convert.ToChar(Convert.ToInt32(alphabet2[i]) - 32);
                            }
                            else { symbol = alphabet2[i]; }

                            break;
                        }
                    }
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
            SwapArrays();

            string result = this.Encrypt(input);

            SwapArrays();

            return result;

            void SwapArrays()
            {
                char temp;
                for (int i = 0; i < alphabet1.Length; ++i)
                {
                    temp = alphabet1[i];
                    alphabet1[i] = alphabet2[i];
                    alphabet2[i] = temp;
                }
            }
        }
    }
}
