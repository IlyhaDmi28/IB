using Lab3;

int m = 499;
int n = 531;
int c = 229;

Console.WriteLine($"НОД({m}, {n}) = " + NumberManagement.GCD(m, n));
Console.WriteLine($"НОД({m}, {n} и {c}) = " + NumberManagement.GCD(m, n, c));
Console.WriteLine();

Console.WriteLine($"Простые числа от {2} до {n}");
List<int> primeNumbers = NumberManagement.SieveEratosthenes(2, n);
foreach (int number in primeNumbers)
{
    Console.Write(number + " ");
}
Console.WriteLine();
Console.WriteLine("Количесвто простых чисел: " + primeNumbers.Count);
Console.WriteLine("n/ln(n): " + Math.Round((n / Math.Log(n))));
Console.WriteLine();

Console.WriteLine($"Простые числа от {m} до {n}:");
primeNumbers = NumberManagement.SieveEratosthenes(m, n);
foreach (int number in primeNumbers)
{
    Console.Write(number + " ");
}

Console.WriteLine();
Console.WriteLine();


Console.WriteLine($"Числа {m} и {n} в канонической форме:");
Console.WriteLine($"{m} = " + NumberManagement.GetCanonicalForm(m));
Console.WriteLine($"{n} = " + NumberManagement.GetCanonicalForm(n));
Console.WriteLine();

Console.WriteLine($"Число {m}{n} простое: " + NumberManagement.IsConcatNumberPrime(m, n));