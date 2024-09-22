using Lab2;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

string text;
string textBase64;
string a = "Dmitruk";
string b = "Ilya";
Dictionary<char, int> NumberSymbols;


text = Coding.ReadTextFromFile("TurkeyText.txt");
NumberSymbols = Coding.CountNumberSymbols(text);
Console.WriteLine("Энтропия Шенона турецкого алфавита: " + Coding.GetShannonEntropy(text, NumberSymbols));
Console.WriteLine("Энтропия Хартли турецкого алфавита: " + Coding.GetHartlyEntropy(29));
Console.WriteLine("Избыточность символов турецкого алфавита: " + Coding.GetRedundancy(text, NumberSymbols, 29));
Console.WriteLine();

textBase64 = Coding.ConvertToBase64(text);
NumberSymbols = Coding.CountNumberSymbols(textBase64);
Console.WriteLine("Энтропия Шенона base64: " + Coding.GetShannonEntropy(textBase64, NumberSymbols));
Console.WriteLine("Энтропия Хартли base64: " + Coding.GetHartlyEntropy(64));
Console.WriteLine("Избыточность символов base64: " + Coding.GetRedundancy(textBase64, NumberSymbols, 64));
Console.WriteLine();

Console.WriteLine("aXORb в ASCII: " + Coding.ToAsciiBinary(Coding.XOR(a, b)));
Console.WriteLine("aXORbXORb в ASCII: " + Coding.ToAsciiBinary(Coding.XOR(Coding.XOR(a, b), b)));
Console.WriteLine();

Console.WriteLine("aXORb в base64: " + Coding.ConvertToBase64(Coding.XOR(Coding.ConvertToBase64(a), Coding.ConvertToBase64(b))));
Console.WriteLine("aXORbXORb в base64: " + Coding.XOR(Coding.XOR(Coding.ConvertToBase64(a), Coding.ConvertToBase64(b)), Coding.ConvertToBase64(b)));

