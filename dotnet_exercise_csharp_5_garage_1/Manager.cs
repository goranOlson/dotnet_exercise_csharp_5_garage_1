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
            _handler = new Handler(_garage);

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
                UI.PrintLine("       Huvudmeny");
                UI.PrintLine("-------------------------");
                UI.PrintLine("1 Incheckning");
                UI.PrintLine("2 Utcheckning");
                UI.PrintLine("3 Sök på reg.nummer");
                UI.PrintLine("4 Sök med filter");
                UI.PrintLine("5 Lista alla fordon");
                UI.PrintLine("6 Lista fordon efter typ");
                UI.PrintLine("0 Avsluta");
                UI.PrintLine("");
                menuChoice = UI.AskForUInt("Välj aktiviet (0 - 6)", 0, 6);

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
                    case 4:
                        SearchSpecial();
                        break;
                    case 5:
                        PrintParkedVehicles();
                        break;
                    case 6:
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
                uint menuChoice = AskCreateVehicleType(0, 5);

                PrintPageHeader();
                PrintSubHeader($"Registerar fordonet");

                if (menuChoice > 0)
                {
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
                    UI.PrintLine($"{Environment.NewLine}Avbryter...");
            }
            else
                UI.PrintLine("Garaget är tyvärr fullt - kan inte parkera flera fordon!");
        }


        private void ParkCar(string regNbr, string color, uint nbrWheels)
        {
            string message;
            string fuleType = UI.AskForString("Drivmedel");
            uint cylinderVolume = UI.AskForUInt("Cylindervolym");

            if (_handler.ParkCar(regNbr, color, nbrWheels, fuleType, cylinderVolume))
                message = "Bilen är nu parkerad";
            else
                message = "Kunde inte parkera bilen - okänt fel!";
            UI.PrintLine($"{Environment.NewLine}{message}");
        }

        private void ParkBus(string regNbr, string color, uint nbrWheels)
        {
            string message;
            uint length = UI.AskForUInt("Längd");
            uint cylinderVolume = UI.AskForUInt("Antal säten");

            if (Handler.ParkBoat(regNbr, color, nbrWheels, length, cylinderVolume))
                message = "Bussen är nu parkerad";
            else
                message = "Kunde inte parkera bussen - okänt fel!";
            UI.PrintLine($"{Environment.NewLine}{message}");
        }

        private void ParkBoat(string regNbr, string color, uint nbrWheels)
        {
            string message;
            uint lenghtInFoot = UI.AskForUInt("Längd i fot");
            uint numberOfEngines = UI.AskForUInt("Antal motorer", 0);

            if (Handler.ParkBus(regNbr, color, nbrWheels, lenghtInFoot, numberOfEngines))
                message = "Båten är nu parkerad";
            else
                message = "Kunde inte parkera båten - okänt fel!";
            UI.PrintLine($"{Environment.NewLine}{message}");
        }

        private void ParkAirplane(string regNbr, string color, uint nbrWheels)
        {
            string message;
            uint nbrSeats = UI.AskForUInt("Antal säten");
            uint nbrEngines = UI.AskForUInt("Antal motorer", 0);

            if (Handler.ParkAirplane(regNbr, color, nbrWheels, nbrSeats, nbrEngines))
                message = "Flygplanet är nu parkerat";
            else
                message = "Kunde inte parkera flygplanet - okänt fel!";
            UI.PrintLine($"{Environment.NewLine}{message}");
        }

        private void ParkMotorcycle(string regNbr, string color, uint nbrWheels)
        {
            string message;
            uint nbrSeats = UI.AskForUInt("Antal säten");
            uint cylinderVolume = UI.AskForUInt("Cylindervolym");

            if (Handler.ParkMotorcycle(regNbr, color, nbrWheels, nbrSeats, cylinderVolume))
                message = "Motorcykeln är nu parkerad";
            else
                message = "Kunde inte parkera motorcykeln - okänt fel!";
            UI.PrintLine($"{Environment.NewLine}{message}");
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
            string message;
            PrintPageHeader();
            PrintSubHeader("Checka ut fordon");

            if (_garage.Count > 0)
            {
                string regNbr = UI.AskForString("Ange reg.nummer");

                Vehicle? vehicle = _handler.SearchByRegNbr(regNbr);
                if (vehicle != null)
                {
                    if (Handler.UnparkVehicle(vehicle))
                        message = $"Fordonet '{vehicle.RegNbr}' är nu utcheckat";
                    else
                        message = "Fel - kunde inte checka ut fordonet!?";
                }
                else
                    message = $"Hittade tyvärr inget fordon med reg.nummer: '{regNbr}'";
            }
            else
                message = "Garaget är tomt!";

            UI.PrintLine($"{Environment.NewLine}{message}");
        }

        private void SearchVehicleByRegNbr()
        {
            PrintPageHeader();
            PrintSubHeader("Sök på reg.nummer");

            string regNbr = UI.AskForString("Ange fordonets reg.nummer");

            // Search parked vehicle by reg.nbr and show details
            Vehicle? vehicle = Handler.SearchByRegNbr(regNbr);
            if (vehicle != null)
            {
                UI.PrintLine($"{Environment.NewLine}Fordonsuppgifter:{Environment.NewLine}");
                UI.PrintLine(vehicle.ToString());
            }
            else
                UI.PrintLine($"{Environment.NewLine}Hittade inget fordon med reg.nummer: {regNbr}");

        }

        private void SearchSpecial()
        {
            PrintPageHeader();
            PrintSubHeader("Sök fordon med filter");
            UI.PrintLine($"Tom rad om ingen sökkriterie{Environment.NewLine}");

            // Ask for search criteria
            string? vehicleType = UI.AskForString("Fordonstyp", null, true).Trim();
            string? color = UI.AskForString("Färg", null, true).Trim();
            string wheels = UI.AskForString("Antal hjul", null, true);

            // convert vehicle type to english
            string type = "";

            if (vehicleType != null)
            {
                switch (vehicleType.ToUpper())
                {
                    case "BIL": type = "Car";  break;
                    case "BUSS": type = "Bus"; break;
                    case "BÅT": type = "Boat"; break;
                    case "FLYGPLAN": type = "Airplane"; break;
                    case "MOTORCYKEL": type = "Motorcycle"; break;
                    default: type = ""; break;
                }
            }

            int nbrWheels = -1;
            if (int.TryParse(wheels, out int nbr))
            {
                if (nbr >= 0)
                    nbrWheels = nbr;
            }

            // Perform search by criterias
            List<Vehicle> result = _handler.SearchWithFilter(type, color, nbrWheels);

            // Print search results
            UI.PrintLine($"{Environment.NewLine}Söker fordon med filter:");
            UI.PrintLine($"-----------------------{Environment.NewLine}");
            if (result.Count > 0)
            {
                foreach (var item in result)
                {
                    Console.WriteLine($"{item.ToString()}");
                }
            }
            else
                UI.PrintLine("Hittade inget fordon!");
        }


        private string CreateOccupancyText()
        {
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




        private uint AskCreateVehicleType(uint min, uint max)
        {
            UI.PrintLine($"Ange fordonstyp:{Environment.NewLine}");
            UI.PrintLine("1 Bil");
            UI.PrintLine("2 Buss");
            UI.PrintLine("3 Båt");
            UI.PrintLine("4 Flygplan");
            UI.PrintLine("5 Motorcykel");
            UI.PrintLine($"0 Avbryt{Environment.NewLine}");

            return UI.AskForUInt($"Välj aktiviet ({min} - {max})", min, max);
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
