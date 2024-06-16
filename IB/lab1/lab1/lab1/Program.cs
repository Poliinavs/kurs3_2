//kirl Pol:
//lat венгерский: 
class Program
{
    static void Main()
    {
        string[] filePaths = new string[]
        {
            @"C:\instit\kurs3_2\IB\lab1\lab1\lab1\Lenuages\pol.txt",
            @"C:\instit\kurs3_2\IB\lab1\lab1\lab1\Lenuages\ven.txt"
        };

        string[] FIO = new string[]
      {
            "AwsiukiewiczPolinaWadimowna",
            "AvsjukevicsPolinaVadimovna"
      };
        int i = 0;

        foreach (string path in filePaths)
        {
            Console.WriteLine($"File: {path}");
            string text = TextReader(path);

            Dictionary<char, int> lettersDict = LettersDict(text);
            Dictionary<char, double> lettersProbs = Probs(text);
            double entropyValue = Entropy(text);


            Console.WriteLine($"\nEntropy: {entropyValue}");

            string binaryText = ConvertToAscii(FIO[i]);
            double binaryEntropy = Entropy(binaryText);

            Console.WriteLine($"\nBinary Entropy: {binaryEntropy}");

            // верроятность ошибки по каналу 

            double[] errorProbabilities = { 0, 0.1, 0.5, 1.0 };
            double[] errorProbabilities1 = { 0.1, 0.5, 1.0 };

            double amount = CalculateAmountInformation(entropyValue, FIO[i]);
            Console.WriteLine($"Количество информации для {FIO[i]}: {amount}");

            amount = CalculateAmountInformation(binaryEntropy, binaryText);
            Console.WriteLine($"Количество информации для ASCI binaryText: {amount}");

            foreach (double errorProbability in errorProbabilities)
            {
                double information1 = CalculateInformation(FIO[i], errorProbability);
                Console.WriteLine($" с вероятностью ошибки {errorProbability}: {information1} бит");
            }
            Console.WriteLine($"Количество информации для ASCII кода {FIO[i]}");
            foreach (double errorProbability in errorProbabilities)
            {
                double information3 = CalculateInformation(binaryText, errorProbability);
                Console.WriteLine($"c вероятностью ошибки {errorProbability}: {information3} бит");
            }
            i++;


            /*  double[] mistakeProbs = { 0, 0.1, 0.5, 1 };

              Console.WriteLine($"\nMistake Quantity for Mistake Probability {mistakeProbs[0]}: {QuantityOfInformation(mistakeProbs[0], binaryText)}");
              Console.WriteLine($"\nMistake Quantity for Mistake Probability {mistakeProbs[1]}: {QuantityOfInformation(mistakeProbs[1], binaryText)}");
              Console.WriteLine($"\nMistake Quantity for Mistake Probability {mistakeProbs[2]}: {QuantityOfInformation(mistakeProbs[2], binaryText)}");
              Console.WriteLine($"\nMistake Quantity for Mistake Probability {mistakeProbs[3]}: {QuantityOfInformation(mistakeProbs[3], binaryText)}");*/
        }
    }

    static double CalculateAmountInformation(double entropy, string text)
    {
        return entropy * text.Length;
    }

    static double CalculateInformation(string message, double errorProbability)
    {
        if (!IsBinary(message))
        {
            return 0;
        }
        return message.Length * GetEffectiveEntropy(errorProbability);

    }
    static bool IsBinary(string message)
    {
        foreach (char c in message)
        {
            if (c != '0' && c != '1')
            {
                return false;
            }
        }
        return true;
    }
    static double GetEffectiveEntropy(double error)
    {
        double conditionEntropy = -1 * (error * Math.Log(error, 2) + (1 - error) * Math.Log(1 - error, 2));
        if (error == 1 || error == 0)
            conditionEntropy = 0;
        double effectEntropy = 1 - conditionEntropy;
        return effectEntropy;
    }

    static double QuantityOfInformation(double mistakeProb, string text)
    {
        try
        {
          /*  if (mistakeProb == 0.0 || mistakeProb == 1.0)
            {
                return 0.0;
            }
            if (mistakeProb == 0.5)
            {
                return text.Length * (1 - (-mistakeProb * Math.Log(mistakeProb, 2) - (1 - mistakeProb) * Math.Log(1 - mistakeProb, 2)));
            }*/
          /*  if (mistakeProb == 1) 
            {
                return 0;
            }*/

            return text.Length * (1 - (-mistakeProb * Math.Log(mistakeProb, 2) - (1 - mistakeProb) * Math.Log(1 - mistakeProb, 2)));
        }
        catch (Exception)
        {
            return 0.0;
        }
    }



    static string ConvertToAscii(string text)
    {
        string asciiText = "";
        foreach (char ch in text)
        {
            if (char.IsLetter(ch))
            {
                asciiText += Convert.ToString(ch, 2);
            }
        }
        return asciiText;
    }



    static string TextReader(string path)
    {
        string text;
        using (StreamReader reader = new StreamReader(path))
        {
            text = reader.ReadToEnd().ToLower();
        }
        return text;
    }

    static Dictionary<char, int> LettersDict(string text)//число вхождений
    {
        Dictionary<char, int> lettersDict = new Dictionary<char, int>();
        foreach (char ch in text)
        {
            if (char.IsLetterOrDigit(ch))
            {
                if (lettersDict.ContainsKey(ch))
                    lettersDict[ch]++;
                else
                    lettersDict[ch] = 1;
            }
        }
        return lettersDict;
    }

    static Dictionary<char, double> Probs(string text) //вероятность выбрать символ 
    {
        Dictionary<char, int> lettersDict = LettersDict(text);
        Dictionary<char, double> lettersProbs = new Dictionary<char, double>();
        int totalChars = lettersDict.Values.Sum();

        foreach (var kvp in lettersDict)
        {
            lettersProbs[kvp.Key] = (double)kvp.Value / totalChars;
        }

        return lettersProbs;
    }

    static double Entropy(string text)
    {
        Dictionary<char, double> lettersProbs = Probs(text);
        double entropy = 0;

        foreach (var prob in lettersProbs.Values)
        {
            entropy += prob * Math.Log(prob, 2);
        }

        return -entropy;
    }
}
