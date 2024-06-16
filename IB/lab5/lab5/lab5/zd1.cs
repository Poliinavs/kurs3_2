using _5;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{

    class Program
    {
        public static Dictionary<char, int> CountCharacterFrequency(string text)
        {
            Dictionary<char, int> frequencyMap = new Dictionary<char, int>();

            foreach (char c in text)
            {
                if (frequencyMap.ContainsKey(c))
                {
                    frequencyMap[c]++;
                }
                else
                {
                    frequencyMap[c] = 1;
                }
            }

            return frequencyMap;
        }
        static void Main(string[] args)
        {

            Marsh marsh = new Marsh();
            Console.OutputEncoding = Encoding.Unicode;

            //encrypt
            int key = 6;
            String msg = "";


            using (StreamReader sr = new StreamReader("belAlf.txt"))
            {
                msg = (sr.ReadToEnd());
                msg = msg.Replace(" ", "");
                msg = msg.ToLower();
            }

            long OldTicks = DateTime.Now.Ticks;
            string result_marsh = marsh.Encrypt(msg, key);
            
            long time_cipher = (DateTime.Now.Ticks - OldTicks) / 1000;

            Console.WriteLine("\nИсходный текст:  " + msg);
            Console.WriteLine("Маршрутный шифр: " + result_marsh);
            Console.WriteLine("Затрачено " + time_cipher + " мс\n\n\n\n");

            OldTicks = DateTime.Now.Ticks;
            string encript = marsh.Decrypt(msg, key);

            time_cipher = (DateTime.Now.Ticks - OldTicks) / 1000;
            Console.WriteLine("Расшифров текст: " + encript);
            Console.WriteLine("Затрачено " + time_cipher + " мс\n\n\n\n");

            Dictionary<char, int> frequencyMap = CountCharacterFrequency(msg);
            Dictionary<char, int> frequencyMap1 = CountCharacterFrequency(encript);

            //marsh.Decrypt(result_marsh, key)


            Console.OutputEncoding = Encoding.UTF8;

            Mnozh mnozh = new Mnozh();
            int[] key1 = { 2, 1, 0, 3 };
            int[] key2 = { 7, 2, 5, 6, 8, 3, 4, 1, 0 };
           // OldTicks = DateTime.Now.Ticks;

            var fio = "Авсюкевіч Паліна Вадзімовна";
            fio = fio.Replace(" ", "");
            fio = fio.ToLower();
             OldTicks = DateTime.Now.Ticks;
            string result_mnozh = mnozh.Encrypt(fio, key1, key2);

            time_cipher = (DateTime.Now.Ticks - OldTicks) / 1000;

            Console.WriteLine("Исходный текст:  " + fio);
            Console.WriteLine("Множеств шифр:   " + result_mnozh);
            Console.WriteLine("Затрачено " + time_cipher + " мс");

            OldTicks = DateTime.Now.Ticks;
            string result_rash = mnozh.Decrypt(result_mnozh, key2, key1);

            time_cipher = (DateTime.Now.Ticks - OldTicks) / 1000;
            Console.WriteLine("Расшифров текст: " + result_rash);
            Console.WriteLine("Затрачено " + time_cipher + " мс");




            Console.ReadKey();
        }
    }
}
