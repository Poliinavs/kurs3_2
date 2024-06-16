using System;
using System.Numerics;

namespace Ell_Gamal_Signature
{
    class EllGamal
    {
        public static int ModuloInverse(int a, int n)
        {
            int result = 0;
            for (int i = 0; i < 10000; i++)
            {
                if (((a * i) % n) == 1) return (i);
            }
            return (result);
        }

        public static Tuple<int, int> GenerateSignature(int p, int g, int x, int k, int message)
        {
            int y = (int)BigInteger.ModPow(g, x, p);
            int a = (int)BigInteger.ModPow(g, k, p);
            int m = p - 1;
            int kInv = ModuloInverse(k, m);
            int b = (int)BigInteger.ModPow((kInv * (message - (x * a) % m) % m) % m, 1, m);
            return Tuple.Create(a, b);
        }

        public static bool VerifySignature(int p, int g, int y, int a, int b, int message)
        {
            BigInteger ya = BigInteger.ModPow(y, a, p);
            BigInteger ab = BigInteger.ModPow(a, b, p);
            BigInteger pr1 = BigInteger.ModPow(ya * ab, 1, p);
            BigInteger pr2 = BigInteger.ModPow(g, message, p);
            return pr1 == pr2;
        }

        static void Main(string[] args)
        {
            int prime = 2137;      // Prime number
            int generator = 2127;  // Generator with order < prime
            int privateKey = 1116; // Private key < prime
            int publicKey = (int)BigInteger.ModPow(generator, privateKey, prime);

            int random = 7;     // Co-prime with prime - 1

            Console.WriteLine($" Prime={prime}\n Generator={generator}\n Private Key={privateKey}\n Public Key={publicKey}\n Random={random}\n");

            int message = 2119;
            var signature = GenerateSignature(prime, generator, privateKey, random, message);
            Console.WriteLine($" Message={message}\n Signature S={signature.Item1},{signature.Item2}");
            bool verified = VerifySignature(prime, generator, publicKey, signature.Item1, signature.Item2, message);
            if (verified)
            {
                Console.WriteLine(" Verification successful");
            }
            else
            {
                Console.WriteLine(" Verification failed");
            }
            Console.ReadKey();
        }
    }
}
