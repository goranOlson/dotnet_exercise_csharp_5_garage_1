using System.Collections;

namespace dotnet_exercise_csharp_5_garage_1.Classes
{
    internal class Garage<T> : IEnumerable<T>, IGarage<T> where T : Vehicle
    {
        private readonly uint _capacity;
        private uint _count;
        private readonly Vehicle[] _parking;

        public uint Count => _count;
        public bool IsFull => _count >= _capacity;
        public uint Capacity => _capacity;

        public Garage(uint capacity)
        {
            _capacity = capacity;
            _count = 0;
            _parking = new Vehicle[capacity];
        }

        public bool AddVehicle(Vehicle vehicle)
        {
            bool success = false;

            if (!IsFull)
            {
                for (int i = 0; i < _parking.Length; i++)
                {
                    if (_parking[i] == null)
                    {
                        _parking[i] = vehicle;
                        _count++;
                        success = true;
                        break;
                    }
                }
            }

            return success;
        }

        public bool RemoveVehicle(Vehicle vehicle)
        {
            bool success = false;

            for (int i = 0; i < _parking.Length; i++)
            {
                if (_parking[i] != null)
                {
                    if (_parking[i].RegNbr.ToUpper() == vehicle.RegNbr.ToUpper())
                    {
                        _parking[i] = null!;
                        _count--;
                        success = true;
                    }
                }
            }

            return success;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in _parking)
            {
                if (item != null)
                    yield return (T)item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
