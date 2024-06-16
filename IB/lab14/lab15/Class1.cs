using System;
using System.Collections.Generic;
using System.Text;
using Aspose.Words;

namespace SteganographyExample
{
    class Program
    {
        const string InputFilePath = "C:\\instit\\kurs3_2\\IB\\lab15\\lab15\\in.docx";
        const string OutputFilePath = "C:\\instit\\kurs3_2\\IB\\lab15\\lab15\\out.docx";
        const double BaseSpacing = 10;
        const double MinSpacing = -10;
        const double MaxSpacing = 10;

        static void Main(string[] args)
        {
            string message = "Polina";

            try
            {
                HideMessageInDocument(InputFilePath, OutputFilePath, message);
                Console.WriteLine("Сообщение скрыто в документе.");

                string decryptedMessage = ExtractMessageFromDocument(OutputFilePath);
                Console.WriteLine("Расшифрованное сообщение: " + decryptedMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }

            Console.ReadLine();
        }

        static void HideMessageInDocument(string inputFilePath, string outputFilePath, string message)
        {
            Document document = new Document(inputFilePath);

            NodeCollection paragraphs = document.GetChildNodes(NodeType.Paragraph, true);
            double deltaSpacing = Math.Abs(MaxSpacing - MinSpacing);
            string binaryMessage = StringToBinary(message);

            if (paragraphs.Count < binaryMessage.Length)
            {
                throw new Exception("Недостаточно параграфов для размещения сообщения.");
            }

            for (int i = 0; i < binaryMessage.Length; i++)
            {
                Paragraph paragraph = (Paragraph)paragraphs[i];
                char currentChar = binaryMessage[i];
                double spacing = BaseSpacing + (currentChar == '1' ? deltaSpacing : 0);

                paragraph.ParagraphFormat.SpaceAfter = spacing;
            }

            document.Save(outputFilePath);
        }

        static string ExtractMessageFromDocument(string outputFilePath)
        {
            Document encryptedDocument = new Document(outputFilePath);
            StringBuilder decryptedMessageBuilder = new StringBuilder();

            foreach (Paragraph paragraph in encryptedDocument.GetChildNodes(NodeType.Paragraph, true))
            {
                double spacing = paragraph.ParagraphFormat.SpaceAfter;
                char decryptedChar = spacing > BaseSpacing ? '1' : '0';

                decryptedMessageBuilder.Append(decryptedChar);
            }

            return BinaryToString(decryptedMessageBuilder.ToString());
        }

        static string StringToBinary(string data)
        {
            StringBuilder binaryBuilder = new StringBuilder();
            foreach (char c in data)
            {
                string binaryChar = Convert.ToString(c, 2).PadLeft(8, '0');
                binaryBuilder.Append(binaryChar);
            }
            return binaryBuilder.ToString();
        }

        static string BinaryToString(string data)
        {
            List<byte> byteList = new List<byte>();
            for (int i = 0; i < data.Length; i += 8)
            {
                string binaryByte = data.Substring(i, 8);
                byte byteValue = Convert.ToByte(binaryByte, 2);
                byteList.Add(byteValue);
            }
            return Encoding.ASCII.GetString(byteList.ToArray());
        }
    }
}
