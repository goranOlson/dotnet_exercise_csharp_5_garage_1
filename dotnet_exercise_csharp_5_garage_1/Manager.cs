using dotnet_exercise_csharp_5_garage_1.Classes;
using dotnet_exercise_csharp_5_garage_1.Interfaces;
using dotnet_exercise_csharp_5_garage_1.UI;
using System.ComponentModel.Design;
using System.Drawing;
using System.Runtime.CompilerServices;

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
                UI.ExitMessageAction("Tryck på valfri tangent för att forsätta!");
            } while (!exit);

        }
       

        private void CheckinVehicle()
        {
            PrintPageHeader();
            PrintSubHeader("Registrera fordon");

            if (!Handler.IsFull)
            {
                UI.PrintLine($"Ange fordonstyp:{Environment.NewLine}");
                UI.PrintLine("1 Bil");
                UI.PrintLine("2 Buss");
                UI.PrintLine("3 Båt");
                UI.PrintLine("4 Flygplan");
                UI.PrintLine("5 Motorcykel");
                UI.PrintLine($"0 Avbryt{Environment.NewLine}");

                uint menuChoice = UI.AskForUInt("Välj aktiviet (0 - 5)", 0, 5);

                PrintPageHeader();
                PrintSubHeader($"Registerar fordonet");

                // Ask user for data that all vehicle types share
                var commonData = AskBaseVehicleData();

                switch (menuChoice)
                {
                    case 1:
                        ParkCar(commonData.regNbr, commonData.color, commonData.wheels);
                        break;
                    case 2:
                        ParkBus(commonData.regNbr, commonData.color, commonData.wheels);
                        break;
                    case 3:
                        ParkBoat(commonData.regNbr, commonData.color, commonData.wheels);
                        break;
                    case 4:
                        ParkAirplane(commonData.regNbr, commonData.color, commonData.wheels);
                        break;
                    case 5:
                        ParkMotorcycle(commonData.regNbr, commonData.color, commonData.wheels);
                        break;

                    default:
                        break;
                }
            }
            else
                UI.PrintLine("Garaget är tyvärr fullt - kan inte parkera flera fordon!");
        }

        private void ParkCar(string regNbr, string color, uint nbrWheels)
        {
            string fuleType = UI.AskForString("Drivmedel");
            uint cylinderVolume = UI.AskForUInt("Cylindervolym");

            if (Handler.ParkCar(regNbr, color, nbrWheels, fuleType, cylinderVolume))
                UI.PrintLine($"{Environment.NewLine}Fordonet är nu parkerat");
            else
                UI.PrintLine($"{Environment.NewLine}Kunde inte parkera fordonet - okänt fel!");
        }

        private void ParkBus(string regNbr, string color, uint nbrWheels)
        {
            uint length = UI.AskForUInt("Längd");
            uint cylinderVolume = UI.AskForUInt("Antal säten");

            if (Handler.ParkBoat(regNbr, color, nbrWheels, length, cylinderVolume))
                UI.PrintLine($"{Environment.NewLine}Fordonet är nu parkerat");
            else
                UI.PrintLine($"{Environment.NewLine}Kunde inte parkera fordonet - okänt fel!");
        }

        private void ParkBoat(string regNbr, string color, uint nbrWheels)
        {
            uint lenghtInFoot = UI.AskForUInt("Längd i fot");
            uint numberOfEngines = UI.AskForUInt("Antal motorer", 0);

            if (Handler.ParkBus(regNbr, color, nbrWheels, lenghtInFoot, numberOfEngines))
                UI.PrintLine($"{Environment.NewLine}Fordonet är nu parkerat");
            else
                UI.PrintLine($"{Environment.NewLine}Kunde inte parkera fordonet - okänt fel!");
        }

        private void ParkAirplane(string regNbr, string color, uint nbrWheels)
        {
            uint nbrSeats = UI.AskForUInt("Antal säten");
            uint nbrEngines = UI.AskForUInt("Antal motorer", 0);

            if (Handler.ParkAirplane(regNbr, color, nbrWheels, nbrSeats, nbrEngines))
                UI.PrintLine($"{Environment.NewLine}Fordonet är nu parkerat");
            else
                UI.PrintLine($"{Environment.NewLine}Kunde inte parkera fordonet - okänt fel!");
        }

        private void ParkMotorcycle(string regNbr, string color, uint nbrWheels)
        {
            uint nbrSeats = UI.AskForUInt("Antal säten");
            uint cylinderVolume = UI.AskForUInt("Cylindervolym");

            if (Handler.ParkMotorcycle(regNbr, color, nbrWheels, nbrSeats, cylinderVolume))
                UI.PrintLine($"{Environment.NewLine}Fordonet är nu parkerat");
            else
                UI.PrintLine($"{Environment.NewLine}Kunde inte parkera fordonet - okänt fel!");
        }


        private (string regNbr, string color, uint wheels) AskBaseVehicleData()
        {
            UI.PrintLine($"{Environment.NewLine}Ange fordonsuppgifer:{Environment.NewLine}");

            string regNbr = UI.AskForString("Reg.nummer");
            string color = UI.AskForString("Färg");
            uint wheels = UI.AskForUInt("Antal hjul", 0);

            return (regNbr, color, wheels);
        }



        private void CheckOutVehicle()
        {
            PrintPageHeader();
            PrintSubHeader("Checka ut fordon");

            if (_garage.Count > 0)
            {
                string regNbr = UI.AskForString("Ange reg.nummer");
                Vehicle? vehicle = _garage.GetVehicleByRegNbr(regNbr);

                if (vehicle != null)
                {
                    if (Handler.UnparkVehicle(vehicle))
                        UI.PrintLine("Fordonet är nu utcheckat");
                    else
                        UI.PrintLine($"{Environment.NewLine}Fel - kunde inte checka ut fordonet!?");
                }
                else
                    UI.PrintLine($"{Environment.NewLine}Hittade tyvärr inget fordon med reg.nummer: " + regNbr);
            }
            else
                UI.PrintLine($"{Environment.NewLine}Garaget är tomt!");
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
            // return $"{Handler.Capacity - Handler.Count} platser lediga av {Handler.Capacity}";
            return $"{Handler.Capacity - Handler.Count} av {Handler.Capacity} platser lediga";
        }

        private void PrintParkedVehicles()
        {
            PrintPageHeader();
            PrintSubHeader($"Parkerade fordon ({_garage.Count} st)");
            
            // List parked vehicles
            foreach (var item in _garage)
            {
                UI.PrintLine(item.ToString());
            }
        }

        private void PrintVehicleByType()
        {
            PrintPageHeader();
            PrintSubHeader($"Parkerade fordon utifrån typ ({CreateOccupancyText()})");

            Dictionary<string, uint> listTypes = Handler.GetVehicleByType();
            if (listTypes.Count > 0)
            {
                foreach (var vehicle in listTypes)
                {
                    UI.PrintLine($"{vehicle.Key}: {vehicle.Value} st");
                }
            }
            else
            {
                UI.PrintLine("Parkerade fordon saknas!");
            }

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
                new Boat("Matilda", "Vit", 0, 65, 2),
                new Bus("EEE001", "Vit", 6, 9, 40),
                new Car("YYY099", "Röd", 4, "Gasoline", 2100),
                new Car("ZZZ123", "Blå", 4, "Gasoline", 2100),
                new Motorcycle("MNO987", "Svart", 2, 2, 1300)
            };

            uint max = testData.Length > garage.Capacity ? garage.Capacity : (uint)testData.Length;

            for (int i = 0; i < max; i++)
            {
                garage.AddVehicle(testData[i]);
            }
        }
    }
}
