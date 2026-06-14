using dotnet_exercise_csharp_5_garage_1.Classes;
using dotnet_exercise_csharp_5_garage_1.UI;

namespace dotnet_exercise_csharp_5_garage_1
{
    internal class Manager
    {
        private ConsoleUI _ui;
        private Garage<Vehicle> _garage;
        private Handler _handler;

        private ConsoleUI UI => _ui;
        private Garage<Vehicle> Garage => _garage;
        private Handler Handler => _handler;
        

        public Manager(string uiType)
        {
            Init(uiType);
        }

        private void Init(string uiType)
        {
            // TODO - if (uiType == "console")...
            _ui = new ConsoleUI();

            _garage = new Garage<Vehicle>(AskForGarageSize());
            _ui.PrintLine("");
            _handler = new Handler(_ui, _garage);

            if (AskForSeedingData() == true)
            {
                SeedData(Garage);
            }
        }

        public void Run()
        {
            // Menu
            bool exit = false;
            uint menuChoice;

            do
            {
                PrintPageHeader();

                // Print menu
                UI.PrintLine("    Huvudmeny");
                UI.PrintLine("-----------------");
                UI.PrintLine("1 Incheckning");
                UI.PrintLine("2 Utcheckning");
                UI.PrintLine("3 Sök fordon");
                UI.PrintLine("   4 Specialsök");
                UI.PrintLine("8 Lista fordon");
                UI.PrintLine("9 Lista fordon efter typ");
                UI.PrintLine("0 Avsluta");
                UI.PrintLine("");
                menuChoice = UI.AskForUInt("Välj aktiviet (0 - 9)", 0, 9);

                switch (menuChoice)
                {
                    case 1:
                        CheckinVehicle(); 
                        break;
                    case 2:
                        CheckOutVehicle();
                        break;
                    case 3:
                        SearchVehicleByRegNbr();
                        break;

                    //...

                    case 8:
                        PrintParkedVehicles();
                        break;

                    case 9:
                        PrintVehicleByType();
                        break;

                    case 0:
                        exit = true;
                        UI.PrintLine($"{Environment.NewLine}Programmet avslutas...");
                        break;
                }
                UI.ExitMessageAction("Tryck på valfri tangent för att forsätta!!!!!");
            } while (!exit);

        }
       

        private void CheckinVehicle()
        {
            PrintPageHeader();
            PrintSubHeader("Registrera fordon");
            
            // Create and park the vehicle
            Handler.ParkVehicle();
            // UI.ExitMessageAction();
        }

        private void CheckOutVehicle()
        {
            PrintPageHeader();
            PrintSubHeader("Checka ut fordon");

            // Checkout vehicle and show info
            Handler.RemoveVehicle();
            //UI.ExitMessageAction();
        }

        private void SearchVehicleByRegNbr()
        {
            PrintPageHeader();
            PrintSubHeader("Sök fordon med reg.nummer");

            // Search parked vehicle by reg.nbr and show details
            Handler.SearchVehicleByRegNbr();
            // UI.ExitMessageAction();
        }

        private void SearchSpecial()
        {
            PrintPageHeader();
            PrintSubHeader("Sök fordon utifrån egenskaper");
            UI.PrintLine($"Tom rad om ingen data{Environment.NewLine}");

            // Color, NbrOfWheels
            // Alla svarta fordon med 4 hjul
            // Alla motorcyklar som är rosa och har 3 hjul
            // Alla lastbilar
            // Alla röda fordon

            string vehicleType = UI.AskForString("Fordonstyp", null, true);
            string color = UI.AskForString("Färg", null, true);
            string nbrWheels = UI.AskForString("Antal hjul", null, true);
            


        }


        private string CreateOccupancyText()
        {
            return $"{Handler.Capacity - Handler.Count} platser lediga av {Handler.Capacity}";
        }

        private void PrintParkedVehicles()
        {
            PrintPageHeader();
            PrintSubHeader($"Parkerade fordon ({_garage.Count} st)");
            
            // List parked vehicles
            Handler.PrintParked();
        }
        private void PrintVehicleByType()
        {
            PrintPageHeader();
            PrintSubHeader($"Parkerade fordon utifrån typ ({CreateOccupancyText()})");
            // List parked vehicles by types with count
            Handler.PrintByType();
        }
        private void PrintPageHeader()
        {
            UI.Clear();
            // Print Header
            //UI.PrintLine($"Välkommen till Garage 1.0          {_handler.Capacity - _handler.Count} av {_handler.Capacity} platser lediga");
            UI.PrintLine($"Välkommen till Garage 1.0          {CreateOccupancyText()}");
            UI.PrintLine($"========================================================={Environment.NewLine}");
        }
        private void PrintSubHeader(string header)
        {
            UI.PrintLine($"_______ {header} _______{Environment.NewLine}");
        }





        private uint AskForGarageSize()
        {
            UI.PrintLine($"Välkommen till Garage 1.0");
            UI.PrintLine($"========================={Environment.NewLine}");

            return UI.AskForUInt($"Ange antal platser i garaget", 1);
        }

        private bool AskForSeedingData()
        {
            string str = UI.AskForString("Fyll garaget med upp till 6 fordon (j/n)", (input) =>
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
                new Car("YYY099", "Röd", 4, "Gasoline", 2100),
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
