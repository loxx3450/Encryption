using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption__Strategy_.IEncryption
{
    using Rand;
    using IEncryption.TextEncryption.MatrixCipher;

    internal class MatrixCipher : IEncryption<string>
    {
        private readonly char[,] matrix;
        private readonly int x;
        private readonly int y;
        private string keyX;
        private string changedKeyX;
        private string keyY;
        private string changedKeyY;

        private void GenerateKeys()
        {
            RandStr random = new();

            do
            {
                keyX = random.Rand(x).ToLower();
                changedKeyX = SortKey(keyX);
            }
            while (!IsValidKey(changedKeyX));

            do
            {
                keyY = random.Rand(y).ToLower();
                changedKeyY = SortKey(keyY);
            }
            while (!IsValidKey(changedKeyY));

            bool IsValidKey(string key)
            {
                for (int i = 0; i < key.Length - 1; ++i)
                {
                    if (key[i] == key[i + 1])
                    {
                        return false;
                    }
                }

                return true;
            }

            string SortKey(string key)
            {
                string result = string.Empty;

                List<char> list = new();

                foreach (char c in key) { list.Add(c); }

                list.Sort();

                foreach (char c in list) { result += c; }

                return result;
            }
        }

        private string Encryption(ISort sort, string input)
        {
            string result = string.Empty;

            int index = 0;

            while (index <= input.Length)
            {
                for (int i = index; i < input.Length && i < matrix.Length + index; ++i)
                {
                    matrix[(i - index) / y, i % (x + 1)] = input[i];
                }
                index += matrix.Length;

                sort.SortColumns(matrix, y, keyX, changedKeyX);
                sort.SortRows(matrix, x, keyY, changedKeyY);

                for (int i = 0; i < x; ++i)
                {
                    for (int j = 0; j < y; ++j)
                    {
                        if (matrix[i, j] != '0') { result += matrix[i, j]; }
                    }
                }

                CleanMatrix();
            }

            return result;

            void CleanMatrix()
            {
                for (int i = 0; i < x; ++i)
                {
                    for (int j = 0; j < y; ++j)
                    {
                        matrix[i, j] = '0';
                    }
                }
            }
        }

        public MatrixCipher(int x = 4, int y = 5)
        {
            matrix = new char[x, y];
            this.x = x;
            this.y = y;

            GenerateKeys();
        }

        public string Encrypt(string input)
        {
            return this.Encryption(new EncryptionSort(), input);
        }

        public string Decrypt(string input)
        {
            return this.Encryption(new DecryptionSort(), input);
        }
    }
}
