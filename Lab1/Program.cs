using Lab1;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;



Entropy.ReadTextFromFile("TurkeyText.txt");
Console.WriteLine("Энтропия турецкого алфавита: " + Entropy.GetEntropy());

Entropy.ReadTextFromFile("MacedonianText.txt");
Console.WriteLine("Энтропия македонского алфавита: " + Entropy.GetEntropy());

Entropy.ReadTextFromFile("BinaryMacedonianText.txt");
Console.WriteLine("Энтропия бинарного алфавита: " + Entropy.GetEntropy());



Entropy.ReadTextFromFile("TurkeyText.txt");
Entropy.ReadNameFromFile("MyTurkishName.txt");
Console.WriteLine("Количеством информации ФИО на турецком: " + Entropy.GetInformationAmount());

Entropy.ReadTextFromFile("MacedonianText.txt");
Entropy.ReadNameFromFile("MyMacedonianName.txt");
Console.WriteLine("Количеством информации ФИО на македонском: " + Entropy.GetInformationAmount());

Entropy.ReadTextFromFile("BinaryMacedonianText.txt");
Entropy.ReadNameFromFile("MyBinaryMacedonianName.txt");
Console.WriteLine("Количеством информации ФИО на бинарном: " + Entropy.GetInformationAmount());




double p = 0.1;
Console.WriteLine("\n------------------------- P = 0.1 -------------------------");
Entropy.ReadTextFromFile("TurkeyText.txt");
Entropy.ReadNameFromFile("MyTurkishName.txt");
Console.WriteLine("Количеством информации ФИО на турецком: " + Entropy.GetInformationAmount(p));

Entropy.ReadTextFromFile("MacedonianText.txt");
Entropy.ReadNameFromFile("MyMacedonianName.txt");
Console.WriteLine("Количеством информации ФИО на македонском: " + Entropy.GetInformationAmount(p));

Entropy.ReadTextFromFile("BinaryMacedonianText.txt");
Entropy.ReadNameFromFile("MyBinaryMacedonianName.txt");
Console.WriteLine("Количеством информации ФИО на бинарном: " + Entropy.GetInformationAmount(p));




p = 0.5;
Console.WriteLine("\n------------------------- P = 0.5 -------------------------");
Entropy.ReadTextFromFile("TurkeyText.txt");
Entropy.ReadNameFromFile("MyTurkishName.txt");
Console.WriteLine("Количеством информации ФИО на турецком: " + Entropy.GetInformationAmount(p));

Entropy.ReadTextFromFile("MacedonianText.txt");
Entropy.ReadNameFromFile("MyMacedonianName.txt");
Console.WriteLine("Количеством информации ФИО на македонском: " + Entropy.GetInformationAmount(p));

Entropy.ReadTextFromFile("BinaryMacedonianText.txt");
Entropy.ReadNameFromFile("MyBinaryMacedonianName.txt");
Console.WriteLine("Количеством информации ФИО на бинарном: " + Entropy.GetInformationAmount(p));




p = 1.0;
Console.WriteLine("\n------------------------- P = 1.0 -------------------------");
Entropy.ReadTextFromFile("TurkeyText.txt");
Entropy.ReadNameFromFile("MyTurkishName.txt");
Console.WriteLine("Количеством информации ФИО на турецком: " + Entropy.GetInformationAmount(p));

Entropy.ReadTextFromFile("MacedonianText.txt");
Entropy.ReadNameFromFile("MyMacedonianName.txt");
Console.WriteLine("Количеством информации ФИО на македонском: " + Entropy.GetInformationAmount(p));


Entropy.ReadTextFromFile("BinaryMacedonianText.txt");
Entropy.ReadNameFromFile("MyBinaryMacedonianName.txt");
Console.WriteLine("Количеством информации ФИО на бинарном: " + Entropy.GetInformationAmount(p));


