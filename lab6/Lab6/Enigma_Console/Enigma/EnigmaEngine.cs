
namespace Enigma
{
    public class EnigmaEngine
    {
        public Rotor Left { get; private set; }
        public Rotor Middle { get; private set; }
        public Rotor Right { get; private set; }

        public Reflector Reflector { get; private set; }

        public EnigmaEngine(Rotor left, Rotor middle, Rotor right, Reflector reflector)
        {
            Left = left;
            Middle = middle;
            Right = right;
            Reflector = reflector;
        }

        public string Encrypt(string text)
        {
            string result = "";

            foreach (char item in Left.LanguageOriginalAlphabet.GetStringWithOnlyAlphabetSymbols(text.ToLower()))
            {
                char symbol = item;

                symbol = Right.GetSymbol(symbol);
                symbol = Middle.GetSymbol(symbol);
                symbol = Left.GetSymbol(symbol);

                symbol = Reflector.Reflect(symbol);

                symbol = Left.GetSymbolReflected(symbol);
                symbol = Middle.GetSymbolReflected(symbol);
                symbol = Right.GetSymbolReflected(symbol);

                result += symbol;

                Right.Move();

                if (Middle.Offset == 0)
                {
                    if (Right.IsScrolled)
                        Middle.SetOffset(Middle.CurrnetOffset + 1);
                }
                else
                    Middle.Move();

                if (Left.Offset == 0)
                {
                    if (Middle.IsScrolled)
                        Left.SetOffset(Left.CurrnetOffset + 1);
                }
                else
                    Left.Move();
            }

            return result;
        }

        public void Reset()
        {
            Left.Reset();
            Middle.Reset();
            Right.Reset();
        }
    }
}
