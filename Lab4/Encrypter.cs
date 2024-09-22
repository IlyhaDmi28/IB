using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab4
{
    public static class Encrypter
    {
        private static int ModInverse(int a, int m)
        {
            a = a % m;

            for (int x = 1; x < m; x++)
            {
                if ((a * x) % m == 1) return x;
            }

            return 1;
        }

        private static char[] alphabet = {
            'A', 'Ą', 'B', 'C', 'Ć', 'D', 'E', 'Ę', 'F', 'G', 'H', 'I', 'J', 'K',
            'L', 'Ł', 'M', 'N', 'Ń', 'O', 'Ó', 'P', 'R', 'S', 'Ś', 'T', 'U',
            'W', 'Y', 'Z', 'Ź', 'Ż'
        };
        public static string ReadTextFromFile(string filename)
        {
            string text = "";

            string pathToFolder = "../../../Texts/";
            string filepath = Path.Combine(pathToFolder, filename);

            if (File.Exists(filepath)) text = File.ReadAllText(filepath, Encoding.UTF8);

            return text;
        }

        public static void WriteTextToFile(string filename, string text)
        {
            string pathToFolder = "../../../Texts/";
            string filepath = Path.Combine(pathToFolder, filename);

            File.WriteAllText(filepath, text);
        }

        public static string EncryptCaesar(string text, int a, int b)
        {
            StringBuilder encryptText = new StringBuilder();
            int n = alphabet.Length;
            int x;
            
            foreach (char c in text)
            {
                x = Array.IndexOf(alphabet, char.ToUpper(c));
                if (x != -1)
                {
                    if (char.IsUpper(c)) encryptText.Append(alphabet[(a * x + b) % n]);
                    else encryptText.Append(char.ToLower(alphabet[((a * x) + b) % n]));
                }
                else encryptText.Append(c);
            }

            return encryptText.ToString();
        }

        public static string DecryptCaesar(string text, int a, int b)
        {
            StringBuilder decryptText = new StringBuilder();
            int n = alphabet.Length;
            int y;
            int a_inv = ModInverse(a, n);

            foreach (char c in text)
            {
                y = Array.IndexOf(alphabet, char.ToUpper(c));
                if (y != -1)
                {
                    if (char.IsUpper(c)) decryptText.Append(alphabet[(a_inv * (y - b + n)) % n]);
                    else decryptText.Append(char.ToLower(alphabet[(a_inv * (y - b + n)) % n]));

                }
                else decryptText.Append(c);
            }

            return decryptText.ToString();
        }

        public static string EncryptPorta(string text)
        {
            List<int> encryptText = new List<int>();
            int[,]tablePorta = new int[alphabet.Length, alphabet.Length];

            text = Regex.Replace(text, @"[\p{P}\s]", "");
            text = text.ToUpper();

            int n = 1;
            for (int i = 0; i < alphabet.Length; i++)
            {
                for (int j = 0; j < alphabet.Length; j++)
                {
                    tablePorta[i, j] = n++;
                }
            }

            if (text.Length % 2 != 0) text += 'a';

            int x = 0, y = 0;
            for (int i = 0; i < text.Length; i += 2)
            {
                x = Array.IndexOf(alphabet, text[i]);
                y = Array.IndexOf(alphabet, text[i + 1]);
                encryptText.Add(tablePorta[x, y]);
            }

            return string.Join(" ", encryptText);
        }

        public static string DecryptPorta(string text)
        {
            StringBuilder decryptText = new StringBuilder();
            int[,] tablePorta = new int[alphabet.Length, alphabet.Length];

            int n = 1;
            for (int i = 0; i < alphabet.Length; i++)
            {
                for (int j = 0; j < alphabet.Length; j++)
                {
                    tablePorta[i, j] = n++;
                }
            }

            foreach (string number in text.Split(' '))
            {
                n = Convert.ToInt32(number);

                for (int i = 0; i < alphabet.Length; i++)
                {
                    for (int j = 0; j < alphabet.Length; j++)
                    {
                        if (tablePorta[i, j] == n) decryptText.Append($"{alphabet[i]}{alphabet[j]}");
                    }
                }
            }

            return decryptText.ToString();
        }
    }
}
