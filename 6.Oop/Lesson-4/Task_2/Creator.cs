using System.Collections.Generic;

namespace Task_2
{
    public static class Creator
    {
        private readonly static Dictionary<int, Building> _buildings = new Dictionary<int, Building>();

        public static int CreateBuilding(double height) => CreateBuilding(height, 0);

        public static int CreateBuilding(double height, int flatCount) => CreateBuilding(height, flatCount, 0);

        public static int CreateBuilding(double height, int flatCount, int floorCount) => CreateBuilding(height, flatCount, floorCount, 0);

        public static int CreateBuilding(double height, int flatCount, int floorCount, int entranceCount)
        {
            var result = new Building()
            {
                Height = height,
                FlatCount = flatCount,
                FloorCount = floorCount,
                EntranceCount = entranceCount
            };
            _buildings.Add(result.Number, result);
            return result.Number;
        }

        public static Building GetBuildingByNumber(int number)
        {
            return _buildings[number];
        }

        public static void DeleteBuilding(int number)
        {
            if (_buildings.ContainsKey(number))
                _buildings.Remove(number);
        }
    }
}