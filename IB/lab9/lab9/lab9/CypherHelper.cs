using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;


public class CypherHelper
{
    const string pathToFolder = "C:\\instit\\kurs3_2\\IB\\TEMP\\KMZI_Lab9\\";
    const string fileNameOpen = "open_text.txt";
    const string fileNameEncrypt = "encrypt.txt";
    const string fileNameDecrypt = "decrypt.txt";


    // Запись текста в файл
    public static bool WriteToFile(byte[] text, string fileName = fileNameEncrypt)
    {
        var filePath = Path.Combine(pathToFolder, fileName);
        try
        {
            File.WriteAllBytes(filePath, text);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }


    // Чтение текста из файла
    public static byte[] ReadFromFile(string fileName = fileNameDecrypt)
    {
        var filePath = Path.Combine(pathToFolder, fileName);
        return File.ReadAllBytes(filePath);
    }


    // Запись списка чисел в файл
    public static void WriteToFile(List<BigInteger> numbers, string fileName = fileNameEncrypt)
    {
        using (var writer = new StreamWriter(Path.Combine(pathToFolder, fileName)))
        {         
            foreach (BigInteger number in numbers)
                writer.Write(number.ToString() + " ");
        }
    }



    // Получить массив byte[] с исходным текстом
    public static byte[] GetOpenText() => ReadFromFile(fileNameOpen);


    // Конвертация строки в массив byte[]
    public static byte[] GetBytes(string str) => Encoding.UTF8.GetBytes(str);

    // Конвертация массива byte[] в строку
    public static string GetString(byte[] bytes) => Encoding.UTF8.GetString(bytes);

    // Развернуть строку
    public static string ReverseString(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
}