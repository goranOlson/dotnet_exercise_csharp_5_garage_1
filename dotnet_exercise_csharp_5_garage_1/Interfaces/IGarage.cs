using dotnet_exercise_csharp_5_garage_1.Classes;

namespace dotnet_exercise_csharp_5_garage_1.Interfaces
{
    internal interface IGarage<T> : IEnumerable<T> where T : Vehicle
    {
        uint Capacity { get; }
        uint Count { get; }
        bool AddVehicle(T vehicle);
        bool RemoveVehicle(T vehicle);
    }
}
