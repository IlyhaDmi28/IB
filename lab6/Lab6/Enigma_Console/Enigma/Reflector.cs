namespace Enigma
{
    public class Reflector
    {
        public struct SymbolPair
        {
            public char a;
            public char b;

            public SymbolPair(char a, char b)
            {
                this.a = a; this.b = b;
            }
        }

        public List<SymbolPair> SymbolPairs { get; private set; }

        public Reflector(List<SymbolPair> pairs)
        {
            SymbolPairs = pairs;
        }

        public char Reflect(char symbol)
        {
            SymbolPair ?pair = null;

            foreach (var item in SymbolPairs)
            {
                if (item.a == symbol || item.b == symbol)
                {
                    pair = item; break;
                }
            }

            if (pair == null)
                return '\0';

            if (pair.Value.a == symbol)
                return pair.Value.b;
            else
                return pair.Value.a;
        }
    }
}
