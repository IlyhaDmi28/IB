using Enigma;
using Enigma.ExtendedMath;

internal class Program
{
    private static void Main(string[] args)
    {
        Alphabet Eng = new Alphabet('a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z');
        Alphabet RotorI = new Alphabet('e', 'k', 'm', 'f', 'l', 'g', 'd', 'q', 'v', 'z', 'n', 't', 'o', 'w', 'y', 'h', 'x', 'u', 's', 'p', 'a', 'i', 'b', 'r', 'c', 'j');
        Alphabet RotorBeta = new Alphabet('l', 'e', 'y', 'j', 'v', 'c', 'n', 'i', 'x', 'w', 'p', 'b', 'q', 'm', 'd', 'r', 't', 'a', 'k', 'z', 'g', 'f', 'u', 'h', 'o', 's');
        Alphabet RotorGamma = new Alphabet('f', 's', 'o', 'k', 'a', 'n', 'u', 'e', 'r', 'h', 'm', 'b', 't', 'i', 'y', 'c', 'w', 'l', 'q', 'p', 'z', 'x', 'v', 'g', 'j', 'd');

        Rotor left = new Rotor(Eng, RotorI, 3);
        Rotor middle = new Rotor(Eng, RotorBeta, 1);
        Rotor right = new Rotor(Eng, RotorGamma, 3);

        Reflector reflectorB = new Reflector(new List<Reflector.SymbolPair>()
        {
            new ('a', 'y'), new ('b', 'r'), new ('c', 'u'), new ('d', 'h'), new ('e', 'q'), new ('f', 's'),
            new ('g', 'l'), new ('i', 'p'), new ('j', 'x'), new ('k', 'n'), new ('m', 'o'), new ('t', 'z'), new ('v', 'w')
        });

        EnigmaEngine enigma = new EnigmaEngine(left, middle, right, reflectorB);

        string text = "z";
        string encrypted = enigma.Encrypt(text);

        enigma.Reset();

        string decrypted = enigma.Encrypt(encrypted);

        Console.WriteLine($"Зашифрованное сообщение: {encrypted}");
        Console.WriteLine($"Расшифрованное сообщение: {decrypted}");
       
    }
}