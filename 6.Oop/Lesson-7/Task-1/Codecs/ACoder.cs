using Task_1.Codecs.Interfaces;

namespace Task_1.Codecs
{
    public class ACoder : ICoder
    {
        public string Code(string source)
        {
            var result = new Char[source.Length];
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] >= 'a' && source[i] <= 'я')
                {
                    result[i] = (char)(source[i] + 1);
                }
                else
                {
                    result[i] = source[i];
                }
            }

            return new string(result);
        }

        public string Decode(string source)
        {
            var result = new Char[source.Length];
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] >= 'a' && source[i] <= 'я')
                {
                    result[i] = (char)(source[i] - 1);
                }
                else
                {
                    result[i] = source[i];
                }
            }

            return new string(result);
        }
    }
}