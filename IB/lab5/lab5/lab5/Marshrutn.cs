using System;
using System.Linq;

namespace _5
{
    class Marsh
    {
        static char[] characters = new char[] { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'і', 'й', 'к', 'л', 'м',
                                         'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ў', 'ф', 'х', 'ц', 'ч', 'ш', 'ы',
                                         'ь', 'э', 'ю', 'я' };
        static int N = characters.Length;
        int[] colFx1 = new int[N];
        int[] colFx2 = new int[N];

        public string Encrypt(string msg, int key)
        {
            // Calculate occurrences of each character in the message
            for (int i = 0; i < N; i++)
            {
                colFx1[i] = msg.Where(el => el == characters[i]).Count();
            }

            string result = string.Empty;

            // Split the message into rows
            string[] msgInArray = new string[key];

            for (int i = 0; i < key; i++)
            {
                for (int j = i; j < msg.Length; j += key)
                {
                    msgInArray[i] += msg[j];
                }
            }

            // Concatenate characters column-wise
            for (int i = 0; i < key; i++)
            {
                result += msgInArray[i];
            }

            return result;
        }

        public string Decrypt(string msg, int key)
        {
            string result = string.Empty;

            // Calculate the number of rows needed
            int rows = (int)Math.Ceiling((double)msg.Length / key);
            int padding = rows * key - msg.Length;

            // Adjust the padding if necessary
            if (padding != 0)
            {
                msg = msg.PadRight(rows * key);
            }

            // Split the message into columns
            string[] msgInArray = new string[rows];

            for (int i = 0; i < rows; i++)
            {
                msgInArray[i] = msg.Substring(i * key, key);
            }

            // Concatenate characters row-wise
            for (int i = 0; i < key; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    result += msgInArray[j][i];
                }
            }

            // Remove padding
            result = result.TrimEnd();

            return result;
        }

    }
}
