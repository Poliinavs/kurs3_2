using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Base64Converter base64Converter = new Base64Converter();
            base64Converter.ConvertToBase64();

            // Task 2
            RedundancyCalculator redundancyCalculator = new RedundancyCalculator();
            redundancyCalculator.CalculateRedundancy();

            // Task 3
            XORNameFamily xorNameFamily = new XORNameFamily();
            xorNameFamily.PerformXOR("Polina", "Avsukevich");
            Console.WriteLine();
        }

        class Base64Converter
        {
            public void ConvertToBase64()
            {
                string base64Result = "";
                using (StreamReader reader = new StreamReader(@"C:\instit\kurs3_2\IB\lab2\lab3\pol.txt"))
                {
                    string fileContent = reader.ReadToEnd();
                    Console.WriteLine("text:");
                    Console.WriteLine(fileContent);
                    Console.WriteLine();

                    byte[] asciiBytes = Encoding.ASCII.GetBytes(fileContent);
                    base64Result = Convert.ToBase64String(asciiBytes);

                    Console.WriteLine("Converted to base64:");
                    Console.WriteLine(base64Result);
                }

                using (StreamWriter writer = new StreamWriter(@"C:\instit\kurs3_2\IB\lab2\lab3\base64.txt", false))
                {
                    writer.WriteLine(base64Result);
                }
            }
        }

        class RedundancyCalculator
        {
            public void CalculateRedundancy()
            {
                // English
                EntropyCalculator.CalculateEntropy("abcdefghijklmnopqrstuvwxyząćęłńóśźż");

                double entropyEnglishHartley = Math.Log(35, 2);
                Console.WriteLine($"Entropy of the alphabet by Shannon: {EntropyCalculator.ShannonEntropy}");
                Console.WriteLine($"Entropy Pol by Hartley: {entropyEnglishHartley}");

                double redundancyEnglish =  ((entropyEnglishHartley-EntropyCalculator.ShannonEntropy )/ entropyEnglishHartley)*100;
                Console.WriteLine($"Redundancy Pol: {redundancyEnglish}");

                // Base
                EntropyCalculator.CalculateEntropy("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/");

                double entropyBinaryHartley = Math.Log(64, 2);
                var ShannonEntropy = 4.33443443;
                Console.WriteLine($"Entropy of the alphabet by Shannon: {ShannonEntropy}");
                Console.WriteLine($"Entropy by Hartley for Base text: {entropyBinaryHartley}");

                double redundancyBinary = ((entropyBinaryHartley- ShannonEntropy) / entropyBinaryHartley)*100;
                Console.WriteLine($"Redundancy for Base text: {redundancyBinary}");
            }
        }

        class XORNameFamily
        {
            public void PerformXOR(string name, string family)
            {
                // Convert strings to byte arrays with ASCII encoding
                byte[] asciiName = Encoding.ASCII.GetBytes(name);
                byte[] asciiFamily = Encoding.ASCII.GetBytes(family);

                PadByteArray(ref asciiName, ref asciiFamily);

                // Ensure both arrays have the same length by padding the shorter one with zeros
                int maxLength = Math.Max(asciiName.Length, asciiFamily.Length);
                Array.Resize(ref asciiName, maxLength);
                Array.Resize(ref asciiFamily, maxLength);

                // Perform XOR operation in ASCII
                byte[] xorResultAscii = new byte[maxLength];
                for (int i = 0; i < maxLength; i++)
                {
                    xorResultAscii[i] = (byte)(asciiName[i] ^ asciiFamily[i]);
                }

                // Convert byte array to string
                string resultAsciiString = "Pol"+ Encoding.ASCII.GetString(xorResultAscii);

                // Convert strings to byte arrays with Base64 encoding
                byte[] base64Name = Convert.FromBase64String(Convert.ToBase64String(Encoding.UTF8.GetBytes(name)));
                byte[] base64Family = Convert.FromBase64String(Convert.ToBase64String(Encoding.UTF8.GetBytes(family)));

                // Ensure both arrays have the same length by padding the shorter one with zeros
                maxLength = Math.Max(base64Name.Length, base64Family.Length);
                Array.Resize(ref base64Name, maxLength);
                Array.Resize(ref base64Family, maxLength);

                // Perform XOR operation in Base64
                byte[] xorResultBase64 = new byte[maxLength];
                for (int i = 0; i < maxLength; i++)
                {
                    xorResultBase64[i] = (byte)(base64Name[i] ^ base64Family[i]);
                }

                // Convert byte array to string
                string resultBase64String = Convert.ToBase64String(xorResultBase64);

                Console.WriteLine("\n\nResult of XOR in ASCII:");
                Console.WriteLine(resultAsciiString);

                Console.WriteLine("\nResult of XOR in Base64:");
                Console.WriteLine(resultBase64String);
            }

            private void PadByteArray(ref byte[] array1, ref byte[] array2)
            {
                int maxLength = Math.Max(array1.Length, array2.Length);

                Array.Resize(ref array1, maxLength);
                Array.Resize(ref array2, maxLength);

                // Pad the shorter array with zeros
                for (int i = 0; i < maxLength; i++)
                {
                    array1[i] = array1.ElementAtOrDefault(i);
                    array2[i] = array2.ElementAtOrDefault(i);
                }
            }

        }

       

        static class EntropyCalculator
        {
            public static double ShannonEntropy = 0;

            public static void CalculateEntropy(string alphabet)
            {
                int[] letterCount = new int[alphabet.Length];
                float[] letter = new float[alphabet.Length];
                int totalLettersInFile = 0;
                double[] letterProbabilities = new double[alphabet.Length];

                using (StreamReader reader = new StreamReader(@"C:\instit\kurs3_2\IB\lab2\lab3\pol.txt"))
                {
                    string fileContent = reader.ReadToEnd().ToLower();
                    totalLettersInFile = fileContent.Length;

                    Console.WriteLine($"Number of characters in the file: {totalLettersInFile}");

                    for (int j = 0; j < alphabet.Length; j++)
                    {
                        letterCount[j] = fileContent.Count(x => x == alphabet[j]);
                        if (letterCount[j] != 0)
                        {
                            Console.WriteLine($"{alphabet[j]}: {letterCount[j]}");
                            letter[j] = letterCount[j];

                            letterProbabilities[j] = (double)letterCount[j] / totalLettersInFile;
                            Console.WriteLine($"P({alphabet[j]}): {letterProbabilities[j]}");
                            Console.WriteLine();

                            ShannonEntropy += letterProbabilities[j] * (Math.Log(letterProbabilities[j]) / Math.Log(2)) * (-1);
                        }
                    }
                }
            }
        }
    }
}
