using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;


public class CaesarCipher
{
    private const string Alphabet = "абвгдеёжзійклмнопрстуўфхцчшыьэюя"; // Белорусский алфавит
    private readonly int _key;
    private readonly int _n;

    public CaesarCipher(int key)
    {
        _key = key;
        _n = Alphabet.Length;
    }

    public string Encrypt(string input)
    {
        StringBuilder result = new StringBuilder();
        input = input.ToLower(); // Приводим все буквы к нижнему регистру

        foreach (char symbol in input)
        {
            int x = Alphabet.IndexOf(symbol);
            if (x < 0)
            {
                result.Append(symbol);
            }
            else
            {
                int y = (x + _key) % _n;
                result.Append(Alphabet[y]);
            }
        }

        return result.ToString();
    }

    public string Decrypt(string input)
    {
        StringBuilder result = new StringBuilder();
        input = input.ToLower(); // Приводим все буквы к нижнему регистру

        foreach (char symbol in input)
        {
            int y = Alphabet.IndexOf(symbol);
            if (y < 0)
            {
                result.Append(symbol);
            }
            else
            {
                int x = (y - _key + _n) % _n;
                result.Append(Alphabet[x]);
            }
        }

        return result.ToString();
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        var Alphabet = "абвгдеёжзійклмнопрстуўфхцчшыьэюя"; // Белорусский алфавит
        var cipher = new CaesarCipher(5);
        var encoding = Encoding.UTF8;

        // Замените "text.txt" на актуальный путь к вашему файлу
        var text = File.ReadAllText("text.txt", encoding);
        var stopwatch = Stopwatch.StartNew();
        var encrypted = cipher.Encrypt(text);
        stopwatch.Stop();
        var encryptionTime = stopwatch.Elapsed;

        stopwatch.Restart();
        var decrypted = cipher.Decrypt(encrypted);
        stopwatch.Stop();
        var decryptionTime = stopwatch.Elapsed;

        Console.WriteLine($"Оригинальный текст: {text}");
        Console.WriteLine($"\n\n\nЗашифрованный текст: {encrypted}");
        Console.WriteLine($"\n\n\nРасшифрованный текст: {decrypted}");
        Console.WriteLine($"\n\n\nВремя шифрования: {encryptionTime.TotalMilliseconds} мс");
        Console.WriteLine($"Время дешифрования: {decryptionTime.TotalMilliseconds} мс");

        var originalFrequencies = CountCharacterFrequencies(text);
        var encryptedFrequencies = CountCharacterFrequencies(encrypted);
        Dictionary<char, int> a = new Dictionary<char, int>(); 

        Console.WriteLine("Частоты символов в оригинальном тексте:");
        foreach (var pair in originalFrequencies)
        {
            if (Alphabet.Contains(pair.Key))
            {
                Console.WriteLine($"Символ {pair.Key}: {pair.Value} раз");
            }
        }

        Console.WriteLine("Частоты символов в зашифрованном тексте:");
        foreach (var pair in encryptedFrequencies)
        {
            if (Alphabet.Contains(pair.Key))
            {
                Console.WriteLine($"Символ {pair.Key}: {pair.Value} раз");
                a.Add(pair.Key, pair.Value);
            }
        }

        Console.ReadLine();
    }

    static Dictionary<char, int> CountCharacterFrequencies(string text)
    {
        var frequencies = new Dictionary<char, int>();
        foreach (var c in text)
        {
            if (frequencies.ContainsKey(c))
            {
                frequencies[c]++;
            }
            else
            {
                frequencies[c] = 1;
            }
        }

        return frequencies;
    }
}
