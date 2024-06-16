using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Text;


public class Crypt
{
    public static List<BigInteger> GeneratePrivateKey(BigInteger initialNumber, int quantityOfNumbers)
    {
        List<BigInteger> sequence = new List<BigInteger>();
        BigInteger term = initialNumber;
        for (int i = 0; i < quantityOfNumbers; i++)
        {
            sequence.Add(term);
            term += initialNumber;
            initialNumber <<= 1;
        }

        return sequence;
    }


    public static List<BigInteger> GeneratePublicKey(List<BigInteger> privateKey, BigInteger a, BigInteger n)
    {
        var sum = Sum(privateKey);
        if (n <= sum)
            throw new ArgumentException("n should be more than sum of all numbers in private key.");
        if (!AreRelativelyPrime(a, n))
            throw new ArgumentException("a and n should be coprime.");

        var publicKey = new List<BigInteger>();
        foreach (BigInteger d in privateKey)
        {
            BigInteger e = d * a % n;
            publicKey.Add(e);
        }

        return publicKey;
    }

    public static List<BigInteger> Encrypt(List<BigInteger> publicKey, byte[] plaintext)
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();
        var encryptedList = new List<BigInteger>();

        foreach (byte b in plaintext)
        {
            string binaryString = Convert.ToString(b, 2).PadLeft(8, '0');

            var positions = new List<int>();
            for (int i = 0; i < binaryString.Length; i++)
                if (binaryString[i] == '1')
                    positions.Add(i);

            var sum = BigInteger.Zero;
            foreach (int position in positions)
                if (position < publicKey.Count)
                    sum += publicKey[position];

            encryptedList.Add(sum);
        }

        stopWatch.Stop();
        return encryptedList;
    }


    public static byte[] Decrypt(List<BigInteger> privateKey, List<BigInteger> encryptedText, BigInteger a, BigInteger n)
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();
        var decryptedBytes = new List<byte>();
        BigInteger inverse = GetInverseNumber(a, n);

        foreach (BigInteger cipher in encryptedText)
        {
            BigInteger decryptedValue = (cipher * inverse) % n;
            var binaryString = ReverseString(GetBinaryRepresentation(decryptedValue, privateKey));
            byte decryptedByte = Convert.ToByte(binaryString, 2);
            decryptedBytes.Add(decryptedByte);
        }

        stopWatch.Stop();
        return decryptedBytes.ToArray();
    }

    private static string ReverseString(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }


    public static BigInteger GenerateRandomNumber(int n)
    {
        var random = new Random();
        BigInteger randomNumber = BigInteger.Zero;

        while (randomNumber == BigInteger.Zero)
        {
            byte[] bytes = new byte[(n + 7) / 8];
            random.NextBytes(bytes);

            var numExtraBits = n % 8;
            if (numExtraBits > 0)
                bytes[bytes.Length - 1] &= (byte)(255 >> ((bytes.Length * 8) - n));
            randomNumber = new BigInteger(bytes);
        }

        return randomNumber;
    }

    public static BigInteger GenerateCoprime(BigInteger n)
    {
        var random = new Random();
        int bitCount = (int)Math.Ceiling(BigInteger.Log(n, 2)); 
        int byteCount = (int)Math.Ceiling(bitCount / 8.0); 

        while (true)
        {
            BigInteger number = GenerateRandomNumber(bitCount, random);
            if (BigInteger.GreatestCommonDivisor(number, n) == 1)
                return number;
        }
    }
    public static string GetBinaryRepresentation(BigInteger number, List<BigInteger> privateKey)
    {
        var binaryString = new StringBuilder();

        for (int i = privateKey.Count - 1; i >= 0; i--)
        {
            if (number >= privateKey[i])
            {
                binaryString.Append("1");
                number -= privateKey[i];
            }
            else
                binaryString.Append("0");
        }

        return binaryString.ToString();
    }

    public static BigInteger GetInverseNumber(BigInteger number, BigInteger modulus)
    {
        BigInteger m0 = modulus;
        BigInteger y = 0, x = 1;

        if (modulus == 1)
            return 0;

        while (number > 1)
        {
            BigInteger quotient = BigInteger.Divide(number, modulus);
            BigInteger temp = modulus;

            modulus = BigInteger.Remainder(number, modulus);
            number = temp;

            temp = y;
            y = x - quotient * y;
            x = temp;
        }

        if (x < 0)
            x += m0;

        return x;
    }

    public static BigInteger Sum(List<BigInteger> numbers)
    {
        BigInteger sum = 0;
        foreach (BigInteger number in numbers)
            sum += number;
        return sum;
    }

    private static bool AreRelativelyPrime(BigInteger a, BigInteger b)
    {
        BigInteger gcd = BigInteger.GreatestCommonDivisor(a, b);
        return gcd == 1;
    }

    private static BigInteger GenerateRandomNumber(int bitLength, Random random)
    {
        var bytes = new byte[bitLength / 8];
        random.NextBytes(bytes);
        bytes[bytes.Length - 1] &= 0x7F;
        return new BigInteger(bytes);
    }
}
