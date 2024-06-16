using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        string text = "Avsukevich Polina";
        Console.WriteLine("Хеширование MD");
        string hash = CalculateMD5Hash(text);
        Console.WriteLine($"Текст: {text}\nХэш: {hash}");

        Console.ReadKey();
    }

    static string CalculateMD5Hash(string input)
    {
        using (MD5 md5Hash = MD5.Create())
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            return ToHex(data);
        }
    }
    static string ToHex(byte[] bytes)
    {
        StringBuilder hex = new StringBuilder(bytes.Length * 2);
        foreach (byte b in bytes)
        {
            hex.AppendFormat("{0:x2}", b);
        }
        return hex.ToString();
    }
}
