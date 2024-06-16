using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

public class VigenereCipher
{
	private const string Alphabet = "11абвгдеёжзійклмнопрстуўфхцчшыьэюя"; // Белорусский алфавит
	private readonly string _key;
	private readonly int _n;

	public VigenereCipher(string key)
	{
		_key = key.ToLower(); // Приводим ключ к нижнему регистру
		_n = Alphabet.Length;
	}

	public string Encrypt(string input)
	{
		StringBuilder result = new StringBuilder();
		input = input.ToLower(); // Приводим все буквы к нижнему регистру

		for (int i = 0; i < input.Length; i++)
		{
			char symbol = input[i];
			int x = Alphabet.IndexOf(symbol);
			if (x < 0)
			{
				result.Append(symbol);
			}
			else
			{
				int k = Alphabet.IndexOf(_key[i % _key.Length]);
				int y = (x + k) % _n;
				result.Append(Alphabet[y]);
			}
		}

		return result.ToString();
	}

	public string Decrypt(string input)
	{
		StringBuilder result = new StringBuilder();
		input = input.ToLower(); // Приводим все буквы к нижнему регистру

		for (int i = 0; i < input.Length; i++)
		{
			char symbol = input[i];
			int y = Alphabet.IndexOf(symbol);
			if (y < 0)
			{
				result.Append(symbol);
			}
			else
			{
				int k = Alphabet.IndexOf(_key[i % _key.Length]);
				int x = (y - k + _n) % _n;
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
		Console.OutputEncoding = Encoding.UTF8;
		var cipher = new VigenereCipher("Аўсюкевіч");
		var text = File.ReadAllText("text.txt");

		var stopwatch = Stopwatch.StartNew();
		var encrypted = cipher.Encrypt(text);
		stopwatch.Stop();
		var encryptionTime = stopwatch.Elapsed;

		stopwatch.Restart();
		var decrypted = cipher.Decrypt(encrypted);
		stopwatch.Stop();
		var decryptionTime = stopwatch.Elapsed;

		Console.WriteLine($"Оригинальный текст: {text}");
		Console.WriteLine($"Зашифрованный текст: {encrypted}");
		Console.WriteLine($"Расшифрованный текст: {decrypted}");
		Console.WriteLine($"Время шифрования: {encryptionTime.TotalMilliseconds} мс");
		Console.WriteLine($"Время дешифрования: {decryptionTime.TotalMilliseconds} мс");

		var originalFrequencies = CountCharacterFrequencies1(text);
		var encryptedFrequencies = CountCharacterFrequencies1(encrypted);

		Console.WriteLine("Частоты символов в оригинальном тексте:");
		foreach (var pair in originalFrequencies)
		{
			Console.WriteLine($"Символ {pair.Key}: {pair.Value} раз");
		}

		Console.WriteLine("Частоты символов в зашифрованном тексте:");
		foreach (var pair in encryptedFrequencies)
		{
			Console.WriteLine($"Символ {pair.Key}: {pair.Value} раз");
		}

		Console.ReadLine();
	}

	static Dictionary<char, int> CountCharacterFrequencies1(string text)
	{
		var Alphabet = "абвгдеёжзійклмнопрстуўфхцчшыьэюя"; // Белорусский алфавит
		var frequencies = new Dictionary<char, int>();
		foreach (var c in text)
		{
			if (!Alphabet.Contains(c)) continue; // Пропускаем символы, которые не входят в алфавит
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
