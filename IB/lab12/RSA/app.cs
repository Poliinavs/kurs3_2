using System;
using System.Collections.Generic;
using System.Numerics;

namespace RSA_Signature
{
    class RSA
    {
        public static readonly char[] ValidCharacters = new char[] { '#', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-' };

        public bool IsPrime(long n)
        {
            if (n < 2) return false;
            if (n == 2) return true;
            for (long i = 2; i < n; i++)
                if (n % i == 0) return false;
            return true;
        }

        public int CalculatePublicKey(int privateKey, int totient)
        {
            var publicKey = 10; // Default value for public key
            while (true)
            {
                if ((publicKey * privateKey) % totient == 1) break;
                else publicKey++;
            }
            return publicKey;
        }

        public int CalculatePrivateKey(int totient)
        {
            var privateKey = totient - 1;
            for (int i = 2; i <= totient; i++)
                if ((totient % i == 0) && (privateKey % i == 0))
                {
                    privateKey--;
                    i = 1;
                }
            return privateKey;
        }

        public List<string> Encode(string hash, int publicKey, int modulus)
        {
            var encodedResult = new List<string>();
            BigInteger encodedCharacter;
            foreach (char character in hash)
            {
                int index = Array.IndexOf(ValidCharacters, character);
                encodedCharacter = new BigInteger(index);
                encodedCharacter = BigInteger.Pow(encodedCharacter, publicKey);
                BigInteger modulusBigInt = new BigInteger(modulus);
                encodedCharacter = encodedCharacter % modulusBigInt;
                encodedResult.Add(encodedCharacter.ToString());
            }
            return encodedResult;
        }

        public string Decode(List<string> input, int privateKey, int modulus)
        {
            try
            {
                string decodedResult = "";
                BigInteger decodedCharacter;
                foreach (string item in input)
                {
                    decodedCharacter = new BigInteger(Convert.ToDouble(item));
                    decodedCharacter = BigInteger.Pow(decodedCharacter, privateKey);
                    BigInteger modulusBigInt = new BigInteger(modulus);
                    decodedCharacter = decodedCharacter % modulusBigInt;
                    int index = Convert.ToInt32(decodedCharacter.ToString());
                    decodedResult += ValidCharacters[index].ToString();
                }
                return decodedResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred during decoding: " + ex.Message);
                return "";
            }
        }

        public List<string> CreateSignature(string hash, int privateKey, int modulus)
        {
            return Encode(hash, privateKey, modulus);
        }

        public bool VerifySignature(List<string> signature, string hash, int publicKey, int modulus)
        {
            string decodedHash = Decode(signature, publicKey, modulus);
            return decodedHash == hash;
        }

        public void PrintKeys(int privateKey, int publicKey)
        {
            Console.WriteLine($"Private Key: {privateKey}");
            Console.WriteLine($"Public Key: {publicKey}");
        }
    }

    class Program
    {
        public static readonly char[] ValidCharacters = new char[] { '#', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-' };

        static void Main(string[] args)
        {
            var rsa = new RSA();
            string message = "HelloWorld";
            int prime1 = 101;
            int prime2 = 103;

            string hash = message.GetHashCode().ToString();
            int modulus = prime1 * prime2;
            int totient = (prime1 - 1) * (prime2 - 1);
            int privateKey = rsa.CalculatePrivateKey(totient);
            int publicKey = rsa.CalculatePublicKey(privateKey, totient);

            rsa.PrintKeys(privateKey, publicKey); // Print keys

            Console.WriteLine($" Modulus = {modulus}\n Totient = {totient}\n Message = {message}\n");

            List<string> signature = rsa.CreateSignature(h
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                ash, privateKey, modulus); // Create signature

            Console.WriteLine("Signature:");
            foreach (var item in signature)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();

            while (true)
            {
                Console.ReadKey();
                {
                    List<string> input = new List<string>();
                    string hash2 = message.GetHashCode().ToString();

                    bool isValid = rsa.VerifySignature(signature, hash2, publicKey, modulus); // Verify signature
                    Console.WriteLine($"Is signature valid? {isValid}");

                    if (isValid)
                        Console.WriteLine("File is authentic. Signature is valid.\n");
                    else
                        Console.WriteLine("Attention! File is NOT authentic!!!\n");
                }
            }
        }
    }
}
