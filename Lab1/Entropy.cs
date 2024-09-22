using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Lab1
{
    public static class Entropy
    {
        private static string text;
        private static string name;
        private static Dictionary<char, int> NumberSymbols;
        public static void ReadTextFromFile(string filename)
        {
            text = "";
            NumberSymbols.Clear();

            string pathToFolder = "../../../Texts/";
            string filepath = Path.Combine(pathToFolder, filename);

            if (File.Exists(filepath))
            {
                text = File.ReadAllText(filepath, Encoding.UTF8);
                text = Regex.Replace(text, @"[\p{P}\s]", "");
                text = text.ToLower();
                CountNumberSymbols();
            }
        }

        public static void ReadNameFromFile(string filename)
        {
            name = "";

            string pathToFolder = "../../../Texts/";
            string filepath = Path.Combine(pathToFolder, filename);

            if (File.Exists(filepath))
            {
                name = File.ReadAllText(filepath, Encoding.UTF8);
                name = Regex.Replace(name, @"[\p{P}\s]", "");
            }
        }
        public static double GetEntropy()
        {
            double entropy = 0d;

            if (NumberSymbols.Count == 2)
            {
                int onesCount = text.Count(c => c == '1');
                double probabilityOfOne = (double)onesCount / text.Length;
                double probabilityOfZero = 1 - probabilityOfOne;

                entropy = -(probabilityOfOne * Math.Log(probabilityOfOne, 2) + probabilityOfZero * Math.Log(probabilityOfZero, 2));
                return Math.Round(entropy, 3);
            }


            foreach (var symbol in NumberSymbols)
            {
                double P = (double)symbol.Value / text.Length;
                entropy -= P * Math.Log2(P);
            }

            return Math.Round(entropy, 3);
        }


        public static double GetEffectiveEntropy(double p)
        {
            double q = 1 - p;
            return 1 - (-p * Math.Log2(p) - q * Math.Log2(q));
        }

        public static double GetInformationAmount()
        {
            double informationAmount = GetEntropy() * name.Length;
            return Math.Round(informationAmount, 3);
        }

        public static double GetInformationAmount(double p)
        {
            if ((p == 1 || p == 0) && NumberSymbols.Count == 2) return GetInformationAmount();
            if((p == 1 || p == 0)) return 0;

            double informationAmount = GetEntropy() * name.Length * GetEffectiveEntropy(p);
            return Math.Round(informationAmount, 3);
        }

        private static void CountNumberSymbols()
        {
            foreach (char c in text) NumberSymbols[c] = NumberSymbols.ContainsKey(c) ? NumberSymbols[c] + 1 : 1;
        }
        static Entropy()
        {
            text = "";
            NumberSymbols = new Dictionary<char, int>();
        }
    }
}

