namespace Enigma.ExtendedMath
{
    public static class EnigmaMath
    {
        public static int Clamp(int value, int min, int max)
        {
            if (value < min)
            {
                int res = max + value;

                if (res < min)
                    return Clamp(res, min, max);
                else
                    return res;
            }
            else if (value >= max)
                return value % max;
            else
                return value;
        }
    }
}
