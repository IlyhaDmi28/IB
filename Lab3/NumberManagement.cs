using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Lab3
{
    public static class NumberManagement
    {
        public static int GCD(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if(a > b) a = a % b;
                else b = b % a;
            }

            return a + b;
        }

        public static int GCD(int a, int b, int c)
        {
            return GCD(GCD(a, b), c);
        }


        public static List<int> SieveEratosthenes(int m, int n)
        {
            bool[] prime = new bool[n + 1];

            for (int i = 0; i <= n; i++)
                prime[i] = true;

            for (int p = 2; p * p <= n; p++)
            {
                if (prime[p] == true)
                {
                    for (int i = p * p; i <= n; i += p)
                        prime[i] = false;
                }
            }

            List<int> primeNumbers = new List<int>();


            for (int i = 0; i <= n; i++)
            {
                if (prime[i] == true && i >= m)
                {
                    primeNumbers.Add(i);
                }
            }

            return primeNumbers;
        }

        public static string GetCanonicalForm(int number)
        {
            List<int> primeNumers = new List<int>();

            for (int divider = 2; divider * divider <= number; divider++)
            {
                while (number % divider == 0)
                {
                    primeNumers.Add(divider);
                    number /= divider;
                }
            }

            if (number > 1)
            {
                primeNumers.Add(number);
            }

            Dictionary<int, int> powers = new Dictionary<int, int>();

            foreach (int n in primeNumers) powers[n] = powers.ContainsKey(n) ? powers[n] + 1 : 1;

            string[] canonicalForm = new string[powers.Count];


            int i = 0;
            foreach (var n in powers)
            {
                if (n.Value != 1) canonicalForm[i] = $"{n.Key}^{n.Value}";
                else canonicalForm[i] = $"{n.Key}";

                i++;
            }


            return string.Join(" * ", canonicalForm);
        }

        public static bool IsConcatNumberPrime(int n, int m)
        {
            int concatNumber = Convert.ToInt32(n.ToString() + m.ToString());

            List<int> primeNumbers = SieveEratosthenes(0, concatNumber);

            return primeNumbers.Contains(concatNumber);
        }

    }
}
