using dotnet_exercise_csharp_5_garage_1.Classes;
using dotnet_exercise_csharp_5_garage_1.UI;

namespace dotnet_exercise_csharp_5_garage_1
{
    internal class Manager
    {
        private ConsoleUI _ui;  // => IUI
        
        private Garage _garage;

        public ConsoleUI UI => _ui;
        public Garage Garage => _garage;
        
        public Manager(string uiType)
        {
            Init(uiType);
        }

        public void Run()
        {
            // Menu
            bool exit = false;
            int menuChoice = -1;

            do
            {
                UI.Clear();
                // Print Header
                // UI.PrintLine($"Välkommen till Garage 1.0 - {garageSize} platser, {GarageSize} lediga");
                UI.PrintLine($"Välkommen till Garage 1.0          {Garage.Capacity - Garage.Count} av {Garage.Capacity} platser lediga");
                UI.PrintLine($"======================================================={Environment.NewLine}");
                // Print menu

                UI.PrintLine("    Meny");
                UI.PrintLine("--------------");
                UI.PrintLine("1 Incheckning");
                UI.PrintLine("2 Utcheckning");
                UI.PrintLine("3 Sök fordon");
                UI.PrintLine("4 Skapa nytt fordon");
                UI.PrintLine("8 Lista fordon");
                UI.PrintLine("9 Lista fordonstyper");
                UI.PrintLine("0 Avsluta");
                UI.PrintLine("");

                menuChoice = (int)UI.AskForUInt("Välj aktiviet (0 - 5)", 0, 0);  // Min/Max
                Console.WriteLine("menuChoice: " + menuChoice);

                // if / swich

                if (menuChoice == 0)
                {
                    exit = true;
                }
            } while (!exit);
        }

        private void Init(string uiType)
        {
            // TODO - if (uiType == "console")...
            _ui = new ConsoleUI();

            _garage = new Garage(AskForGarageSize());

            if (AskForSeedingData() == true)
            {
                SeedData(Garage);
            }
        }

        private uint AskForGarageSize()
        {
            UI.PrintLine($"Välkommen till Garage 1.0");
            UI.PrintLine($"========================={Environment.NewLine}");

            return UI.AskForUInt("Ange antal platser i garaget");
        }

        private bool AskForSeedingData()
        {
            string str = UI.AskForString("Fyll garaget med 6 fordon (j/n)", (input) =>
            {
                if (!String.IsNullOrEmpty(input) && input.Length == 1 && (input.ToUpper() == "J" || input.ToUpper() == "N"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            
            });

            return str.ToUpper() == "J" ? true : false;
        }

        private void SeedData(Garage garage)
        {
            Vehicle[] testData = new Vehicle[]
            {
                new Airplane("SN 1234", "silver", 16, 50, 4),
                new Boat("Matilda", "Vit", 0, 65.5, 2),
                new Bus("EEE001", "Vit", 6, 9.8, 40),
                new Car("ABC123", "Röd", 4, "Gasoline", 2100),
                new Car("ZZZ123", "Blå", 4, "Gasoline", 2100),
                new Motorcycle("MNO987", "Svart", 2, 2, 1300.5)
            };

            uint max = testData.Length > garage.Capacity ? garage.Capacity : (uint)testData.Length;

            for (int i = 0; i < max; i++)
            {
                garage.ParkCar(testData[i]);
            }
        }
    }
}
