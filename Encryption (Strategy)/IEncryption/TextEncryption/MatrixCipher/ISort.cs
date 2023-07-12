using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption__Strategy_.IEncryption.TextEncryption.MatrixCipher
{
    internal abstract class ISort
    {
        protected static void SwapColumns(char[,] matrix, int y, int firstIndex, int secondIndex)
        {
            char temp;

            for (int i = 0; i < y; ++i)
            {
                temp = matrix[firstIndex, i];
                matrix[firstIndex, i] = matrix[secondIndex, i];
                matrix[secondIndex, i] = temp;
            }
        }

        protected static void SwapRows(char[,] matrix, int x, int firstIndex, int secondIndex)
        {
            char temp;

            for (int i = 0; i < x; ++i)
            {
                temp = matrix[i, firstIndex];
                matrix[i, firstIndex] = matrix[i, secondIndex];
                matrix[i, secondIndex] = temp;
            }
        }

        public abstract void SortColumns(char[,] matrix, int y, string keyX, string changedKeyX);

        public abstract void SortRows(char[,] matrix, int x, string keyY, string changedKeyY);
    }

    internal class EncryptionSort : ISort
    {
        public override void SortColumns(char[,] matrix, int y, string keyX, string changedKeyX)
        {
            for (int i = 0; i < keyX.Length; ++i)
            {
                for (int j = 0; j < changedKeyX.Length; ++j)
                {
                    if (keyX[i] == changedKeyX[j])
                    {
                        SwapColumns(matrix, y, i, j);
                        break;
                    }
                }
            }
        }

        public override void SortRows(char[,] matrix, int x, string keyY, string changedKeyY)
        {
            for (int i = 0; i < keyY.Length; ++i)
            {
                for (int j = 0; j < changedKeyY.Length; ++j)
                {
                    if (keyY[i] == changedKeyY[j])
                    {
                        SwapRows(matrix, x, i, j);
                        break;
                    }
                }
            }
        }
    }

    internal class DecryptionSort : ISort
    {
        public override void SortColumns(char[,] matrix, int y, string keyX, string changedKeyX)
        {
            for (int i = keyX.Length - 1; i >= 0; --i)
            {
                for (int j = changedKeyX.Length - 1; j >= 0; --j)
                {
                    if (keyX[i] == changedKeyX[j])
                    {
                        SwapColumns(matrix, y, i, j);
                        break;
                    }
                }
            }
        }

        public override void SortRows(char[,] matrix, int x, string keyY, string changedKeyY)
        {
            for (int i = keyY.Length - 1; i >= 0; --i)
            {
                for (int j = changedKeyY.Length - 1; j >= 0; --j)
                {
                    if (keyY[i] == changedKeyY[j])
                    {
                        SwapRows(matrix, x, i, j);
                        break;
                    }
                }
            }
        }
    }
}
