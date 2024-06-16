using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    class Mnozh
    {
        static char[] characters = new char[] { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'і', 'й', 'к', 'л', 'м',
                                         'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ў', 'ф', 'х', 'ц', 'ч', 'ш', 'ы',
                                         'ь', 'э', 'ю', 'я' };
        public string Encrypt(string msg, int[] key1, int[] key2)
        {
            string result = string.Empty;
            string[] msgInArray = new string[(msg.Length / key1.Length) + 1];

            for (int i = 0; i < msg.Length / key1.Length + 1; i++)
            {
                int length = Math.Min(key1.Length, msg.Length - i * key1.Length);
                msgInArray[i] = msg.Substring(i * key1.Length, length);
                Console.WriteLine($"msgInArray[{i}] = {msgInArray[i]}");
            }

            char[,] res = new char[key1.Length, key2.Length];

            for (int i = 0; i < key1.Length; i++)
            {
                for (int k = 0; k < key2.Length; k++)
                {
                    if (k < msgInArray.Length && i < msgInArray[k].Length)
                    {
                        res[key1[i], key2[k]] = msgInArray[k][i];
                    }
                }
            }

            result = GetStringFromMatrix(res);
            return result;
        }

        public string Decrypt(string msg, int[] key1, int[] key2)
        {
            string result = string.Empty;
            string[] msgInArray = new string[(msg.Length / key1.Length) + 1];

            for (int i = 0; i < msg.Length / key1.Length + 1; i++)
            {
                int length = Math.Min(key1.Length, msg.Length - i * key1.Length);
                msgInArray[i] = msg.Substring(i * key1.Length, length);
            }

            char[,] res = new char[key1.Length, key2.Length];

            for (int i = 0; i < key1.Length; i++)
            {
                for (int k = 0; k < key2.Length; k++)
                {
                    if (k < msgInArray.Length && key1[i] < msgInArray[k].Length)
                    {
                        res[i, k] = msgInArray[k][key1[i]];
                    }
                }
            }

            result = GetStringFromMatrix(res);
            return result;
        }

        private string GetStringFromMatrix(char[,] matrix)
        {
            string result = string.Empty;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    result += matrix[i, j];
                }
            }
            return result.Replace("\0", "");
        }
    }
}
