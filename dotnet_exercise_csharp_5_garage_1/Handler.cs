using dotnet_exercise_csharp_5_garage_1.Classes;
using dotnet_exercise_csharp_5_garage_1.UI;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace dotnet_exercise_csharp_5_garage_1
{
    internal class Handler
    {
        private readonly Garage<Vehicle> _garage;
        public uint Capacity => _garage.Capacity;
        public uint Count => _garage.Count;
        public bool IsFull => _garage.IsFull;


        public Handler(Garage<Vehicle> garage)
        {
            _garage = garage;
        }


        public bool ParkCar(string regNbr, string color, uint nbrWheels, string fuleType, uint cylinderVolume)
        {
            bool success = false;

            if (!_garage.IsFull) { 
                Car car = new Car(regNbr, color, nbrWheels, fuleType, cylinderVolume);
                if (_garage.AddVehicle(car))
                    success = true;
            }

            return success;
        }

        public bool ParkBus(string regNbr, string color, uint nbrWheels, uint length, uint nbrSeats)
        {
            bool success = false;

            if (!_garage.IsFull)
            {
                Bus bus = new Bus(regNbr, color, nbrWheels, length, nbrSeats);
                if (_garage.AddVehicle(bus))
                    success = true;
            }

            return success;
        }

        public bool ParkBoat(string regNbr, string color, uint nbrWheels, uint lenghtInFoot, uint numberOfEngines)
        {
            bool success = false;

            if (!_garage.IsFull)
            {
                Boat boat = new Boat(regNbr, color, nbrWheels, lenghtInFoot, numberOfEngines);
                if (_garage.AddVehicle(boat))
                    success = true;
            }

            return success;
        }

        public bool ParkAirplane(string regNbr, string color, uint nbrWheels, uint nbrSeats, uint nbrEngines)
        {
            bool success = false;

            if (!_garage.IsFull)
            {
                Airplane airplane = new Airplane(regNbr, color, nbrWheels, nbrSeats, nbrEngines);
                if (_garage.AddVehicle(airplane))
                    success = true;
            }

            return success;
        }

        public bool ParkMotorcycle(string regNbr, string color, uint nbrWheels, uint nbrOfSeats, uint cylinderVolume)
        {
            bool success = false;

            if (!_garage.IsFull)
            {
                Motorcycle mc = new Motorcycle(regNbr, color, nbrWheels, nbrOfSeats, cylinderVolume);
                if (_garage.AddVehicle(mc))
                    success = true;
            }

            return success;
        }

        public bool UnparkVehicle(Vehicle vehicle)
        {
            bool success = false;

            if (Count > 0)
            {
                if (_garage.RemoveVehicle(vehicle))
                    success = true;
            }

            return success;
        }

        public List<Vehicle> SearchWithFilter(string type, string color, int nbrWheels)
        {
            IEnumerable<Vehicle> query = _garage.Where(v => v != null);
            
            // Remove results if no search criterias appended!
            if (type == "" && color == "" && nbrWheels < 0)
            {
                query = query.Where(v => v.GetType().Name.ToUpper() == "");
            }
            else
            {
                // Else append search criterias
                if (type != "")
                    query = query.Where(v => v.GetType().Name.ToUpper() == type.ToUpper());

                if (color != "")
                    query = query.Where(v => v.Color.ToUpper() == color.ToUpper());

                if (nbrWheels >= 0)
                    query = query.Where(v => v.NbrOfWheels == nbrWheels);
            }

            return query.ToList();
        }

        public Dictionary<string, uint> GetVehicleByType()
        {
            Dictionary<string, uint> types = new Dictionary<string, uint>();

            if (Count > 0)
            {
                // Count unique vehicle by type
                foreach (Vehicle v in _garage)
                {
                    string name = v.GetType().Name;  // Get class name
                    if (types.ContainsKey(name))
                        types[name] += 1;
                    else
                        types.Add(name, 1);
                }
            }

            return types;
        }

        public Vehicle? SearchByRegNbr(string regNbr)
        {
            return _garage.FirstOrDefault(v => v != null && v.RegNbr.ToUpper() == regNbr.ToUpper());
        }
    }
}
