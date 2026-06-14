using dotnet_exercise_csharp_5_garage_1.Classes;
using dotnet_exercise_csharp_5_garage_1.UI;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace dotnet_exercise_csharp_5_garage_1
{
    internal class Handler
    {
        private ConsoleUI _ui;
        private Garage<Vehicle> _garage;

        public uint Capacity => _garage.Capacity;
        public uint Count => _garage.Count;
        public bool IsFull => _garage.IsFull;
        private ConsoleUI UI => _ui;


        public Handler(ConsoleUI ui, Garage<Vehicle> garage)
        {
            _garage = garage;
            _ui = ui;
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

        

        public Dictionary<string, uint> GetVehicleByType()
        {
            return _garage.ListVehicleTypes();
        }

        public void SearchVehicleByRegNbr()
        {
            string regNbr = UI.AskForString("Ange fordonets reg.nummer");

            Vehicle? vehicle = _garage.GetVehicleByRegNbr(regNbr);

            if (vehicle != null)
            {
                UI.PrintLine($"{Environment.NewLine}Fordonsuppgifter:{Environment.NewLine}");
                UI.PrintLine(vehicle.ToString());
            }
            else
                UI.PrintLine($"{Environment.NewLine}Hittade inget fordon med reg.nummer: {regNbr}");
        }
    }
}
