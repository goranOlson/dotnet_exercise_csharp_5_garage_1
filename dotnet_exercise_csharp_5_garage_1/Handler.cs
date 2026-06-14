using dotnet_exercise_csharp_5_garage_1.Classes;
using dotnet_exercise_csharp_5_garage_1.UI;

namespace dotnet_exercise_csharp_5_garage_1
{
    internal class Handler
    {
        private Garage<Vehicle> _garage;
        //private IGarage<Vehicle> _garage;
        private ConsoleUI _ui;

        public uint Capacity => _garage.Capacity;
        public uint Count => _garage.Count;

        public bool IsFull => _garage.IsFull;

        public Handler(ConsoleUI ui, Garage<Vehicle> garage)
        {
            _garage = garage;
            _ui = ui;
        }

        public Vehicle? GetVehicleByRegNbr(string regNbr) => _garage?.GetVehicleByRegNbr(regNbr);

        // public bool ParkVehicle(Vehicle vehicle) => _garage.ParkCar(vehicle);
        public bool ParkVehicle(Vehicle vehicle)
        {
            bool success = false;

            if (!_garage.IsFull)
            {
                if (_garage.ParkCar(vehicle))
                    success = true;
                else
                    _ui.PrintLine("Kunde inte parkera fordenet - okänt fel!");
            }
            else
                _ui.PrintLine("Kan inte parkera fordonet - garaget är tyvärr fullt");

            return success;
        }


        public void PrintParkedVehicles(ConsoleUI ui) => _garage.PrintParkedVehicles(ui);

        public void PrintVehiclesByType(ConsoleUI ui) => _garage.PrintVehiclesByType(ui);

        public bool UnparkVehicle(Vehicle vehicle) => _garage.UnparkVehicle(vehicle);




    }
}
