using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption__Strategy_.IEncryption
{
    internal class FourSquaresCipher : IEncryption<string>
    {
        private char[,] array1;
        private char[,] array2;
        private char[,] array3;
        private char[,] array4;
        private static int size = 5;

        private void FillArrays()
        {
            Random random = new Random();

            int x = 0, y = 0;

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
                do
                {
                    x = random.Next(array.GetLength(0));
                    y = random.Next(array.GetLength(0));
                }
                while (array[x, y] != '\0');

                array[x, y] = Convert.ToChar(i);
            }
        }

        struct Point
        {
            public Point(int x = -1, int y = -1)
            {
                this.X = x;
                this.Y = y;
            }

            public int X { get; set; }

            public int Y { get; set; }
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
                if ((input[i] >= 'A' && input[i] <= 'Z') || (input[i] >= 'a' && input[i] <= 'z'))
                {
                    if (input[i] >= 'A' && input[i] <= 'Z') { firstUpper = true; }
                    x = FindValInArr(input[i++], array1);

                    index = i;

                    while (!((input[index] >= 'A' && input[index] <= 'Z') || (input[index] >= 'a' && input[index] <= 'z'))) { 
                        if (++index == input.Length) 
                        {
                            for (int j = i - 1; j < index; ++j) { result += input[j]; }

                            return result;
                        } 
                    }

                    if (input[index] >= 'A' && input[index] <= 'Z') { secondUpper = true; }
                    y = FindValInArr(input[index], array4);

                    if (firstUpper) 
                    { 
                        result += Convert.ToChar(Convert.ToInt32(array3[y.X, x.Y] - 32)); 
                    }
                    else { result += array3[y.X, x.Y]; }

                    for(int j = i; j < index; ++j) { result += input[j]; }

                    if (secondUpper) 
                    { 
                        result += Convert.ToChar(Convert.ToInt32(array2[x.X, y.Y] - 32)); 
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

            Point FindValInArr(char value, char[,] array)
            {
                for (int i = 0; i < array.GetLength(0); ++i)
                {
                    for (int j = 0; j < array.GetLength(0); ++j)
                    {
                        if (array[i, j] == value || array[i, j] == Convert.ToChar(Convert.ToInt32(value) + 32))
                        {
                            return new Point(i, j);
                        }
                    }
                }

                return new Point();
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

            void Swap(char[,] firstArray, char[,] secondArray)
            {
                char temp;

                for (int i = 0; i < firstArray.GetLength(0); ++i)
                {
                    for (int j = 0; j < firstArray.GetLength(0); ++j)
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
