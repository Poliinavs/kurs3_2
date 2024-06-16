using System;
using System.Collections.Generic;

namespace LAB_06
{
    class Program
    {
        static void Main()
        {
            var test = "THE SUN BLAZED IN THE SKY, PAINTING THE WORLD IN VIBRANT SHADES OF ORANGE AND PINK. BIRDS CHIRPED MERRILY AS THEY SOARED THROUGH THE AIR, THEIR WINGS GLISTENING IN THE GOLDEN LIGHT. THE BREEZE WHISPERED SOFTLY THROUGH THE TREES, CARRYING WITH IT THE SCENT OF FRESHLY MOWN GRASS. IN THIS MOMENT, EVERYTHING FELT ALIVE AND FULL OF POSSIBILITY. THE WORLD WAS A CANVAS, WAITING TO BE EXPLORED AND DISCOVERED.";
            var fio = "AA";

            Enigma enigma = new Enigma();
            string encoded = enigma.Crypt(fio, 0, 2, 2);
            Console.WriteLine(encoded);
            Console.WriteLine(enigma.Decrypt(encoded, 0, 2, 2));

            Dictionary<char, int> frequency1 = enigma.GetCharacterFrequency(fio);
            Console.WriteLine("Частота появления символов в исходном тексте:");
            foreach (var pair in frequency1)
            {
                Console.WriteLine($"{pair.Key}: {pair.Value}");
            }
            Dictionary<char, int> frequency2 = enigma.GetCharacterFrequency(encoded);
            Console.WriteLine("Частота появления символов в зашифрованном тексе:");
            foreach (var pair in frequency1)
            {
                Console.WriteLine($"{pair.Key}: {pair.Value}");
            }
            Console.ReadLine();
        }
    }
}