namespace Task_1.Codecs.Interfaces
{
    public interface ICoder
    {
        string Code(string source);
        string Decode(string source);
    }
}