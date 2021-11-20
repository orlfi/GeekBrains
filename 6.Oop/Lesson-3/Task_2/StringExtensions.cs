namespace Task_2
{
    public static class StringExtensions
    {
        public static string Reverse(this string source)
        {
            char[] result = new char[source.Length];
            for (int i = source.Length - 1; i > -1; i--)
            {
                result[source.Length - 1 - i] = source[i];
            }
            return new string(result);
        }
    }
}