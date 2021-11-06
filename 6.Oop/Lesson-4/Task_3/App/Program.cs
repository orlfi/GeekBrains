using System;
using Data;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            // var directCreateBuilding = new Building(); not available

            int buildingNumber1 = Creator.CreateBuilding(30, 72, 9, 2);
            Console.WriteLine(Creator.GetBuildingByNumber(buildingNumber1).ToString());

            int buildingNumber2 = Creator.CreateBuilding(50, 320, 16, 4);
            Console.WriteLine(Creator.GetBuildingByNumber(buildingNumber2).ToString());

            Creator.DeleteBuilding(buildingNumber2);

            var fluentBuilding = Building.CreateBuilder().WithHeight(30).WithFlatCount(72).WithFloorCount(9).WithEntranceCount(2).Build();
            Console.WriteLine($"Builded with fluent api:\r\n{fluentBuilding}");
            Console.ReadKey();
        }
    }
}
