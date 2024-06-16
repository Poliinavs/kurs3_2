using System;
using System.IO;

namespace KMZI_Lab14
{
    public class Program
    {
        static void Main(string[] args)
        {
            var fileNameOpen = @"C:\instit\kurs3_2\IB\lab13\lab13\img\cat.bmp";
            var fileNameEncryptByRows = @"C:\instit\kurs3_2\IB\lab13\lab13\img\ecnr_rows.bmp";
            var fileNameEncryptByColumns = @"C:\instit\kurs3_2\IB\lab13\lab13\img\ecnr_columns.bmp";
            var fileNameMatrixSample = @"C:\instit\kurs3_2\IB\lab13\lab13\img\matrix_sample.bmp";
            var fileNameMatrixEncryptByRows = @"C:\instit\kurs3_2\IB\lab13\lab13\img\matrix_encr_rows.bmp";
            var fileNameMatrixEncryptByColumns = @"C:\instit\kurs3_2\IB\lab13\lab13\img\matrix_encr_columns.bmp";

            var openTextByRows = "PolinaAvsukevich";
            var openTextByColumns = "Polina";
            /* var openTextByRows = File.ReadAllText("C:\\instit\\kurs3_2\\IB\\lab13\\lab13\\Bigtext.txt");
             var openTextByColumns = File.ReadAllText("C:\\instit\\kurs3_2\\IB\\lab13\\lab13\\Bigtext.txt");*/


            Stenogr.EmbedMessageByRows(fileNameOpen, openTextByRows, fileNameEncryptByRows);
            Stenogr.HideMessageByColumns(fileNameOpen, openTextByColumns, fileNameEncryptByColumns);
            var resultByRows = Stenogr.ExtractMessageByRows(fileNameEncryptByRows);
            var resultByColumns = Stenogr.ExtractMessageByColumns(fileNameEncryptByColumns);

            Console.WriteLine($"Text by rows: {resultByRows.Substring(0, resultByRows.Length - 5)}");
            Console.WriteLine($"Text by columns: {resultByColumns.Substring(0, resultByColumns.Length - 4)}");


            Stenogr.GetColorMatrix(fileNameOpen, fileNameMatrixSample);
            Stenogr.GetColorMatrix(fileNameEncryptByRows, fileNameMatrixEncryptByRows);
            Stenogr.GetColorMatrix(fileNameEncryptByColumns, fileNameMatrixEncryptByColumns);
            Console.ReadLine();
        }
    }
}
