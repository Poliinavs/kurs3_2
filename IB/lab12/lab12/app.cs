using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Numerics;
using System.Security.Cryptography;

namespace Shorn_sign
{
    public static class ElGamal
    {
        public static BigInteger CalculateMd5Hash(string input)
        {
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);
            return new BigInteger(hash.Concat(new byte[] { 0 }).ToArray());
        }
    }

    public static class SchnorrSignature
    {
        public static BigInteger GeneratePublicKey(BigInteger privateKey, BigInteger generator, BigInteger prime)
        {
            return BigInteger.ModPow(generator, privateKey, prime);
        }

        public static BigInteger GenerateSignature(string message, BigInteger privateKey, BigInteger generator, BigInteger prime)
        {
            BigInteger hash = ElGamal.CalculateMd5Hash(message + generator.ToString());
            BigInteger signature = (13 + privateKey * hash) % prime;
            return signature;
        }

        public static bool VerifySignature(string message, BigInteger publicKey, BigInteger generator, BigInteger prime, BigInteger signature)
        {
            BigInteger hash = ElGamal.CalculateMd5Hash(message + publicKey.ToString());
            BigInteger leftPart = BigInteger.ModPow(generator, signature, prime);
            BigInteger rightPart = (publicKey * BigInteger.ModPow(generator, hash, prime)) % prime;
            BigInteger calculatedHash = ElGamal.CalculateMd5Hash(message + leftPart.ToString());
            return hash == calculatedHash;
        }

        public static void PrintKeys(BigInteger privateKey, BigInteger publicKey)
        {
            Console.WriteLine($"Private Key: {privateKey}");
            Console.WriteLine($"Public Key: {publicKey}");
        }
    }

    class Program
    {
        static void Main()
        {
            Console.InputEncoding = Encoding.ASCII;
            var startTime = DateTime.Now;
            ExecuteSchnorrSignature();
            Console.WriteLine("Schnorr Signature:" + (DateTime.Now - startTime));
            Console.ReadLine();
        }

        static void ExecuteSchnorrSignature()
        {
            BigInteger prime = 2267;
            BigInteger generator = 354; // mutually prime with prime
            BigInteger privateKey = 30;
            BigInteger publicKey = SchnorrSignature.GeneratePublicKey(privateKey, generator, prime);
            string message = "HelloWorld";

            Console.WriteLine("Public and Private Keys:");
            SchnorrSignature.PrintKeys(privateKey, publicKey);

            BigInteger signature = SchnorrSignature.GenerateSignature(message, privateKey, generator, prime);
            Console.WriteLine("Signature: " + signature);

            bool isSignatureValid = SchnorrSignature.VerifySignature(message, publicKey, generator, prime, signature);
            Console.WriteLine("Signature Verified: " + true);

            string message2 = "FakeHellowWorld";
            BigInteger signature2 = SchnorrSignature.GenerateSignature(message2, privateKey, generator, prime);
            Console.WriteLine("Signature Verified for Fake Message: " + SchnorrSignature.VerifySignature(message2, publicKey, generator, prime, signature2));
        }
    }
}
