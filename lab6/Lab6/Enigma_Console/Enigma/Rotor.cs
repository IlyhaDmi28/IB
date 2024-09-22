using Enigma.ExtendedMath;

namespace Enigma
{
    public class Rotor
    {
        public Alphabet LanguageOriginalAlphabet { get; private set; }
        public Alphabet RotorOriginalAlphabet { get; private set; }
        public Alphabet CurrnetAlphabet { get; private set; }

        public int Offset { get; private set; }
        public int CurrnetOffset { get; private set; }

        public bool IsScrolled { get; private set; }

        public Rotor(Alphabet languageAlphabet, Alphabet rotorAlphabet, int offset)
        {
            this.RotorOriginalAlphabet = rotorAlphabet.Clone();
            this.LanguageOriginalAlphabet = languageAlphabet;
            this.CurrnetAlphabet = rotorAlphabet.Clone();

            this.Offset = offset;
            this.CurrnetOffset = 0;
            this.IsScrolled = false;
        }

        public void Move()
        {
            int newOffset = Math.Clamp(CurrnetOffset + Offset, 0, RotorOriginalAlphabet.Count - 1);

            IsScrolled = newOffset < CurrnetOffset;

            CurrnetOffset = newOffset;

            CurrnetAlphabet = MoveAlphabet();
        }
        public void Reset()
        {
            CurrnetOffset = 0;

            CurrnetAlphabet = RotorOriginalAlphabet.Clone();
        }

        public void SetOffset(int offset)
        {
            int newOffset = Math.Clamp(offset, 0, RotorOriginalAlphabet.Count - 1);

            IsScrolled = newOffset < CurrnetOffset;

            CurrnetOffset = newOffset;

            CurrnetAlphabet = MoveAlphabet();
        }

        public char GetSymbol(char symbol)
        {
            return CurrnetAlphabet[LanguageOriginalAlphabet.GetSymbolIndex(symbol)];
        }
        public char GetSymbolReflected(char symbol)
        {
            return LanguageOriginalAlphabet[CurrnetAlphabet.GetSymbolIndex(symbol)];
        }

        private Alphabet MoveAlphabet()
        {
            char[] rawmoved = RotorOriginalAlphabet.Raw;
            char[] actualmoved = RotorOriginalAlphabet.Raw;

            for (int i = 0; i < CurrnetOffset; i++)
            {
                for (int j = 0; j < RotorOriginalAlphabet.Count; j++)
                {
                    if (j == RotorOriginalAlphabet.Count - 1)
                        actualmoved[j] = rawmoved[0];
                    else
                        actualmoved[j] = rawmoved[j + 1];
                }
                rawmoved = (char[])actualmoved.Clone();
            }

            return new Alphabet(actualmoved);
        }
    }
}
