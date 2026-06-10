using dotnet_exercise_csharp_5_garage_1.Classes;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;

namespace dotnet_exercise_csharp_5_garage_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Init menu
            Console.WriteLine($"Välkommen till Garage 1.0");
            Console.WriteLine($"========================={Environment.NewLine}");
            // Get garage size

            //Vehicle[] garage2 = new Vehicle[12];

            // SeedData
            // Test-data
            Airplane airplane = new Airplane("SN 1234", "silver", 16, 50, 4);           
            Boat boat = new Boat("Matilda", "Vit", 0, 65.5, 2);
            Bus bus = new Bus("EEE001", "Vit", 6, 9.8, 40);
            Car car = new ("ABC123", "Röd", 4, "Gasoline", 2100);
            Car car2 = new("ZZZ123", "Blå", 4, "Gasoline", 2100);
            Motorcycle motorcycle = new("MNO987", "Svart", 2, 2, 1300.5);

            //garage2.Append(airplane);  // 

            Console.WriteLine();
            Console.WriteLine("> " + airplane.ToString());
            Console.WriteLine("> " + bus.ToString());
            Console.WriteLine("> " + boat.ToString());
            Console.WriteLine("> " + car.ToString());
            Console.WriteLine("> " + car2.ToString());
            Console.WriteLine("> " + motorcycle.ToString());

            // Program menu

            Garage garage = new(2);
            Console.WriteLine($"Garage size: " + garage.Capacity);
            Console.WriteLine($"Garage count: " + garage.Count);
            Console.WriteLine($"Garage IsFull: " + garage.IsFull);
            Console.WriteLine();

            if (!garage.ParkCar(car))
                Console.WriteLine("Failed parked car");
            Console.WriteLine($"Garage count: " + garage.Count);
            Console.WriteLine();

            if (!garage.ParkCar(car2))
                Console.WriteLine("Failed parked car2");
            Console.WriteLine($"Garage count: " + garage.Count);
            Console.WriteLine();

            garage.PrintParkedVehicles();

            if (!garage.ParkCar(airplane))
                Console.WriteLine("Failed parked airplane");
            Console.WriteLine($"Garage count: " + garage.Count);
            Console.WriteLine();

            garage.PrintParkedVehicles();

            //if (!garage.UnparkCar(car))
            //    Console.WriteLine("Failed to unpark parked car");

            if (!garage.UnparkCar(car))
                Console.WriteLine("Failed to unpark parked car");

            //garage.PrintGarage();

            if (!garage.UnparkCar(car2))
                Console.WriteLine("Failed to unpark parked car2");

            garage.PrintParkedVehicles();



            //if (!garage.ParkCar(airplane))
            //    Console.WriteLine("Failed parking airplane");
            //Console.WriteLine($"Garage count: " + garage.Count);
            //Console.WriteLine();
            //Console.WriteLine();

            //Console.WriteLine($"\nUnpark vehicle airplane");
            //var unpark = garage.UnparkCar(airplane);
            //if (!unpark)
            //    Console.WriteLine("Failed unpark airplane!");
            //Console.WriteLine($"Garage count: " + garage.Count);

            //Console.WriteLine($"\nUnpark vehicle airplane - AGAIN");
            //var unpark5 = garage.UnparkCar(airplane);
            //if (unpark5)
            //    Console.WriteLine("unparked airplane!");
            //Console.WriteLine($"Garage count: " + garage.Count);

            //Console.WriteLine($"\nUnpark vehicle car");
            //var unpark2 = garage.UnparkCar(car);
            //if (unpark2)
            //{
            //    Console.WriteLine("unparked car!");
            //    Console.WriteLine($"Garage count: " + garage.Count);
            //}
            //else
            //{
            //    Console.WriteLine("failed unpark!");
            //}

            Console.WriteLine($"Garage count: " + garage.Count);


            //Console.WriteLine($"\nUnpark vehicle car");
            //var unpark4 = garage.UnparkCar(car);
            //if (unpark4)
            //{
            //    Console.WriteLine("unparked!");
            //    Console.WriteLine($"Garage count: " + garage.Count);
            //}
            //else
            //{
            //    Console.WriteLine("failed unpark!");
            //}

            Console.WriteLine($"Garage count => " + garage.Count);
        }





    }
}
