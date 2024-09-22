using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab2
{
    public class Coding
    {

        public static string ReadTextFromFile(string filename)
        {
            string text = "";

            string pathToFolder = "../../../Texts/";
            string filepath = Path.Combine(pathToFolder, filename);

            if (File.Exists(filepath))
            {
                text = File.ReadAllText(filepath, Encoding.UTF8);
                text = Regex.Replace(text, @"[\p{P}\s]", "");
                text = text.ToLower();
            }
            return text;
        }


        public static string ConvertToBase64(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(bytes);
        }
        public static double GetShannonEntropy(string text, Dictionary<char, int> numberSymbols)
        {
            double entropy = 0d;

            foreach (var symbol in numberSymbols)
            {
                double P = (double)symbol.Value / text.Length;
                entropy -= P * Math.Log2(P);
            }

            return Math.Round(entropy, 3);
        }

        public static double GetHartlyEntropy(int AlphabetSize)
        {
            return Math.Round(Math.Log2(AlphabetSize), 3);
        }

        public static double GetRedundancy(string text, Dictionary<char, int> numberSymbols, int AlphabetSize)
        {
            return Math.Round(((GetHartlyEntropy(AlphabetSize) - GetShannonEntropy(text, numberSymbols)) / GetHartlyEntropy(AlphabetSize)) * 100, 3);
        }

        public static string XOR(string a, string b)
        {
            byte[] bytesA = Encoding.UTF8.GetBytes(a);
            byte[] bytesB = Encoding.UTF8.GetBytes(b);
            byte[] result;

            if (bytesA.Length > bytesB.Length)
            {
                Array.Resize(ref bytesB, bytesA.Length);
                result = new byte[bytesA.Length];
            }
            else
            {
                Array.Resize(ref bytesA, bytesB.Length);
                result = new byte[bytesB.Length];
            }


            for (int i = 0; i < result.Length; i++)
            {
                result[i] = (byte)(bytesA[i] ^ bytesB[i]);
            }

            return Encoding.UTF8.GetString(result);
        }

        public static string ToAsciiBinary(string text)
        {
            string asciiBinary = "";
            foreach (var c in text)
            {
                string bits = Convert.ToString((int)c, 2);

                while (bits.Length < 8)
                {
                    bits = bits.Insert(0, "0");
                }

                asciiBinary += bits;
            }

            return asciiBinary;
        }

        public static Dictionary<char, int> CountNumberSymbols(string text)
        {
            Dictionary<char, int> numberSymbols = new Dictionary<char, int>();
            foreach (char c in text) numberSymbols[c] = numberSymbols.ContainsKey(c) ? numberSymbols[c] + 1 : 1;
            return numberSymbols;
        }
    }
}
