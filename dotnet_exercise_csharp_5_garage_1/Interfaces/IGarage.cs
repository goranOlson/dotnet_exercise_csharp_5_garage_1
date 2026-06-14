using dotnet_exercise_csharp_5_garage_1.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_exercise_csharp_5_garage_1.Interfaces
{
    internal interface IGarage<T> : IEnumerable<T> where T : Vehicle
    {
        uint Capacity { get; }
        uint Count { get; }
        bool ParkCar(T vehicle);
        bool UnparkVehicle(T vehicle);
    }
}
