using dotnet_exercise_csharp_5_garage_1.Classes;

namespace dotnet_exercise_csharp_5_garage_1.Interfaces
{
    internal interface IGarage<T> where T : Vehicle
    {
        uint Capacity { get; }
        uint Count { get; }
        bool IsFull { get; }

        bool AddVehicle(Vehicle vehicle);
        IEnumerator<T> GetEnumerator();
        bool RemoveVehicle(Vehicle vehicle);
    }
}