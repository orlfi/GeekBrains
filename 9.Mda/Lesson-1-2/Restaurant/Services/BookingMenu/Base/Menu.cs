namespace Services.BookingMenu.Base;

public abstract class Menu
{

    protected int GetNumberAnswer(string wrongConditionMessage)
    {
        return GetNumberAnswer(_ => true, wrongConditionMessage);
    }

    protected int GetNumberAnswer(Func<int, bool> condition, string wrongConditionMessage)
    {
        var answer = Console.ReadLine();
        if (!(int.TryParse(answer, out int value) && condition(value)))
        {
            Console.WriteLine(wrongConditionMessage);
            return GetNumberAnswer(condition, wrongConditionMessage);
        }
        return value;
    }
}
