using dotnet_exercise_csharp_5_garage_1.Classes;
using dotnet_exercise_csharp_5_garage_1.UI;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace dotnet_exercise_csharp_5_garage_1
{
    internal class Manager
    {
        private ConsoleUI _ui;  // => IUI
        
        private Garage<Vehicle> _garage;

        private ConsoleUI UI => _ui;

        private Garage<Vehicle> Garage => _garage;

        private Handler _handler;
        
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
                UI.PrintLine($"Välkommen till Garage 1.0          {_handler.Capacity - _handler.Count} av {_handler.Capacity} platser lediga");
                UI.PrintLine($"======================================================={Environment.NewLine}");
                // Print menu

                UI.PrintLine("    Meny");
                UI.PrintLine("--------------");
                UI.PrintLine("1 Incheckning");
                UI.PrintLine("   2 Utcheckning");
                UI.PrintLine("   3 Sök fordon");
                UI.PrintLine("   4 Skapa nytt fordon");
                UI.PrintLine("8 Lista fordon");
                UI.PrintLine("   9 Lista fordonstyper");
                UI.PrintLine("0 Avsluta");
                UI.PrintLine("");

                menuChoice = (int)UI.AskForUInt("Välj aktiviet (0 - 9)", 0, 8);  // Min/Max
                Console.WriteLine("menuChoice: " + menuChoice);

                switch (menuChoice)
                {
                    case 1:
                        CheckinVehicle(); 
                        break;
                    case 8:
                        _garage.PrintParkedVehicles();
                        break;
                    case 0:
                        exit = true;
                        break;
                }
                UI.ExitMessageAction("Tryck på valfri tangent för att forsätta!");
            } while (!exit);
        }



        private void CheckinVehicle()
        {
            UI.PrintLine("_____ Registrera fordon _____");  // "fordon"
            
            if (!_handler.IsFull)
            {
                // Type of vehicle?
                var vehicle = CreateVehicle();

                if (_handler.ParkVehicle(vehicle!))
                    UI.PrintLine($"{Environment.NewLine}Fordonet är nu parkerat!");
                else
                    UI.PrintLine("Kunde tyvärr inte parkera fordonet - okänt fel!");

                //string msg = _handler.newPark(vehicle) ? "Fordonet är parkerat" : "Kunde tyvärr inte parkera fordonet - okänt fel!";
                //UI.PrintLine(msg);
            }
            else
            {
                UI.PrintLine("Garaget är tyvärr redan fullt!");
            }
        }



        private (string regNbr, string color, uint wheels) AskBaseVehicleData()
        {
            string regNbr = UI.AskForString("Registreringsnummer");
            string color = UI.AskForString("Färg");
            uint wheels = UI.AskForUInt("Antal hjul", 0);
            return (regNbr, color, wheels);
        }

        private Vehicle? CreateVehicle()
        {
            Vehicle? vehicle = null;

            UI.PrintLine("");
            UI.PrintLine("Ange fordonstyp");
            UI.PrintLine("");
            UI.PrintLine("1 Bil");
            UI.PrintLine("2 Buss");
            UI.PrintLine("3 Båt");
            UI.PrintLine("4 Flygplan");
            UI.PrintLine("5 Motorcykel");

            uint menuChoice = UI.AskForUInt("Välj aktiviet (1 - 5)", 1, 5);  // Min/Max
            // Console.WriteLine("menuChoice: " + menuChoice);

            switch (menuChoice)
            {
                case 1:
                    vehicle = CreateCar();
                    break;

                default:
                    break;
            }

            return vehicle;
        }

        private Vehicle CreateCar()
        {
            // string regNbr, string color, uint wheels, string fuleType, double cylinderVolume

            // Ask user for data that all vehicle types share
            var commonData = AskBaseVehicleData();

            // Ask user for date for date specific for 'Car'
            string fuleType = UI.AskForString("Drivmedel");
            uint cylinderVolume = UI.AskForUInt("Cylindervolym", 0);
           
            return new Car(commonData.regNbr, commonData.color, commonData.wheels, fuleType, cylinderVolume);
        }


        private void Init(string uiType)
        {
            // TODO - if (uiType == "console")...
            _ui = new ConsoleUI();

            _garage = new Garage<Vehicle>(_ui, AskForGarageSize());
            _handler = new Handler(_ui, _garage);

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

        private void SeedData(Garage<Vehicle> garage)
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
