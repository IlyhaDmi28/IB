using Lab5;
using System.Diagnostics;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

string polishText;
string encryptText;
string decrypText;

DateTime start = DateTime.Now, end = DateTime.Now;
polishText = Permutations.ReadTextFromFile("PolishText.txt");

start = DateTime.Now;
encryptText = Permutations.EncryptRoutePermutation(polishText);
end = DateTime.Now;
Permutations.WriteTextToFile("EncryptPolishText.txt", encryptText);
Console.WriteLine(encryptText);
Console.WriteLine("Время шифрования маршрутной перестановкой: " + (end - start).TotalMilliseconds + " мс");
Console.WriteLine();

start = DateTime.Now;
decrypText = Permutations.DecryptRoutePermutation(polishText);
end = DateTime.Now;
Permutations.WriteTextToFile("DecryptPolishText.txt", decrypText);
Console.WriteLine(decrypText);
Console.WriteLine("Время расшифрования маршрутной перестановки: " + (end - start).TotalMilliseconds + " мс");
Console.WriteLine();

start = DateTime.Now;
encryptText = Permutations.EncryptMultiplePermutation(polishText, "dmitruk", "ilya");
end = DateTime.Now;
Permutations.WriteTextToFile("EncryptPolishText2.txt", encryptText);
Console.WriteLine(encryptText);
Console.WriteLine("Время шифрования множественной перестановкой: " + (end - start).TotalMilliseconds + " мс");
Console.WriteLine();

start = DateTime.Now;
decrypText = Permutations.DecryptMultiplePermutation(encryptText, "dmitruk", "ilya");
end = DateTime.Now;
Permutations.WriteTextToFile("DecryptPolishText2.txt", decrypText);
Console.WriteLine(decrypText);
Console.WriteLine("Время расшифрования множественной перестановкой: " + (end - start).TotalMilliseconds + " мс");
Console.WriteLine();
