// 1. Реализовать класс для описания здания (уникальный номер здания, высота, этажность, количество квартир, подъездов).
//  Поля сделать закрытыми, предусмотреть методы для заполнения полей и получения значений полей для печати.
//  Добавить методы вычисления высоты этажа, количества квартир в подъезде, количества квартир на этаже и т.д. 
// Предусмотреть возможность, чтобы уникальный номер здания генерировался программно. 
// Для этого в классе предусмотреть статическое поле, которое бы хранило последний использованный номер здания, 
// и предусмотреть метод, который увеличивал бы значение этого поля.

namespace Task_1
{
    public sealed class Building
    {
        private static int _lastNumber;

        private int _number;

        private int _floorCount;

        private double _height;

        private int _flatCount;

        private int _entranceCount;

        private static int GenerateNumber() => _lastNumber++;

        public Building() => _number = GenerateNumber();

        public int GetNumber() => _number;

        public int GetFloorCount() => _floorCount;

        public void SetFloorCount(int floorCount) => _floorCount = floorCount;

        public double GetHeight() => _height;

        public void SetHeight(int height) => _height = height;

        public int GetFlatCount() => _flatCount;

        public void SetFlatCount(int flatCount) => _flatCount = flatCount;

        public int GetEntranceCount() => _entranceCount;

        public void SetEntranceCount(int entranceCount) => _entranceCount = entranceCount;

        public double GetFloorHeight()
        {
            if (_floorCount != 0)
            {
                return _height / _floorCount;
            }
            else
            {
                return 0;
            }
        }

        public int GetFlatCountInEntrance()
        {
            if (_entranceCount != 0)
            {
                return _flatCount / _entranceCount;
            }
            else
            {
                return 0;
            }
        }

        public int GetFlatCountOnFloor()
        {
            if (_floorCount != 0)
            {
                return GetFlatCountInEntrance() / _floorCount;
            }
            else
            {
                return 0;
            }
        }
    }
}