using dotnet_exercise_csharp_5_garage_1.Classes;
using dotnet_exercise_csharp_5_garage_1.UI;
using System.Net.Quic;

namespace dotnet_exercise_csharp_5_garage_1
{
    internal class Handler
    {
        private Garage<Vehicle> _garage;
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

        public bool ParkVehicle(Vehicle vehicle) => _garage.ParkCar(vehicle);

        public void PrintParkedVehicles(ConsoleUI ui) => _garage.PrintParkedVehicles(ui);

        public void PrintVehiclesByType(ConsoleUI ui) => _garage.PrintVehiclesByType(ui);
        public bool UnparkVehicle(Vehicle vehicle) => _garage.UnparkVehicle(vehicle);
    }
}
