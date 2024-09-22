using Lab10;
using System;
using System.Numerics;


SolveEquation(11, 1000, 9999, 209);

string text = "Dmintruk Ilya Igorevich";
string encryptedText;
DateTime start = DateTime.Now, end = DateTime.Now;
Console.WriteLine("Исходное сообщение: " + text);


// RSA Encryption/Decryption
var rsaKeys = RSA.GenerateRSAKeys();

start = DateTime.Now;
var encryptedRSA = RSA.RSAEncrypt(text, rsaKeys.publicKey);
end = DateTime.Now;
encryptedText = Convert.ToBase64String(encryptedRSA);
Console.WriteLine("Зашифрованное сообщение RSA: " + encryptedText);
Console.WriteLine($"Время шифрования RSA: {(end - start).TotalMilliseconds}");
Console.WriteLine($"Размер исходного текста: {text.Length}, размер зашифрованного RSA текста: {encryptedText.Length} (больше в {encryptedText.Length / text.Length} раз исходного)");

start = DateTime.Now;
var decryptedRSA = RSA.RSADecrypt(encryptedRSA, rsaKeys.privateKey);
end = DateTime.Now;
Console.WriteLine("Расшифрованное сообщение RSA: " + decryptedRSA);
Console.WriteLine($"Время расшифрования RSA: {(end - start).TotalMilliseconds}");



// ElGamal Encryption/Decryption
var elGamalKeys = ElGamal.GenerateElGamalKeys();

start = DateTime.Now;
var encryptedElGamal = ElGamal.ElGamalEncrypt(text, elGamalKeys.publicKey);
end = DateTime.Now;

encryptedText = Convert.ToBase64String(encryptedElGamal);
Console.WriteLine("Зашифрованное сообщение Эль-Гамаля: " + encryptedText);
Console.WriteLine($"Время шифрования Эль-Гамаля: {(end - start).TotalMilliseconds}");

start = DateTime.Now;
var decryptedElGamal = ElGamal.ElGamalDecrypt(encryptedElGamal, elGamalKeys.privateKey);
end = DateTime.Now;
Console.WriteLine("Расшифрованное сообщение Эль-Гамаля: " + decryptedElGamal);
Console.WriteLine($"Время расшифрования Эль-Гамаля: {(end - start).TotalMilliseconds}");
Console.WriteLine($"Размер исходного текста: {text.Length}, размер зашифрованного Эль-Гамаля текста: {encryptedText.Length} (больше в {encryptedText.Length / text.Length} раз исходного)");



void SolveEquation(BigInteger a, BigInteger xStart, BigInteger xEnd, BigInteger n)
{
    var step = (xEnd - xStart) / 6;

    for (BigInteger x = xStart; x < xEnd; x += step)
	{
        var pow = a;
        for (BigInteger i = 1; i < x; i++)
        {
            pow *= a;
        }

        start = DateTime.Now;
        Console.WriteLine($"{a}^{x} mod {n} = {pow % n}");
        end = DateTime.Now;

        Console.WriteLine($"Время вычисления y: {(end - start).TotalMilliseconds}");
    }
}