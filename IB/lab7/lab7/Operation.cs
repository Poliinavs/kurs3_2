using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab7
{


    public static class Operation
    {
        public static string text = File.ReadAllText("text.txt");

        public static List<string> keys = new List<string>
        {
                "MBCDEFABCDEFAB56", // Произвольный ключ
                 "010101010FFFFFFF",  // Полуслабый ключ
                "0101010101010101", // Слабый ключ
        };

        public static List<string> names = new List<string>
        {
                "Сильный ключ", // Произвольный ключ
                "Полуслабый ключ",  // Полуслабый ключ
                "Слабый ключ", // Слабый ключ
        };
        public static List<string> SplitIntoBlocks(string text, int blockSize)
        {
            var blocks = new List<string>();

            for (int i = 0; i < text.Length; i += blockSize)
            {
                blockSize = Math.Min(blockSize, text.Length - i);
                string block = text.Substring(i, blockSize);
                block = block.PadRight(8, ' ');
                blocks.Add(block);
            }

            return blocks;
        }

        public static byte[] Compress(byte[] data)
        {
            using (var compressedStream = new MemoryStream())
            using (var zipStream = new GZipStream(compressedStream, CompressionMode.Compress))
            {
                zipStream.Write(data, 0, data.Length);
                zipStream.Close();
                return compressedStream.ToArray();
            }
        }
    }
}
