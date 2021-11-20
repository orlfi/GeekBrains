// 1. Реализовать класс для описания здания (уникальный номер здания, высота, этажность, количество квартир, подъездов).
//  Поля сделать закрытыми, предусмотреть методы для заполнения полей и получения значений полей для печати.
//  Добавить методы вычисления высоты этажа, количества квартир в подъезде, количества квартир на этаже и т.д. 
// Предусмотреть возможность, чтобы уникальный номер здания генерировался программно. 
// Для этого в классе предусмотреть статическое поле, которое бы хранило последний использованный номер здания, 
// и предусмотреть метод, который увеличивал бы значение этого поля.

using System.Text;

namespace Data
{
    public sealed class Building
    {
        private static int _lastNumber;

        private static int GenerateNumber() => _lastNumber++;

        private readonly int _number;

        private int _floorCount;

        private double _height;

        private int _flatCount;

        private int _entranceCount;

        public int Number { get => _number; }

        public int FloorCount { get => _floorCount; set => _floorCount = value; }

        public double Height { get => _height; set => _height = value; }

        public int FlatCount { get => _flatCount; set => _flatCount = value; }

        public int EntranceCount { get => _entranceCount; set => _entranceCount = value; }

        public double FloorHeight
        {
            get
            {
                if (FloorCount != 0)
                {
                    return Height / FloorCount;
                }
                else
                {
                    return 0;
                }
            }
        }

        public int FlatCountInEntrance
        {
            get
            {
                if (EntranceCount != 0)
                {
                    return FlatCount / EntranceCount;
                }
                else
                {
                    return 0;
                }
            }
        }

        public int FlatCountOnFloor
        {
            get
            {
                if (FloorCount != 0)
                {
                    return FlatCountInEntrance / FloorCount;
                }
                else
                {
                    return 0;
                }
            }
        }

        public static BuildingBuilder CreateBuilder() => new BuildingBuilder();

        internal Building() => _number = GenerateNumber();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Номер дома: {Number}");
            sb.AppendLine($"Высота здания: {Height}");
            sb.AppendLine($"Количество квартир: {FlatCount}");
            sb.AppendLine($"Количество этажей': {FloorCount}");
            sb.AppendLine($"Количество подъездов': {EntranceCount}");
            sb.AppendLine($"Высота этажа: {FloorHeight:0.##}");
            sb.AppendLine($"Количество квартир в подъезде: {FlatCountInEntrance}");
            sb.AppendLine($"количества квартир на этаже: {FlatCountOnFloor}");
            return sb.ToString();
        }
    }
}