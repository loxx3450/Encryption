using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption__Strategy_.IEncryption
{
    internal class FourSquaresCipher : IEncryption<string>
    {
        private readonly char[,] array1;
        private readonly char[,] array2;
        private readonly char[,] array3;
        private readonly char[,] array4;
        private static readonly int size = 5;

        private void FillArrays()
        {
            Random random = new();

            for (int i = 97; i <= 122; ++i)
            {
                if (i == 113) { continue; }

                FillElement(array1, i);
                FillElement(array2, i);
                FillElement(array3, i);
                FillElement(array4, i);
            }

            void FillElement(char[,] array, int i)
            {
                int x, y;

                do
                {
                    x = random.Next(array.GetLength(0));
                    y = random.Next(array.GetLength(0));
                }
                while (array[x, y] != '\0');

                array[x, y] = Convert.ToChar(i);
            }
        }

        public FourSquaresCipher()
        {
            array1 = new char[size, size];
            array2 = new char[size, size];
            array3 = new char[size, size]; 
            array4 = new char[size, size];

            this.FillArrays();
        }

        public string Encrypt(string input)
        {
            string result = string.Empty;

            Point x, y;

            int index;

            bool firstUpper = false, secondUpper = false;

            for (int i = 0; i < input.Length; ++i)
            {
                if (IsLetter(input[i]))
                {
                    if (IsUpper(input[i])) { firstUpper = true; }
                    x = FindValInArr(input[i++], array1);

                    index = i;

                    while (!IsLetter(input[index])) 
                    { 
                        if (++index == input.Length) 
                        {
                            for (int j = i - 1; j < index; ++j) { result += input[j]; }

                            return result;
                        } 
                    }

                    if (IsUpper(input[index])) { secondUpper = true; }
                    y = FindValInArr(input[index], array4);

                    if (firstUpper) 
                    { 
                        result += Upper(array3[y.X, x.Y]); 
                    }
                    else { result += array3[y.X, x.Y]; }

                    for(int j = i; j < index; ++j) { result += input[j]; }

                    if (secondUpper) 
                    { 
                        result += Upper(array2[x.X, y.Y]); 
                    }
                    else { result += array2[x.X, y.Y]; }

                    i = index;

                    firstUpper = false;
                    secondUpper = false;
                }
                else
                {
                    result += input[i];
                }
            }

            return result;

            static Point FindValInArr(char value, char[,] array)
            {
                for (int i = 0; i < size; ++i)
                {
                    for (int j = 0; j < size; ++j)
                    {
                        if (array[i, j] == value || array[i, j] == Convert.ToChar(Convert.ToInt32(value) + 32))
                        {
                            return new Point(i, j);
                        }
                    }
                }

                return new Point();
            }

            bool IsUpper(char c)
            {
                return c >= 'A' && c <= 'Z';
            }

            bool IsLetter(char c)
            {
                return IsUpper(c) || (c >= 'a' && c <= 'z');
            }

            char Upper(char c)
            {
                return Convert.ToChar(Convert.ToInt32(c - 32));
            }
        }

        public string Decrypt(string input)
        {
            Swap(this.array1, this.array3);
            Swap(this.array2, this.array4);

            string result = this.Encrypt(input);

            Swap(this.array1, this.array3);
            Swap(this.array2, this.array4);

            return result;

            static void Swap(char[,] firstArray, char[,] secondArray)
            {
                char temp;

                for (int i = 0; i < size; ++i)
                {
                    for (int j = 0; j < size; ++j)
                    {
                        temp = firstArray[i, j]; 
                        firstArray[i, j] = secondArray[i, j];
                        secondArray[i, j] = temp;
                    }
                }
            }
        }
    }
}
