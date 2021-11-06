namespace Data
{
    public class BuildingBuilder
    {
        private readonly Building _building;
        public BuildingBuilder() => _building = new Building();

        public BuildingBuilder WithFloorCount(int count)
        {
            _building.FloorCount = count;
            return this;
        }

        public BuildingBuilder WithHeight(double height)
        {
            _building.Height = height;
            return this;
        }
        public BuildingBuilder WithFlatCount(int count)
        {
            _building.FlatCount = count;
            return this;
        }

        public BuildingBuilder WithEntranceCount(int count)
        {
            _building.EntranceCount = count;
            return this;
        }

        public Building Build() => _building;
    }
}