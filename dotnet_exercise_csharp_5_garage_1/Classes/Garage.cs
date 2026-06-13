using dotnet_exercise_csharp_5_garage_1.UI;

namespace dotnet_exercise_csharp_5_garage_1.Classes
{
    internal class Garage<T> where T : Vehicle
    {
        private Vehicle[] _parking = new Vehicle[10];

        private uint _capacity;  // Available parkings
        private uint _count;  // Occupied parkings
        private ConsoleUI _ui;

        public uint Capacity => _capacity;
        public uint Count => _count;

        public bool IsFull => _count >= _capacity;


        public Garage(ConsoleUI ui, uint capacity)
        {
            _ui = ui;
            _capacity = capacity;
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
                int freePos = GetFirstEmptyParking();
                if (freePos >= 0)
                {
                    _parking[freePos] = vehicle;
                    _count++;
                    success = true;
                    // Console.WriteLine("Ok parkerad, pos " + freePos);
                }
                else
                {
                    Console.WriteLine("Garaget är fullt!");
                }
            }

            return success;
        }


        public void PrintParkedVehicles()
        {
            Console.WriteLine($"{Environment.NewLine}___ Parkerade fordon ___ ({Count}/{Capacity})");
            if (_count > 0)
            {
                for (int i = 0; i < _parking.Length; i++)
                {
                    if (_parking[i] != null)
                    {
                        Console.WriteLine("- " + _parking[i].ToString());
                    }
                }
            }
            else
            {
                Console.WriteLine("Garaget är tomt!");
            }
            Console.WriteLine();
        }


        public void PrintVehiclesByType()
        {
            Console.WriteLine($"{Environment.NewLine}___ Parkerade fordon utifrån typ ___ ({Count}/{Capacity})");

            var types = ListVehicleTypes();
            if (types.Count > 0)
            {
                foreach (var item in types)
                {
                    Console.WriteLine($"{item.Key}: {item.Value} st");
                }
            }
            else
            {
                Console.WriteLine("Garaget är tomt!");
            }
            Console.WriteLine();
        }

        public bool UnparkCar(Vehicle vehicle)
        {
            bool success = false;

            for (int i = 0; i < _parking.Length; i++)
            {
                if (_parking[i] != null)
                {
                    if (_parking[i].RegNbr.ToUpper() == vehicle.RegNbr.ToUpper())
                    {
                        Console.WriteLine("Unparking RegNbr.: " + vehicle.RegNbr);

                        // ToDo - Error: _parking[i] = null; 
                        _parking[i] = null;
                        _count--;
                        success = true;
                    }
                }
            }

            return success;
        }

        private int GetFirstEmptyParking()
        {
            return Array.FindIndex(_parking, p => p == null);
        }

        private Dictionary<string, uint> ListVehicleTypes()
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
    }
}
