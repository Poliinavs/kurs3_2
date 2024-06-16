using System;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;
class Program
{

    static BigInteger ParamY(BigInteger a, BigInteger x, BigInteger n)
    {
        var pow = a;
        for (BigInteger i = 1; i < x; i++)
        {
            pow *= a;
        }
        return pow % n;
    }

    static bool isPrime(BigInteger n)
    {
        bool isSimple = true;
        for (int i = 2; i < n; i++)
            if (n % i == 0) isSimple = false;
        return isSimple;
    }

    static void Main()
    {
        var a = 3;
        var p = 29;
        var g = 7;
        var x = 10;
        BigInteger n = p * g;
        Random random = new Random();

        for (int i = 0; i < 10; i++)
        {

            BigInteger x1 = random.Next(103, 10100);
            while (!isPrime(x1))
            {
                x1 = random.Next(103, 10100);
            }


            BigInteger y = ParamY(a, x1, n);


            Console.WriteLine("x: " + x1 + " y: " + y );
        }


        var openText = "HelloWorld";
        RSAParameters publicKey;
        RSAParameters privateKey;


        using (var rsa = new RSACryptoServiceProvider(4096))
        {
            publicKey = rsa.ExportParameters(false);
            privateKey = rsa.ExportParameters(true);
        }

        byte[] openTextBytes = Encoding.UTF8.GetBytes(openText);

        var encryptedTextRSA = RSA.Encrypt(openTextBytes, publicKey);
        var decryptedTextRSA = RSA.Decrypt(encryptedTextRSA, privateKey);
        string encryptedTextLetters = "";
        foreach (byte b in encryptedTextRSA)
        {
            if (encryptedTextLetters.Length< 50)
            {
                encryptedTextLetters += (char)b;
            }
        }
        Console.WriteLine($"зашифрованный: {encryptedTextLetters}");
        Console.WriteLine($"расшифрованный: {Encoding.UTF8.GetString(decryptedTextRSA)}");
        var elGamal = new ElGamal(p, g, x);

        var encryptedTextElGamal = elGamal.Encrypt(openTextBytes);
        var decryptedTextElGamal = elGamal.Decrypt(encryptedTextElGamal);

        Console.WriteLine($"зашифрованный: {string.Join(", ", encryptedTextElGamal)}");
        Console.WriteLine($"расшифрованный: {openText}");

        Console.ReadLine();

    }
}