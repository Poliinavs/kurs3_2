using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static int CalculateGCD(int a, int b)
    {
        while (a != 0 && b != 0)
        {
            if (a > b)
                a = a % b;
            else
                b = b % a;
        }
        return a + b;
    }

    static int CalculateGCDOfThree(int a, int b, int c)
    {
        return CalculateGCD(CalculateGCD(a, b), c);
    }

    static List<int> FindPrimesInRange(int min, int max)
    {
        List<int> primes = new List<int>();
        int[] sieve = new int[max + 1];

        for (int i = 0; i <= max; i++)
            sieve[i] = 1;

        for (int i = 2; i <= Math.Sqrt(max); i++)
        {
            if (sieve[i] == 1)
            {
                int j = 2;
                while (i * j <= max)
                {
                    sieve[i * j] = 0;
                    j++;
                }
            }
        }

        for (int i = 2; i <= max; i++)
        {
            if (sieve[i] == 1)
                primes.Add(i);
        }

        primes.RemoveAll(num => num < min);
        return primes;
    }

    static string GetPrimeFactors(int n)
    {
        var factors = new Dictionary<int, int>();
        int divisor = 2;

        while (divisor * divisor <= n)
        {
            if (n % divisor == 0)
            {
                if (factors.ContainsKey(divisor))
                {
                    factors[divisor]++;
                }
                else
                {
                    factors.Add(divisor, 1);
                }
                n /= divisor;
            }
            else
            {
                divisor++;
            }
        }

        if (n > 1)
        {
            if (factors.ContainsKey(n))
            {
                factors[n]++;
            }
            else
            {
                factors.Add(n, 1);
            }
        }

        var factorStrings = factors.Select(kvp => kvp.Value > 1 ? $"{kvp.Key}^{kvp.Value}" : kvp.Key.ToString());
        return $"{n} = " + string.Join(" * ", factorStrings);
    }

    static bool IsNumberPrime(int n)
    {
        int divisorCount = 0;

        for (int i = 2; i <= n / 2; i++)
        {
            if (n % i == 0)
                divisorCount++;
        }

        return divisorCount <= 0;
    }

    static void Main()
    {
        int firstNumber = 450;
        int secondNumber = 503;

        Console.WriteLine("НОД двух чисел");
        Console.Write("Введите первое число: ");
        int userInputA = int.Parse(Console.ReadLine());
        Console.Write("Введите второе число: ");
        int userInputB = int.Parse(Console.ReadLine());
        Console.WriteLine(CalculateGCD(userInputA, userInputB));

        Console.WriteLine("\nНОД трех чисел");
        Console.Write("Введите первое число: ");
        userInputA = int.Parse(Console.ReadLine());
        Console.Write("Введите второе число: ");
        userInputB = int.Parse(Console.ReadLine());
        Console.Write("Введите третье число: ");
        int userInputC = int.Parse(Console.ReadLine());
        Console.WriteLine(CalculateGCDOfThree(userInputA, userInputB, userInputC));

        Console.WriteLine("\nПоиск простых чисел");
        Console.Write("Введите нижнюю границу: ");
        int userMin = int.Parse(Console.ReadLine());
        Console.Write("Введите верхнюю границу: ");
        int userMax = int.Parse(Console.ReadLine());

        List<int> primeNumbers = FindPrimesInRange(userMin, userMax);
        Console.WriteLine(string.Join(", ", primeNumbers));
        Console.WriteLine($"Количество простых чисел в указанном интервале: {primeNumbers.Count}");
        Console.WriteLine($"{userMax}/ln({userMax}): {userMax / Math.Log(userMax)}");

        Console.WriteLine($"\nПоиск простых чисел в интервале [2, {secondNumber}]");
        primeNumbers = FindPrimesInRange(2, secondNumber);
        Console.WriteLine($"Количество простых чисел в указанном интервале: {primeNumbers.Count}");
        Console.WriteLine($"{secondNumber}/ln({secondNumber}): {secondNumber / Math.Log(secondNumber)}");

        Console.WriteLine($"\nПоиск простых чисел в интервале [{firstNumber}, {secondNumber}]");
        primeNumbers = FindPrimesInRange(firstNumber, secondNumber);
        Console.WriteLine($"Количество простых чисел в указанном интервале: {primeNumbers.Count}");

        Console.WriteLine("\nЧисла в виде произведения простых множителей");
        Console.WriteLine($"Простые множители {secondNumber}: {GetPrimeFactors(secondNumber)}");
        Console.WriteLine($"Простые множители  {firstNumber} : : {GetPrimeFactors(firstNumber)}");

        int concatenatedNumber = int.Parse(string.Concat(firstNumber.ToString(), secondNumber.ToString()));
        Console.WriteLine($"\nЯвляется ли число, состоящее из конкатенации цифр {firstNumber} || {secondNumber} простым: {IsNumberPrime(concatenatedNumber)}");

        Console.WriteLine($"\nНОД ({firstNumber}, {secondNumber}): {CalculateGCD(firstNumber, secondNumber)}");
        Console.ReadLine();
    }
}
