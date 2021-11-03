using System;

namespace Task_2
{
    class Program
    {
        static void Main(string[] args)
        {
            int buildingNumber1 = Creator.CreateBuilding(30, 72, 9, 2);
            Console.WriteLine(Creator.GetBuildingByNumber(buildingNumber1).ToString());

            int buildingNumber2 = Creator.CreateBuilding(50, 320, 16, 4);
            Console.WriteLine(Creator.GetBuildingByNumber(buildingNumber2).ToString());

            Creator.DeleteBuilding(buildingNumber2);
            Console.ReadKey();
        }
    }
}
