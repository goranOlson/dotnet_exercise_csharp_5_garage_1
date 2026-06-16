using dotnet_exercise_csharp_5_garage_1.Interfaces;
using dotnet_exercise_csharp_5_garage_1.UI;

namespace dotnet_exercise_csharp_5_garage_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IUI ui = new ConsoleUI();
            Manager manager = new Manager(ui);
            manager.Run();
        }
    }
}
