using dotnet_exercise_csharp_5_garage_1.Classes;

namespace dotnet_exercise_csharp_5_garage_1
{
    internal class Handler
    {
        private Garage _garage;



        public Handler(Garage garage)
        {
            _garage = garage;   
        }

        public bool ParkVehicle(Vehicle vehicle)
        {
            bool success = false;

            if (!_garage.IsFull)
            {
                success = _garage.ParkCar(vehicle);
            }

            return success;
        }
    }
}
