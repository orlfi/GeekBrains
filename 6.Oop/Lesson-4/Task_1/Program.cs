using System;

namespace Task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Building building = new();
            building.SetHeight(30);
            building.SetFloorCount(9);
            building.SetFlatCount(72);
            building.SetEntranceCount(2);
            PrintBuildingInfo(building);

            Building building2 = new();
            building2.SetHeight(50);
            building2.SetFloorCount(16);
            building2.SetFlatCount(320);
            building2.SetEntranceCount(4);
            Console.WriteLine();
            PrintBuildingInfo(building2);
        }

        public static void PrintBuildingInfo(Building building)
        {
            Console.WriteLine($"Параметры здания №{building.GetNumber()}:");
            Console.WriteLine($"Высота этажа: {building.GetFloorHeight():0.##}");
            Console.WriteLine($"Количество квартир в подъезде: {building.GetFlatCountInEntrance()}");
            Console.WriteLine($"количества квартир на этаже: {building.GetFlatCountOnFloor()}");

        }
    }
}
