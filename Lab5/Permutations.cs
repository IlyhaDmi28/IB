using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Lab5
{
    public class Permutations   
    {
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

        public static string EncryptRoutePermutation(string text)
        {
            char[,] table = new char[10, 50];

            for (int i = 0, n = 0, t = 0; i < text.Length; i++, n++)
            {
                table[n, t] = text[i];

                if (n == 9)
                {
                    n = -1;
                    t++;
                }
            }

            StringBuilder encryptText = new StringBuilder();
            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    encryptText.Append(table[i, j]);
                }
            }

            return encryptText.ToString(); 
        }


        public static string DecryptRoutePermutation(string text)
        {
            char[,] table = new char[10, 50];

            for (int i = 0, n = 0, t = 0; i < text.Length; i++, t++)
            {
                table[n, t] = text[i];

                if (t == 49)
                {
                    n++;
                    t = -1;
                }
            }

            StringBuilder decrypttText = new StringBuilder();
            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    decrypttText.Append(table[i, j]);
                }
            }

            return decrypttText.ToString();
        }

        public static string EncryptMultiplePermutation(string text, string columnKey, string rowKey)
        {
            int columnLength = columnKey.Length;
            int rowLength = rowKey.Length;
            int index = 0;
            int size = (int)Math.Ceiling((double)text.Length / (rowLength * columnLength));
            char[,] matrix = new char[rowLength, columnLength];
            StringBuilder encryptedText = new StringBuilder();

            for (int k = 0; k < size; k++)
            {
                for (int i = 0; i < rowLength; i++)
                {
                    for (int j = 0; j < columnLength; j++)
                    {
                        if (index < text.Length)
                        {
                            matrix[i, j] = text[index++];
                        }
                        else
                        {
                            matrix[i, j] = '\0';
                        }
                    }
                }

                char[,] buff1 = new char[rowLength, columnLength];
                int pr1 = 0;
                HashSet<int> processedIndexes1 = new HashSet<int>();
                foreach (char column in columnKey)
                {
                    for (int columnIndex = 0; columnIndex < columnKey.Length; columnIndex++)
                    {
                        if (column == columnKey[columnIndex] && !processedIndexes1.Contains(columnIndex))
                        {
                            processedIndexes1.Add(columnIndex);
                            for (int i = 0; i < rowLength; i++)
                            {
                                buff1[i, pr1] = matrix[i, columnIndex];
                            }
                            pr1++;
                        }
                    }
                }

                char[,] buff2 = new char[rowLength, columnLength];
                int pr2 = 0;
                HashSet<int> processedIndexes2 = new HashSet<int>();
                foreach (char row in rowKey)
                {
                    for (int rowIndex = 0; rowIndex < rowKey.Length; rowIndex++)
                    {
                        if (row == rowKey[rowIndex] && !processedIndexes2.Contains(rowIndex))
                        {
                            processedIndexes2.Add(rowIndex);
                            for (int i = 0; i < columnLength; i++)
                            {
                                buff2[pr2, i] = buff1[rowIndex, i];
                            }
                            pr2++;
                        }
                    }
                }

                for (int i = 0; i < columnLength; i++)
                {
                    for (int j = 0; j < rowLength; j++)
                    {
                        encryptedText.Append(buff2[j, i]);
                    }
                }
            }
            return encryptedText.ToString();
        }

        public static string DecryptMultiplePermutation(string text, string rowKey, string columnKey)
        {
            int columnLength = columnKey.Length;
            int rowLength = rowKey.Length;
            int index = 0;
            int size = (int)Math.Ceiling((double)text.Length / (rowLength * columnLength));
            char[,] matrix = new char[rowLength, columnLength];
            StringBuilder decodeText = new StringBuilder();

            for (int k = 0; k < size; k++)
            {
                for (int i = 0; i < rowLength; i++)
                {
                    for (int j = 0; j < columnLength; j++)
                    {
                        if (index < text.Length)
                        {
                            matrix[i, j] = text[index++];
                        }
                        else
                        {
                            matrix[i, j] = '\0';
                        }
                    }
                }

                char[,] buff1 = new char[rowLength, columnLength];
                int pr1 = 0;
                HashSet<int> processedIndexes1 = new HashSet<int>();
                foreach (char row in rowKey)
                {
                    for (int rowIndex = 0; rowIndex < rowKey.Length; rowIndex++)
                    {
                        if (row == rowKey[rowIndex] && !processedIndexes1.Contains(rowIndex))
                        {
                            processedIndexes1.Add(rowIndex);
                            for (int i = 0; i < columnLength; i++)
                            {
                                buff1[rowIndex, i] = matrix[pr1, i];
                            }
                            pr1++;
                        }
                    }
                }

                char[,] buff2 = new char[rowLength, columnLength];
                int pr2 = 0;
                HashSet<int> processedIndexes2 = new HashSet<int>();
                foreach (char column in columnKey)
                {
                    for (int columnIndex = 0; columnIndex < columnKey.Length; columnIndex++)
                    {
                        if (column == columnKey[columnIndex] && !processedIndexes2.Contains(columnIndex))
                        {
                            processedIndexes2.Add(columnIndex);
                            for (int i = 0; i < rowLength; i++)
                            {
                                buff2[i, columnIndex] = buff1[i, pr2];
                            }
                            pr2++;
                        }
                    }
                }

                for (int i = 0; i < columnLength; i++)
                {
                    for (int j = 0; j < rowLength; j++)
                    {
                        decodeText.Append(buff2[j, i]);
                    }
                }
            }
            return decodeText.ToString().Replace("\0", "");
        }
    }
}
