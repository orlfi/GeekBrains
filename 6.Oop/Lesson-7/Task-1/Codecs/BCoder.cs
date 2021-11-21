using Task_1.Codecs.Interfaces;
using Task_1.Codecs.Extensions;

namespace Task_1.Codecs
{
    public class BCoder : ICoder
    {
        private readonly Range[] _ranges = new Range[]
        {
            'a'..'z',
            'A'..'Z',
            'А'..'Я',
            'а'..'я'
        };

        public string Code(string source)
        {

            var result = new Char[source.Length];
            for (int i = 0; i < source.Length; i++)
            {
                result[i] = (char)GetMirrorCode(source[i]);
            }

            return new string(result);
        }

        public string Decode(string source)
        {

            var result = new Char[source.Length];
            for (int i = 0; i < source.Length; i++)
            {
                result[i] = (char)GetMirrorCode(source[i]);
            }

            return new string(result);
        }

        private int GetMirrorCode(int code)
        {
            foreach (var range in _ranges)
            {
                if (range.Contains(code))
                    return range.End.Value - code + range.Start.Value;
            }

            return code;
        }

    }
}