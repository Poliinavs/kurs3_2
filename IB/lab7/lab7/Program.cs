using lab7;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main()
    {
        bool isTrue = false;
        int ii = 0;

        foreach (var key in Operation.keys)
        {
            isTrue = !isTrue;
            var tdes = new TripleDESCryptoServiceProvider();
            if (TripleDES.IsWeakKey(tdes.Key))
            {
                tdes = new TripleDESCryptoServiceProvider() { Key = tdes.Key, Mode = CipherMode.ECB };
                tdes.Key = ASCIIEncoding.ASCII.GetBytes(key.PadRight(8, '0').Substring(0, 8));
            }

            var blocks = Operation.SplitIntoBlocks(Operation.text, 8);
            Console.WriteLine("Ключ: " + key);

            foreach (var block in blocks)
            {
                var stopwatch = new Stopwatch();
                var data = ASCIIEncoding.ASCII.GetBytes(block);

                var encryptor = tdes.CreateEncryptor();
                stopwatch.Start();
                var encData = encryptor.TransformFinalBlock(data, 0, data.Length);
                stopwatch.Stop();
                var encryptionTime = stopwatch.Elapsed;
                Console.WriteLine("Данные для ключа: " + Operation.names[ii]);
                Console.WriteLine("Данные зашифрованные в base64: " + Convert.ToBase64String(encData));
                Console.WriteLine("Время для шифрования: " + encryptionTime + " мс");

                var decryptor = tdes.CreateDecryptor();
                stopwatch.Reset();
                stopwatch.Start();
                var originalData = decryptor.TransformFinalBlock(encData, 0, encData.Length);
                stopwatch.Stop();
                var decryptionTime = stopwatch.Elapsed;

                Console.WriteLine("Дешифрованные данные: " + ASCIIEncoding.ASCII.GetString(originalData));
                Console.WriteLine("Время для дешифрования: " + decryptionTime + " мс");

                int changedBits = 0;
                for (int i = 0; i < originalData.Length; i++)
                {
                    changedBits += CountBits((byte)(originalData[i] ^ encData[i]));
                }

                Console.WriteLine("лавинный эффект: " + changedBits);
            }

            byte[] textCompressed = Operation.Compress(ASCIIEncoding.ASCII.GetBytes(Operation.text));
            byte[] encryptedCompressed = Operation.Compress(ASCIIEncoding.ASCII.GetBytes(BitConverter.ToString(ASCIIEncoding.ASCII.GetBytes(key))));
            Console.WriteLine("Степень сжатия для изначального текста: " + ((double)textCompressed.Length / Operation.text.Length));
            Console.WriteLine("Степень сжатия для зашифрованного текста: " + ((double)encryptedCompressed.Length / ASCIIEncoding.ASCII.GetBytes(key).Length) + "\n");
            ii++;

        }
        Console.ReadLine();
    }

    static int CountBits(byte b)
    {
        int count = 0;
        for (int i = 0; i < 8; i++)
        {
            if ((b & (1 << i)) != 0)
            {
                count++;
            }
        }
        return count;
    }

   
}
