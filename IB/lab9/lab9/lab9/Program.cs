using System;
using System.Text;

class Program
{
    static void Main()
    {
        var z = 8;
        var sizeBits = 100;
        var initialNumber = Crypt.GenerateRandomNumber(sizeBits);
        var privateKey = Crypt.GeneratePrivateKey(initialNumber, z);

        var n = Crypt.Sum(privateKey) + 1;
        var a = Crypt.GenerateCoprime(n);
        var publicKey = Crypt.GeneratePublicKey(privateKey, a, n);

        var text = "Avsukevich";
        byte[] openTextBytes = Encoding.UTF8.GetBytes(text);
        var encrypted = Crypt.Encrypt(publicKey, openTextBytes);
        var decrypted = Crypt.Decrypt(privateKey, encrypted, a, n);

        Console.WriteLine($"зашифрованный: {string.Join(", ", encrypted)}");
        Console.WriteLine($"расшифрованный: {Encoding.UTF8.GetString(decrypted)}");

        Console.WriteLine("\nПриватный ключ");
        foreach (var item in privateKey)
            Console.WriteLine(item);

        Console.WriteLine("\nПубличный ключ");
        foreach (var item in publicKey)
            Console.WriteLine(item);
        Console.ReadLine();
    }
}
