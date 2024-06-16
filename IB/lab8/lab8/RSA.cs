using lab8;
using System;
using System.Diagnostics;
using System.Numerics;
using System.Text;

public class RSARandomGener
{
    private BigInteger p;
    private BigInteger q;
    private BigInteger n;
    private BigInteger e;
    private BigInteger x;

    public RSARandomGener(BigInteger p, BigInteger q, BigInteger e, BigInteger x)
    {
        this.p = p;
        this.q = q;
        this.n = p * q;
        this.e = e;
        this.x = x;
    }

    public byte GenerateRandomBit()
    {
        x = BigInteger.ModPow(x, e, n);
        byte randomBit = (byte)(x % 2);
        return randomBit;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        BigInteger p = BigInteger.Parse("57896044618658097711785492504343953926634992332820282019728792003956564819967");
        BigInteger q = BigInteger.Parse("76743985164523602061355291354880921859389331661131769782895224264006620558367");
        BigInteger e = BigInteger.Parse("65537");
        BigInteger x0 = BigInteger.Parse("12345");

        RSARandomGener generator = new RSARandomGener(p, q, e, x0);

        Console.WriteLine("RSA");
        for (int i = 0; i < 10; i++)
        {
            byte randomBit = generator.GenerateRandomBit();
            Console.Write(randomBit);
        }

        Console.WriteLine();
        Console.WriteLine("RC4");

        int n = 8;
        var RC4 = new RC4(n);

        var key = new byte[] { 121, 14, 89, 15 };
        var openText = "Hello";

        var sBlock = RC4.InitializeSBox(key);
        var keyStream = RC4.GenerateKeyStream(sBlock, openText.Length);
        var encryptedText = RC4.Encrypt(Encoding.UTF8.GetBytes(openText), keyStream);
        var decryptedText = RC4.Decrypt(encryptedText, keyStream);

        Console.WriteLine($"зашифрованный:{Encoding.UTF8.GetString(encryptedText)}");
        Console.WriteLine($"расшифрованный:{Encoding.UTF8.GetString(decryptedText)}");

        Console.ReadLine();
           
          
    }
}
