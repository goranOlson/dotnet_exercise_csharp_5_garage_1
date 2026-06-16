using dotnet_exercise_csharp_5_garage_1.Classes;

namespace dotnet_exercise_csharp_5_garage_1.Interfaces
{
    internal interface IHandler
    {
        uint Capacity { get; }
        uint Count { get; }
        bool IsFull { get; }

        Dictionary<string, uint> GetVehicleByType();
        bool ParkAirplane(string regNbr, string color, uint nbrWheels, uint nbrSeats, uint nbrEngines);
        bool ParkBoat(string regNbr, string color, uint nbrWheels, uint lenghtInFoot, uint numberOfEngines);
        bool ParkBus(string regNbr, string color, uint nbrWheels, uint length, uint nbrSeats);
        bool ParkCar(string regNbr, string color, uint nbrWheels, string fuleType, uint cylinderVolume);
        bool ParkMotorcycle(string regNbr, string color, uint nbrWheels, uint nbrOfSeats, uint cylinderVolume);
        Vehicle? SearchByRegNbr(string regNbr);
        List<Vehicle> SearchWithFilter(string type, string color, int nbrWheels);
        bool UnparkVehicle(Vehicle vehicle);
    }
}