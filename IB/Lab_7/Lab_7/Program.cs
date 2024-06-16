using System.Diagnostics;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

public class Program
{
	public static void Main()
	{
		Console.OutputEncoding = System.Text.Encoding.UTF8;

		// Исходный текст
		string original = File.ReadAllText("text.txt");
		bool isTrue = false;
		// Произвольный ключ, слабый ключ и полуслабый ключ
		var keys = new List<string>
		{
			//"1234567890ABCDEF", // Произвольный ключ
            //"0101010101010101", // Слабый ключ
            "01FE01FE01FE01FE"  // Полуслабый ключ
        };

		foreach (string key in keys)
		{
			isTrue = !isTrue;
			Console.ForegroundColor = isTrue ? ConsoleColor.Red : ConsoleColor.Blue;
			// Создание нового экземпляра класса TripleDESCryptoServiceProvider
			var tdes = new TripleDESCryptoServiceProvider();

			if (TripleDES.IsWeakKey(tdes.Key))
			{
				// Отключение проверки на слабый ключ
				tdes = new TripleDESCryptoServiceProvider() { Key = tdes.Key, Mode = CipherMode.ECB };//тройной Des 3 вар
				// Установка ключа и режима шифрования
				tdes.Key = ASCIIEncoding.ASCII.GetBytes(key);
			}

			// Разделение исходного текста на блоки
			List<string> blocks = SplitIntoBlocks(original, 8);
			Console.WriteLine("Key: " + key);
			foreach (string block in blocks)
			{
				var stopwatch = new Stopwatch();
				// Преобразование блока в байты
				byte[] data = ASCIIEncoding.ASCII.GetBytes(block);

				// Создание объекта Encryptor и выполнение операции шифрования
				ICryptoTransform encryptor = tdes.CreateEncryptor();
				stopwatch.Start();// Запуск таймера
				byte[] encData = encryptor.TransformFinalBlock(data, 0, data.Length);
				stopwatch.Stop(); // Остановка таймера
				TimeSpan encryptionTime = stopwatch.Elapsed;

				// Вывод зашифрованных данных и времени шифрования
				Console.WriteLine("Зашифрованные данные(Bytes): " + BitConverter.ToString(encData));
				Console.WriteLine("Зашифрованные данные(ASCII): " + Encoding.ASCII.GetString(encData));
				Console.WriteLine("Зашифрованные данные(Base64): " + Convert.ToBase64String(encData));
				Console.WriteLine("Время шифрования: " + encryptionTime + " мс\n");

				// Создание объекта Decryptor и выполнение операции дешифрования
				ICryptoTransform decryptor = tdes.CreateDecryptor();
				stopwatch.Reset();
				stopwatch.Start(); // Перезапуск таймера
				byte[] originalData = decryptor.TransformFinalBlock(encData, 0, encData.Length);
				stopwatch.Stop(); // Остановка таймера
				TimeSpan decryptionTime = stopwatch.Elapsed;

				// Вывод исходных данных после дешифрования и времени дешифрования
				Console.WriteLine("Используется другой ключ для расшифровки: " + "00FF00FF00FF010FF");
				Console.WriteLine("Дешифрованные данные: " + ASCIIEncoding.ASCII.GetString(originalData));
				Console.WriteLine("Время дешифрования: " + decryptionTime + " мс\n");

				// Анализ лавинного эффекта
				int changedBits = 0;
				for (int i = 0; i < originalData.Length; i++)
				{
					changedBits += CountBits((byte)(originalData[i] ^ encData[i]));
				}
				Console.WriteLine("Количество измененных битов: " + changedBits + "\n");
			}

			// Оценка степени сжатия
			byte[] originalCompressed = Compress(ASCIIEncoding.ASCII.GetBytes(original));
			byte[] encryptedCompressed = Compress(ASCIIEncoding.ASCII.GetBytes(BitConverter.ToString(ASCIIEncoding.ASCII.GetBytes(key))));
			Console.WriteLine("Степень сжатия открытого текста: " + ((double)originalCompressed.Length / original.Length));
			Console.WriteLine("Степень сжатия зашифрованного текста: " + ((double)encryptedCompressed.Length / ASCIIEncoding.ASCII.GetBytes(key).Length) + "\n\n\n");
		}
	}

	// Функция для подсчета количества установленных битов в байте
	private static int CountBits(byte b)
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

	// Функция для разделения текста на блоки
	public static List<string> SplitIntoBlocks(string text, int blockSize)
	{
		var blocks = new List<string>();

		for (int i = 0; i < text.Length; i += blockSize)
		{
			if (i + blockSize > text.Length)
			{
				blockSize = text.Length - i;
				string block = text.Substring(i, blockSize);
				block = block.PadRight(8, ' '); // Дополнение блока до полного размера
				blocks.Add(block);
			}
			else
			{
				blocks.Add(text.Substring(i, blockSize));
			}
		}

		return blocks;
	}

	// Функция для сжатия данных
	public static byte[] Compress(byte[] data)
	{
		using (var compressedStream = new MemoryStream())
		using (var zipStream = new GZipStream(compressedStream, CompressionMode.Compress))
		{
			zipStream.Write(data, 0, data.Length);
			zipStream.Close();
			return compressedStream.ToArray();
		}
	}
}