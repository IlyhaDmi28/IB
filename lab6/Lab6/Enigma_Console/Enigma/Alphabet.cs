namespace Enigma {
    public class Alphabet
    {
        private List<char> alphabet;
        public char[] Raw => alphabet.ToArray();

        public int Count => alphabet.Count;

        public bool IsBinnary => Count == 2;

        public char this[int index]
        {
            get { return alphabet[index]; }
        }

        public Alphabet(string alphabet)
        {
            this.alphabet = alphabet.ToCharArray().ToList();
        }
        public Alphabet(params char[] alphabet)
        {
            this.alphabet = alphabet.ToList();
        }

        public int[] GetCharactersAmount(string text)
        {
            int[] counts = new int[alphabet.Count];

            foreach (char c in text)
            {
                if (alphabet.Contains(c))
                    counts[alphabet.IndexOf(c)]++;
            }

            return counts;
        }

        public int GetCharactersTotal(string text)
        {
            return GetCharactersAmount(text).Sum();
        }

        public double[] GetCharactersProbability(string text)
        {
            double[] probs = new double[alphabet.Count];

            int[] counts = GetCharactersAmount(text);
            int total = counts.Sum();

            for (int i = 0; i < alphabet.Count; i++)
            {
                probs[i] = (double)counts[i] / (double)total;
            }

            return probs;
        }

        public double GetShannonEntropy(string text)
        {
            double[] probs = GetCharactersProbability(text);

            double entropy = 0;

            foreach (double item in probs)
            {
                if (item <= 0)
                    continue;

                entropy += item * Math.Log(item, 2);
            }
            entropy *= -1;

            return entropy;
        }

        public double GetEffectiveEntorpy(double mistake)
        {
            double good = 1 - mistake;

            if (IsBinnary && (mistake == 0 || good == 0))
                return 1;

            if (!IsBinnary && mistake == 1)
                return 0;

            return 1 - (-mistake * Math.Log(mistake, 2) - good * Math.Log(good, 2));
        }

        public double GetHartlyEntropy()
        {
            return Math.Log(Count, 2);
        }

        public double GetRedundancy(string text)
        {
            return (GetHartlyEntropy() - GetShannonEntropy(text)) / GetHartlyEntropy();
        }

        public double GetInformationAmount(string text)
        {
            return GetShannonEntropy(text) * text.Length;
        }
        public double GetInformationAmount(string text, double mistake)
        {
            return GetShannonEntropy(text) * text.Length * GetEffectiveEntorpy(mistake);
        }

        public string GetStringWithOnlyAlphabetSymbols(string text)
        {
            string str = "";

            foreach (char c in text)
            {
                if (alphabet.Contains(c))
                    str += c;
            }

            return str;
        }

        public int GetSymbolIndex(char symbol) => alphabet.IndexOf(symbol);

        public Alphabet Clone() => new Alphabet(Raw);

        public static Alphabet GetAlphabetByText(string text, bool ingoreCase = false, bool ignoreSpace = true, bool ingorePunct = true, bool ignoreNumbers = true, bool ignoreSpecial = true)
        {
            List<char> chars = new List<char>();

            foreach (char c in text)
            {
                char symbol = ingoreCase ? c.ToString().ToLower()[0] : c;

                if (ignoreSpace && symbol == ' ')
                    continue;

                if (ignoreSpecial && (symbol == '\n' || symbol == '\t' || symbol == '\0' || symbol == '\r' || symbol == '\a' || symbol == '\f' || symbol == '\v' || symbol == '\b'))
                    continue;

                if (ingorePunct && (symbol == '(' || symbol == ')' || symbol == '{' || symbol == '}' || symbol == '[' || symbol == ']'
                                || symbol == ',' || symbol == '.' || symbol == '|' || symbol == '"' || symbol == '\'' || symbol == ':'
                                || symbol == ';' || symbol == '\\' || symbol == '/' || symbol == '`' || symbol == '!' || symbol == '&'
                                || symbol == '?' || symbol == '^' || symbol == '$' || symbol == '#' || symbol == '*' || symbol == '-'
                                || symbol == '+' || symbol == '=' || symbol == '№' || symbol == '@'))
                    continue;

                if (ignoreNumbers && (symbol == '0' || symbol == '1' || symbol == '2' || symbol == '3' || symbol == '4' || symbol == '5' || symbol == '6'
                                  || symbol == '7' || symbol == '8' || symbol == '9'))
                    continue;


                if (!chars.Contains(symbol))
                {
                    chars.Add(symbol);
                }
            }

            return new Alphabet(chars.ToArray());
        }
    }
}