using dotnet_exercise_csharp_5_garage_1.Classes;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;

namespace dotnet_exercise_csharp_5_garage_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            uint garageSize = 10;

            Garage garage = new(garageSize);

            //=== SeedData
            SeedData(garage);


            //=== Init menu
            Console.WriteLine($"Välkommen till Garage 1.0 - {garageSize} platser");
            Console.WriteLine($"======================================{Environment.NewLine}");

            Console.WriteLine($"Antal platser: " + garage.Capacity);
            Console.WriteLine($"Antal använda platser: " + garage.Count);
            Console.WriteLine($"Antal lediga platser: " + (garage.Capacity - garage.Count));

            Console.WriteLine($"Garaget fullt: " + garage.IsFull);
            Console.WriteLine();

            garage.PrintParkedVehicles();

            //=== Park new airplane
            Console.WriteLine("Parker nytt flygplan:");
            if (!garage.ParkCar(new Airplane("YN 1234", "silver", 16, 50, 4)))
            {
                Console.WriteLine("Parkeringsfel! Fullt? " + garage.IsFull);
            }
            Console.WriteLine();

            //=== Unpark by regNbr
            string regNbr = "ABc123";
            Console.WriteLine($"# Unpark by regNbr '{regNbr}': ");
            var vehicle = garage.GetVehicleByRegNbr(regNbr);
            if (vehicle != null)
            {
                Console.WriteLine("# Unparking " + vehicle.RegNbr);
                if (!garage.UnparkCar(vehicle))
                    Console.WriteLine("FAILED TO UNPARK: " + vehicle.RegNbr);
                else
                    Console.WriteLine("Unparked now!");
            }
            else
            {
                Console.WriteLine("HTIIAR INTE: REG.NR " + regNbr);
            }

            //=== List vehicles
            garage.PrintParkedVehicles();

            garage.PrintVehiclesByType();

            Console.WriteLine($"Garage size => " + garage.Capacity);
            Console.WriteLine($"Garage count =>: " + garage.Count);
        }

        public static void SeedData(Garage garage)
        {
            garage.ParkCar(new Airplane("SN 1234", "silver", 16, 50, 4));
            garage.ParkCar(new Boat("Matilda", "Vit", 0, 65.5, 2));
            garage.ParkCar(new Bus("EEE001", "Vit", 6, 9.8, 40));
            garage.ParkCar(new Car("ABC123", "Röd", 4, "Gasoline", 2100));
            garage.ParkCar(new Car("ZZZ123", "Blå", 4, "Gasoline", 2100));
            garage.ParkCar(new Motorcycle("MNO987", "Svart", 2, 2, 1300.5));
        }
    }
}
