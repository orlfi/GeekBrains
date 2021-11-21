namespace Task_1.Codecs.Extensions
{
    public static class RangeEx
    {
        public static bool Contains(this Range range, int index) =>
            range.Start.Value <= index && range.End.Value >= index;

    }
}