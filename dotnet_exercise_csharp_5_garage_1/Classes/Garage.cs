using dotnet_exercise_csharp_5_garage_1.UI;
using System.Collections;

namespace dotnet_exercise_csharp_5_garage_1.Classes
{
    internal class Garage<T> : IEnumerable<T> where T : Vehicle
    {
        private readonly uint _capacity;
        private uint _count;
        private Vehicle[] _parking;

        public uint Count => _count;
        public bool IsFull => _count >= _capacity;
        public uint Capacity => _capacity;


        public Garage(uint capacity)
        {
            _capacity = capacity;
            _count = 0;
            _parking = new Vehicle[capacity];
        }


        public Vehicle? GetVehicleByRegNbr(string regNbr)
        {
            Vehicle? vehicle = null;

            if (Count > 0 && !String.IsNullOrEmpty(regNbr))
            {
                var list = _parking.ToList();
                var result = list.FirstOrDefault(v => v != null && v.RegNbr.ToUpper() == regNbr.ToUpper());
                vehicle = result;
            }

            return vehicle;
        }

        public bool ParkCar(Vehicle vehicle)
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

        public bool UnparkVehicle(Vehicle vehicle)
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

        public Dictionary<string, uint> ListVehicleTypes()
        {
            Dictionary<string, uint> types = new Dictionary<string, uint>();
            if (Count > 0)
            {
                // Count unique vehicle types
                for (int i = 0; i < _capacity; i++)
                {
                    if (_parking[i] != null)
                    {
                        string name = _parking[i].GetType().Name;
                        if (types.ContainsKey(name))
                        {
                            types[name] += 1;
                        }
                        else
                        {
                            types.Add(name, 1);
                        }
                    }
                }
            }

            return types;
        }

        public IEnumerator<Vehicle> GetEnumerator()
        {
            //throw new NotImplementedException();
            foreach (var item in _parking)
            {
                if (item != null)
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
