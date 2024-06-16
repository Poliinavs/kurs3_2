using System;
using System.Diagnostics;

namespace lab8
{

    public class RC4
    {
        private int n;
        private int mod;

        public RC4(int n)
        {
            this.n = n;
            mod = (int)Math.Pow(2, n);
        }

        public byte[] InitializeSBox(byte[] key)
        {
            var j = 0;
            var sBlock = new byte[mod];

            for (var i = 0; i < mod; i++)
                sBlock[i] = (byte)i;

            for (var i = 0; i < mod; i++)
            {
                j = (j + key[i % key.Length] + sBlock[i]) % mod;
                Swap(sBlock, i, j);
            }

            return sBlock;
        }

        public byte[] GenerateKeyStream(byte[] sBlock, int length)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var i = 0;
            var j = 0;
            var keyStream = new byte[length];

            for (var k = 0; k < length; k++)
            {
                i = (i + 1) % mod;
                j = (j + sBlock[i]) % mod;
                Swap(sBlock, i, j);
                keyStream[k] = sBlock[(sBlock[i] + sBlock[j]) % mod];
            }

            stopWatch.Stop();
            Console.WriteLine($"Время:\t{stopWatch.ElapsedTicks} ");
            return keyStream;
        }

        public byte[] Encrypt(byte[] data, byte[] keyStream)
        {
            var encryptedData = new byte[data.Length];

            for (var i = 0; i < data.Length; i++)
                encryptedData[i] = (byte)(data[i] ^ keyStream[i]);

            return encryptedData;
        }

        public byte[] Decrypt(byte[] data, byte[] keyStream) => Encrypt(data, keyStream);

        private static void Swap(byte[] array, int i, int j)
        {
            (array[j], array[i]) = (array[i], array[j]);
        }
    }
}
