using dotnet_exercise_csharp_5_garage_1.Classes;
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


            // SeedData
            // Test-data
            Airplane airplane = new Airplane("SN 1234", "silver", 16, 50, 4);           
            Boat boat = new Boat("Matilda", "Vit", 0, 65.5, 2);
            Bus bus = new Bus("EEE001", "Vit", 6, 9.8, 40);
            
            Car car = new ("ABC123", "Röd", 4, "Gasoline", 2100);

            Motorcycle motorcycle = new("MNO987", "Svart", 2, 2, 1300.5);


            Console.WriteLine();
            Console.WriteLine("> " + airplane.ToString());
            Console.WriteLine("> " + bus.ToString());
            Console.WriteLine("> " + boat.ToString());
            Console.WriteLine("> " + car.ToString());
            Console.WriteLine("> " + motorcycle.ToString());

            // Program menu



        }
    }
}
