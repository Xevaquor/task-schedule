namespace TaskSchedule.Algo
{
    public class Util
    {
        public static string IntToString(int value, char[] baseChars, int digits)
        {
            string result = string.Empty;
            int targetBase = baseChars.Length;

            do
            {
                result = baseChars[value % targetBase] + result;
                value = value / targetBase;
            }
            while (value > 0);


            return result.PadLeft(digits, baseChars[0]);
        }
    }
}