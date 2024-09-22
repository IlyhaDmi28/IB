using Lab4;
using System.Diagnostics;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

string polishText;
string encryptText;

Stopwatch sw = new Stopwatch();

polishText = Encrypter.ReadTextFromFile("PolishText.txt");
sw.Start();
encryptText = Encrypter.EncryptCaesar(polishText, 5, 7);
sw.Stop();
Encrypter.WriteTextToFile("EncryptPolishText.txt", encryptText);
Console.WriteLine(encryptText);
Console.WriteLine("Время шифрования аффинной системой подстановки Цезаря: " + sw.ElapsedMilliseconds + " мс");
Console.WriteLine();
sw.Reset();

sw.Start();
string decrypText = Encrypter.DecryptCaesar(encryptText, 5, 7);
sw.Stop();
Console.WriteLine(decrypText);
Console.WriteLine("Время расшифрования текста, зашифрованного аффинной системой подстановки Цезаря: " + sw.ElapsedMilliseconds + " мс");
Console.WriteLine();
sw.Reset();

sw.Start();
encryptText = Encrypter.EncryptPorta(polishText);
sw.Stop();
Encrypter.WriteTextToFile("EncryptPortaPolishText.txt", encryptText);
Console.WriteLine(encryptText);
Console.WriteLine("Время шифрования шифром Порты: " + sw.ElapsedMilliseconds + " мс");
Console.WriteLine();
sw.Reset();

sw.Start();
decrypText = Encrypter.DecryptPorta(encryptText);
sw.Stop();
Encrypter.WriteTextToFile("DecryptPortaPolishText.txt", decrypText);
Console.WriteLine(decrypText);
Console.WriteLine("Время расшифрования текста, зашифрованного шифром Порты: " + sw.ElapsedMilliseconds + " мс");