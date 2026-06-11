using dotnet_exercise_csharp_5_garage_1.Classes;
using dotnet_exercise_csharp_5_garage_1.UI;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace dotnet_exercise_csharp_5_garage_1
{
    internal class Manager
    {
        private ConsoleUI _ui;  // => IUI
        
        private Garage _garage;

        public ConsoleUI UI => _ui;
        public Garage Garage => _garage;

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

                menuChoice = (int)UI.AskForUInt("Välj aktiviet (0 - 5)", 0, 1);  // Min/Max
                Console.WriteLine("menuChoice: " + menuChoice);

                switch(menuChoice)
                {
                    case 1:
                        if (!_garage.IsFull)
                        {
                            if (CheckinVehicle())
                            {
                                UI.PrintLine("Fordonet är parkerat");

                            }
                            else
                            {
                                UI.PrintLine("Kunde tyvärr inte parkera fordonet!");
                            }

                        }
                        else
                        {
                            UI.PrintLine("Garaget är tyvärr redan fullt!");
                        }
                        break;

                    case 0:
                        exit = true;
                        break;
                }
                UI.ExitMessageAction("Tryck på valfri tangent för att forsätta!");


                //if (menuChoice == 0)
                //{
                //    exit = true;
                //}
            } while (!exit);
        }

        private bool CheckinVehicle()
        {
            UI.PrintLine("___ Skapa car ___");


            // Car
            var vehicle = CreateCar();
            vehicle.ToString();

            return true;
        }

        private (string regNbr, string color, uint wheels) AskBaseVehicleData()
        {
            string regNbr = UI.AskForString("Registreringsnummer");
            string color = UI.AskForString("Färg");
            uint wheels = UI.AskForUInt("Antal hjul", 0);
            return (regNbr, color, wheels);
        }

        private Car CreateCar()
        {
            // string regNbr, string color, uint wheels, string fuleType, double cylinderVolume
            //UI.PrintLine("___ Skapa car ___");
            //string regNbr = UI.AskForString("Registreringsnummer");
            //string color = UI.AskForString("Färg");
            //uint wheels = UI.AskForUInt("Antal hjul", 0);

            var commonData = AskBaseVehicleData();

            string fuleType = UI.AskForString("Drivmedel");
            uint cylinderVolume = UI.AskForUInt("Cylindervolym", 0);




            return new Car(commonData.regNbr, commonData.color, commonData.wheels, fuleType, cylinderVolume);
        }

        // AskFixedString(){}

        private void Init(string uiType)
        {
            // TODO - if (uiType == "console")...
            _ui = new ConsoleUI();

            _garage = new Garage(AskForGarageSize());
            _handler = new Handler(_garage);

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
