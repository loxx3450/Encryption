using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption__Strategy_.IEncryption
{
    using Rand;

    internal class ADFGXCipher : IEncryption<string>
    {
        private readonly char[,] matrix;
        private static readonly int size = 5;
        private string key;
        private string sortedKey;
        private static readonly int keyLength = 7;
        private readonly Dictionary<int, char> keys;
        private readonly Dictionary<char, int> indexes;

        private void FillMatrix()
        {
            Random random = new();

            int x, y;

            for(int i = 97; i <= 122; ++i)
            {
                if (i == 113) { continue; }

                do
                {
                    x = random.Next(size);
                    y = random.Next(size);
                }
                while (matrix[x, y] != '\0');

                matrix[x, y] = Convert.ToChar(i);
            }
        }

        private void GenerateKey()
        {
            RandStr randStr = new();

            do
            {
                this.key = randStr.Rand(keyLength).ToLower();
                this.sortedKey = SortKey(this.key);
            }
            while(!IsValidKey(sortedKey));

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

        public ADFGXCipher()
        {
            this.matrix = new char[size, size];

            this.FillMatrix();

            this.GenerateKey();

            this.keys = new Dictionary<int, char>() { { 0, 'A' }, { 1, 'D' }, { 2, 'F' }, { 3, 'G' }, { 4, 'X' }, };    

            this.indexes = new Dictionary<char, int>() { { 'A', 0 }, { 'D', 1 }, { 'F', 2 }, { 'G', 3 }, { 'X', 4 }, };
        }

        public string Encrypt(string input)
        {
            string result = string.Empty;

            string cipher = GetCipher();

            int x = cipher.Length / keyLength + 1;
            int y = keyLength;

            char[,] tempMatrix = new char[x, y];

            int index = 0;

            foreach (char c in cipher)
            {
                tempMatrix[index / y, index % y] = c;
                index++;
            }

            tempMatrix = SortMatrix();

            for (int i = 0; i < y; ++i)
            {
                for (int j = 0; j < x; ++j)
                {
                    result += tempMatrix[j, i];
                }
                result += ' ';
            }

            return result;

            string GetCipher()
            {
                string cipher = string.Empty;

                Point position;

                foreach (char c in input)
                {
                    position = FindLetter(c);

                    if (position.X == -1 && position.Y == -1) { continue; }

                    cipher += keys[position.X];
                    cipher += keys[position.Y];
                }

                return cipher;
            }

            Point FindLetter(char c)
            {
                for (int i = 0; i < size; ++i)
                {
                    for (int j = 0; j < size; ++j)
                    {
                        if (matrix[i, j] == c || Convert.ToInt32(matrix[i, j] - 32) == Convert.ToInt32(c)) 
                        {
                            return new Point(i, j);
                        }
                    }
                }

                return new Point(-1, -1);
            }

            char[,] SortMatrix()
            {
                char[,] result = new char[x, y]; 

                for (int i = 0; i < sortedKey.Length; ++i)
                {
                    for (int j = 0; j < key.Length; ++j)
                    {
                        if (sortedKey[i] == key[j])
                        {
                            Swap(i, j);
                            break;
                        }
                    }
                }

                return result;

                void Swap(int to, int from)
                {
                    for (int index = 0; index < x; ++index)
                    {
                        result[index, to] = tempMatrix[index, from];
                    }
                }
            }
        }

        public string Decrypt(string input)
        {
            string result = string.Empty;

            int times = 0;

            foreach (char c in input) { if (c != ' ') { times++; } }

            int x = times / keyLength + 1;
            int y = keyLength;

            char[,] tempMatrix = CreateTempMatrix();

            tempMatrix = SortMatrix();

            string cipher = GetCipher();

            int indexI, indexJ;

            for (int index = 0; index < cipher.Length; index += 2)
            {
                indexI = indexes[cipher[index]];
                indexJ = indexes[cipher[index + 1]];

                result += matrix[indexI, indexJ];
            }

            return result;

            string GetCipher()
            {
                string cipher = string.Empty;

                for (int i = 0; i < x; ++i)
                {
                    for (int j = 0; j < y; ++j)
                    {
                        if (tempMatrix[i, j] != '\0')
                        {
                            cipher += tempMatrix[i, j];
                        }
                    }
                }

                return cipher;
            }

            char[,] CreateTempMatrix()
            {
                char[,] tempMatrix = new char[x, y];

                int indexI = 0, indexJ = 0;

                foreach (char c in input)
                {
                    if (c != ' ')
                    {
                        tempMatrix[indexI++, indexJ] = c;
                    }
                    else
                    {
                        indexI = 0;
                        indexJ++;
                    }
                }

                return tempMatrix;
            }

            char[,] SortMatrix()
            {
                char[,] result = new char[x, y];

                for (int i = 0; i < key.Length; ++i)
                {
                    for (int j = 0; j < sortedKey.Length; ++j)
                    {
                        if (key[i] == sortedKey[j])
                        {
                            Swap(i, j);
                            break;
                        }
                    }
                }

                return result;

                void Swap(int to, int from)
                {
                    for (int index = 0; index < x; ++index)
                    {
                        result[index, to] = tempMatrix[index, from];
                    }
                }
            }
        }
    }
}
