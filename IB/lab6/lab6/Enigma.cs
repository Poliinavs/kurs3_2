using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAB_06
{
    public class Enigma
    {
        string alph = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string rotor1 = "EKMFLGDQVZMTOWYHXUSPAIBRCJ";
        string rotor3 = "BDFHJLCPRTXVZNYEIWGAKMUSQO";
        string rotor2 = "AJDKSIRUXBLHWTMCQGZNPYFVOE";
        string[] reflectorB = { "AY", "BR", "CU", "DH", "EQ", "FS", "GL", "IP", "JX", "KN", "MO", "TZ", "VW" };
        public string Crypt(string text, int posL, int posM, int posR)
        {
            var rotorR = new Rotor(rotor3, posR);
            var rotorM = new Rotor(rotor2, posM);
            var rotorL = new Rotor(rotor1, posL);
            var result = new StringBuilder(text.Length);
            foreach (var ch in text)
            {
                Console.WriteLine(ch);

                char symbol = rotorR[alph.IndexOf(ch)];
                WriteText(symbol);
                symbol = rotorM[alph.IndexOf(symbol)];
                WriteText(symbol);
                symbol = rotorL[alph.IndexOf(symbol)];
                WriteText(symbol);
                symbol = reflectorB.First(x => x.Contains(symbol)).First(x => !x.Equals(symbol));
                WriteText(symbol);
                symbol = rotorL[alph.IndexOf(symbol)];
                WriteText(symbol);
                symbol = rotorM[alph.IndexOf(symbol)];
                WriteText(symbol);
                symbol = rotorR[alph.IndexOf(symbol)];
                WriteText(symbol);
                Console.WriteLine();
                result.Append(symbol);
            }

            return result.ToString();
        }
        public Dictionary<char, int> GetCharacterFrequency(string text)
        {
            Dictionary<char, int> frequency = new Dictionary<char, int>();

            foreach (char c in text)
            {
                if (char.IsLetter(c))
                {
                    if (frequency.ContainsKey(c))
                        frequency[c]++;
                    else
                        frequency[c] = 1;
                }
            }

            return frequency;
        }


        public string Decrypt(string text, int posL, int posM, int posR)
        {
            var rotorR = new Rotor(rotor3, posR);
            var rotorM = new Rotor(rotor2, posM);
            var rotorL = new Rotor(rotor1, posL);
            StringBuilder result = new StringBuilder(text.Length);
            foreach (var ch in text)
            {
                 Console.WriteLine(ch);
                char symbol = alph[rotorR.IndexOf(ch)];
                WriteText(symbol);
                symbol = alph[rotorM.IndexOf(symbol)];
                WriteText(symbol);
                symbol = alph[rotorL.IndexOf(symbol)];
                WriteText(symbol);
                symbol = reflectorB.First(x => x.Contains(symbol)).First(x => !x.Equals(symbol));
                WriteText(symbol);
                symbol = alph[rotorL.IndexOf(symbol)];
                WriteText(symbol);
                symbol = alph[rotorM.IndexOf(symbol)];
                WriteText(symbol);
                symbol = alph[rotorR.IndexOf(symbol)];
                WriteText(symbol);
                Console.WriteLine();
                result.Append(symbol);
            }

            return result.ToString();
        }

        public void WriteText(char symbol)
        {
            Console.Write("-> " + symbol);
        }
    }
    
}