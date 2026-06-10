namespace dotnet_exercise_csharp_5_garage_1.Classes
{
    internal class Garage
    {
        private Vehicle[] _parking = new Vehicle[10];

        private int _capacity;  // Available parkings
        private int _count;  // Occupied parkings

        public int Capacity => _capacity;
        public int Count => _count;

        public bool IsFull => _count >= _capacity;


        public Garage(int capacity)
        {
            _capacity = capacity;
            Console.WriteLine($"Garage({_capacity})");
        }

        public bool ParkCar(Vehicle vehicle)
        {
            bool success = false;

            if (!IsFull)
            {
                int freePos = GetEmptyParking();
                if (freePos >= 0)
                {
                    Console.WriteLine($"freePos: {freePos}");
                    _parking[freePos] = vehicle;
                    _count++;
                    success = true;
                }
                else
                {
                    Console.WriteLine("Garaget är fullt!");
                }
            }

            return success;
        }

        public bool UnparkCar(Vehicle vehicle)
        {
            bool success = false;

            for (int i = 0; i < _parking.Length; i++)
            {
                if (_parking[i] != null)
                {
                    // Console.WriteLine($" # {i} - {_parking[i].RegNbr}");
                    if (_parking[i].RegNbr.ToUpper() == vehicle.RegNbr.ToUpper())
                    {
                         Console.WriteLine("Unparking RegNbr.: " + vehicle.RegNbr);

                        // ToDo - null, 
                        _parking[i] = null;  
                        _count--;
                        success = true;
                    }
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

        public Vehicle? GetVehicleByRegNbr(string regNbr)
        {
            Vehicle? vehicle = null;

            if (Count > 0 && regNbr != "")
            {
                var list = _parking.ToList();
                foreach (var item in list)
                {
                    if (item != null && item.RegNbr.ToUpper() == regNbr.ToUpper())
                    {
                        vehicle = item;
                    }
                }
            }

            return vehicle;
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



        private int GetEmptyParking()
        {
            int n = -1;

            // int numIndex = Array.IndexOf(numbers, numToRemove);
            for (int i = 0; i < _parking.Length; i++)
            {
                if (_parking[i] is null)
                {
                    n = i;
                    break;
                }
            }


            return n;
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
