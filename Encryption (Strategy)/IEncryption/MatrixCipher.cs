using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption__Strategy_.IEncryption
{
    using Rand;

    internal interface ISort
    {
        public void SortColumns(char[,] matrix, int y, string keyX, string changedKeyX);

        public void SortRows(char[,] matrix, int x, string keyY, string changedKeyY);
    }

    internal class EncryptionSort : ISort
    {
        public void SortColumns(char[,] matrix, int y, string keyX, string changedKeyX)
        {
            for (int i = 0; i < keyX.Length; ++i)
            {
                for (int j = 0; j < changedKeyX.Length; ++j)
                {
                    if (keyX[i] == changedKeyX[j])
                    {
                        Swap(i, j);
                        break;
                    }
                }
            }


            void Swap(int firstIndex, int secondIndex)
            {
                char temp;

                for (int i = 0; i < y; ++i)
                {
                    temp = matrix[firstIndex, i];
                    matrix[firstIndex, i] = matrix[secondIndex, i];
                    matrix[secondIndex, i] = temp;
                }
            }
        }

        public void SortRows(char[,] matrix, int x, string keyY, string changedKeyY)
        {
            for (int i = 0; i < keyY.Length; ++i)
            {
                for (int j = 0; j < changedKeyY.Length; ++j)
                {
                    if (keyY[i] == changedKeyY[j])
                    {
                        Swap(i, j);
                        break;
                    }
                }
            }


            void Swap(int firstIndex, int secondIndex)
            {
                char temp;

                for (int i = 0; i < x; ++i)
                {
                    temp = matrix[i, firstIndex];
                    matrix[i, firstIndex] = matrix[i, secondIndex];
                    matrix[i, secondIndex] = temp;
                }
            }
        }
    }

    internal class DecryptionSort : ISort
    {
        public void SortColumns(char[,] matrix, int y, string keyX, string changedKeyX)
        {
            for (int i = keyX.Length - 1; i >= 0; --i)
            {
                for (int j = changedKeyX.Length - 1; j >= 0; --j)
                {
                    if (keyX[i] == changedKeyX[j])
                    {
                        Swap(i, j);
                        break;
                    }
                }
            }


            void Swap(int firstIndex, int secondIndex)
            {
                char temp;

                for (int i = 0; i < y; ++i)
                {
                    temp = matrix[firstIndex, i];
                    matrix[firstIndex, i] = matrix[secondIndex, i];
                    matrix[secondIndex, i] = temp;
                }
            }
        }

        public void SortRows(char[,] matrix, int x, string keyY, string changedKeyY)
        {
            for (int i = keyY.Length - 1; i >= 0; --i)
            {
                for (int j = changedKeyY.Length - 1; j >= 0; --j)
                {
                    if (keyY[i] == changedKeyY[j])
                    {
                        Swap(i, j);
                        break;
                    }
                }
            }


            void Swap(int firstIndex, int secondIndex)
            {
                char temp;

                for (int i = 0; i < x; ++i)
                {
                    temp = matrix[i, firstIndex];
                    matrix[i, firstIndex] = matrix[i, secondIndex];
                    matrix[i, secondIndex] = temp;
                }
            }
        }
    }


    internal class MatrixCipher : IEncryption<string>
    {
        private char[,] matrix;
        private int x;
        private int y;
        private string keyX;
        private string changedKeyX;
        private string keyY;
        private string changedKeyY;

        private void GenerateKeys()
        {
            Rand.RandStr random = new Rand.RandStr();

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

                List<char> list = new List<char>();

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
