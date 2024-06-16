using System;
using System.Diagnostics;
using System.Numerics;
using System.Security.Cryptography;

public class RSA
{
    public static byte[] Encrypt(byte[] plaintext, RSAParameters publicKey)
    {
        var rsa = new RSACryptoServiceProvider();
        rsa.ImportParameters(publicKey);
        return rsa.Encrypt(plaintext, true);
    }


    public static byte[] Decrypt(byte[] ciphertext, RSAParameters privateKey)
    {
        var rsa = new RSACryptoServiceProvider();
        rsa.ImportParameters(privateKey);
        return rsa.Decrypt(ciphertext, true);
    }



    public static void FirstTask()
    {
        BigInteger a = new BigInteger(20);
        var x = 100000;
        BigInteger n;
        string N = "1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
        BigInteger.TryParse(N, out n);
        for (var i = 0; i < 10; i++)
        {
            FyncY(a, x, n);
            x += 100000;
        }
    }



    public static BigInteger FyncY(BigInteger a, int x, BigInteger n)
    {
        return BigInteger.Pow(a, x) % n;
    }
}
