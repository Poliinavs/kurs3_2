using System;
using System.Security.Cryptography;
using System.Text;

public class Program
{
	public static void Main()
	{
		// Исходный текст
		string original = "Hello, World!";

		// Полуслабые ключи
		string key1 = "01FE01FE01FE01FE";
		string key2 = "FE01FE01FE01FE01";

		// Создание нового экземпляра класса DESCryptoServiceProvider
		var desEncrypt = new TripleDESCryptoServiceProvider();

		if (TripleDES.IsWeakKey(desEncrypt.Key))
		{
			// Отключение проверки на слабый ключ
			desEncrypt = new TripleDESCryptoServiceProvider() { Key = desEncrypt.Key, Mode = CipherMode.CBC };
			// Установка ключа и режима шифрования
			desEncrypt.Key = StringToByteArray(key1);
			desEncrypt.IV = new byte[desEncrypt.BlockSize / 8]; // Установка вектора инициализации
		}

		var desDecrypt = new TripleDESCryptoServiceProvider();

		if (TripleDES.IsWeakKey(desEncrypt.Key))
		{
			// Отключение проверки на слабый ключ
			desDecrypt = new TripleDESCryptoServiceProvider() { Key = desDecrypt.Key, Mode = CipherMode.CBC };
			// Установка ключа и режима шифрования
			desDecrypt.Key = StringToByteArray(key1); // Используем тот же ключ, что и для шифрования
			desDecrypt.IV = new byte[desDecrypt.BlockSize / 8]; // Установка вектора инициализации
		}

		// Преобразование исходного текста в байты
		byte[] data = ASCIIEncoding.ASCII.GetBytes(original);

		// Шифрование данных
		ICryptoTransform encryptor = desEncrypt.CreateEncryptor();
		byte[] encData = encryptor.TransformFinalBlock(data, 0, data.Length);

		// Вывод зашифрованных данных
		Console.WriteLine("Зашифрованные данные: " + BitConverter.ToString(encData));

		// Дешифрование данных
		ICryptoTransform decryptor = desDecrypt.CreateDecryptor();
		byte[] decData = decryptor.TransformFinalBlock(encData, 0, encData.Length);

		// Вывод дешифрованных данных
		Console.WriteLine("Дешифрованные данные: " + ASCIIEncoding.ASCII.GetString(decData));
	}

	public static byte[] StringToByteArray(string hex)
	{
		int NumberChars = hex.Length;
		byte[] bytes = new byte[NumberChars / 2];
		for (int i = 0; i < NumberChars; i += 2)
			bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
		return bytes;
	}
}
