using dotnet_exercise_csharp_5_garage_1.Classes;
using dotnet_exercise_csharp_5_garage_1.Interfaces;

namespace dotnet_exercise_csharp_5_garage_1
{
    internal class Manager
    {
        const string messageGarageEmpty = "Garaget är tomt!";

        private readonly IUI _ui;
        private readonly IGarage<Vehicle> _garage;
        private readonly IHandler _handler;

        public Manager(IUI ui)
        {
            _ui = ui;
            _garage = new Garage<Vehicle>(AskForGarageSize());
            _ui.PrintLine("");
            _handler = new Handler(_garage);

            if (AskForSeedingData() == true)
                SeedData(_garage);
        }        

        public void Run()
        {
            // Menu
            bool exit = false;
            uint menuChoice;

            do
            {
                PrintPageHeader();

                _ui.PrintLine("       Huvudmeny");
                _ui.PrintLine("-------------------------");
                _ui.PrintLine("1 Incheckning");
                _ui.PrintLine("2 Utcheckning");
                _ui.PrintLine("3 Sök på reg.nummer");
                _ui.PrintLine("4 Sök med filter");
                _ui.PrintLine("5 Lista alla fordon");
                _ui.PrintLine("6 Lista fordon efter typ");
                _ui.PrintLine("0 Avsluta");
                _ui.PrintLine("");
                menuChoice = _ui.AskForUInt("Välj aktiviet (0 - 6)", 0, 6);

                switch (menuChoice)
                {
                    case 1:
                        CheckInVehicle(); 
                        break;
                    case 2:
                        CheckOutVehicle();
                        break;
                    case 3:
                        SearchVehicleByRegNbr();
                        break;
                    case 4:
                        SearchVehicleByProperty();
                        break;
                    case 5:
                        PrintParkedVehicles();
                        break;
                    case 6:
                        PrintVehicleByType();
                        break;
                    case 0:
                        exit = true;
                        _ui.PrintLine($"{Environment.NewLine}Programmet avslutas...");
                        break;
                }
                _ui.ExitMessageAction("Tryck på valfri tangent för att forsätta!");
            } while (!exit);
        }
       
        private void CheckInVehicle()
        {
            PrintPageHeader();
            PrintSubHeader("Registrera fordon");

            if (!_handler.IsFull)
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
                            CheckInCar(commonData.regNbr, commonData.color, commonData.wheels);
                            break;
                        case 2:
                            CheckInBus(commonData.regNbr, commonData.color, commonData.wheels);
                            break;
                        case 3:
                            CheckInBoat(commonData.regNbr, commonData.color, commonData.wheels);
                            break;
                        case 4:
                            CheckInAirplane(commonData.regNbr, commonData.color, commonData.wheels);
                            break;
                        case 5:
                            CheckInMotorcycle(commonData.regNbr, commonData.color, commonData.wheels);
                            break;
                        default:
                            break;
                    }
                }
                else
                    _ui.PrintLine($"{Environment.NewLine}Avbryter...");
            }
            else
                _ui.PrintLine("Garaget är tyvärr fullt - kan inte parkera flera fordon!");
        }

        private void CheckInCar(string regNbr, string color, uint nbrWheels)
        {
            string message;
            string fuleType = _ui.AskForString("Drivmedel");
            uint cylinderVolume = _ui.AskForUInt("Cylindervolym");

            if (_handler.ParkCar(regNbr, color, nbrWheels, fuleType, cylinderVolume))
                message = "Bilen är nu parkerad";
            else
                message = "Kunde inte parkera bilen - okänt fel!";
            _ui.PrintLine($"{Environment.NewLine}{message}");
        }

        private void CheckInBus(string regNbr, string color, uint nbrWheels)
        {
            string message;
            uint length = _ui.AskForUInt("Längd");
            uint cylinderVolume = _ui.AskForUInt("Antal säten");

            if (_handler.ParkBoat(regNbr, color, nbrWheels, length, cylinderVolume))
                message = "Bussen är nu parkerad";
            else
                message = "Kunde inte parkera bussen - okänt fel!";
            _ui.PrintLine($"{Environment.NewLine}{message}");
        }

        private void CheckInBoat(string regNbr, string color, uint nbrWheels)
        {
            string message;
            uint lenghtInFoot = _ui.AskForUInt("Längd i fot");
            uint numberOfEngines = _ui.AskForUInt("Antal motorer", 0);

            if (_handler.ParkBus(regNbr, color, nbrWheels, lenghtInFoot, numberOfEngines))
                message = "Båten är nu parkerad";
            else
                message = "Kunde inte parkera båten - okänt fel!";
            _ui.PrintLine($"{Environment.NewLine}{message}");
        }

        private void CheckInAirplane(string regNbr, string color, uint nbrWheels)
        {
            string message;
            uint nbrSeats = _ui.AskForUInt("Antal säten");
            uint nbrEngines = _ui.AskForUInt("Antal motorer", 0);

            if (_handler.ParkAirplane(regNbr, color, nbrWheels, nbrSeats, nbrEngines))
                message = "Flygplanet är nu parkerat";
            else
                message = "Kunde inte parkera flygplanet - okänt fel!";
            _ui.PrintLine($"{Environment.NewLine}{message}");
        }

        private void CheckInMotorcycle(string regNbr, string color, uint nbrWheels)
        {
            string message;
            uint nbrSeats = _ui.AskForUInt("Antal säten");
            uint cylinderVolume = _ui.AskForUInt("Cylindervolym");

            if (_handler.ParkMotorcycle(regNbr, color, nbrWheels, nbrSeats, cylinderVolume))
                message = "Motorcykeln är nu parkerad";
            else
                message = "Kunde inte parkera motorcykeln - okänt fel!";
            _ui.PrintLine($"{Environment.NewLine}{message}");
        }

        private (string regNbr, string color, uint wheels) AskBaseVehicleData()
        {
            _ui.PrintLine($"{Environment.NewLine}Ange fordonsuppgifer:{Environment.NewLine}");

            bool regNbrIsUnique = false;
            string regNbr;

            do {
                // Reg.number must be unique
                regNbr = _ui.AskForString("Reg.nummer");
                if (_handler.SearchByRegNbr(regNbr) == null)
                    regNbrIsUnique = true;
                else
                    _ui.PrintLine($"Fordonet är redan parkerat - testa ett annat reg.nummer!{Environment.NewLine}");
            } while (!regNbrIsUnique);

            string color = _ui.AskForString("Färg");
            uint wheels = _ui.AskForUInt("Antal hjul", 0);

            return (regNbr, color, wheels);
        }

        private void CheckOutVehicle()
        {
            string message;
            PrintPageHeader();
            PrintSubHeader("Checka ut fordon");

            if (_garage.Count > 0)
            {
                string regNbr = _ui.AskForString("Ange reg.nummer");

                Vehicle? vehicle = _handler.SearchByRegNbr(regNbr);
                if (vehicle != null)
                {
                    if (_handler.UnparkVehicle(vehicle))
                        message = $"Fordonet '{vehicle.RegNbr}' är nu utcheckat";
                    else
                        message = "Fel - kunde inte checka ut fordonet!?";
                }
                else
                    message = $"Hittade tyvärr inget fordon med reg.nummer: '{regNbr}'";
            }
            else
                message = messageGarageEmpty;

            _ui.PrintLine($"{Environment.NewLine}{message}");
        }

        private void SearchVehicleByRegNbr()
        {
            PrintPageHeader();
            PrintSubHeader("Sök på reg.nummer");

            if (_garage.Count > 0)
            {
                string regNbr = _ui.AskForString("Ange fordonets reg.nummer");

                // Search parked vehicle by reg.nbr and show details
                Vehicle? vehicle = _handler.SearchByRegNbr(regNbr);
                if (vehicle != null)
                {
                    _ui.PrintLine($"{Environment.NewLine}Fordonsuppgifter:{Environment.NewLine}");
                    _ui.PrintLine(vehicle.ToString());
                }
                else
                    _ui.PrintLine($"{Environment.NewLine}Hittade inget fordon med reg.nummer: {regNbr}");
            }
            else
                _ui.PrintLine(messageGarageEmpty);
        }

        private void SearchVehicleByProperty()
        {
            PrintPageHeader();
            PrintSubHeader("Sök fordon med filter");

            if (_garage.Count > 0)
            {
                // Ask for search criteria
                _ui.PrintLine($"Tom rad om ingen sökkriterie{Environment.NewLine}");

                string? vehicleType = _ui.AskForString("Fordonstyp", null, true).Trim();
                string? color = _ui.AskForString("Färg", null, true).Trim();
                string wheels = _ui.AskForString("Antal hjul", null, true);

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
                _ui.PrintLine($"{Environment.NewLine}Söker fordon med filter:");
                _ui.PrintLine($"-----------------------{Environment.NewLine}");
                if (result.Count > 0)
                {
                    foreach (var item in result)
                    {
                        Console.WriteLine($"{item.ToString()}");
                    }
                }
                else
                    _ui.PrintLine("Hittade inget fordon!");
            }
            else
                _ui.PrintLine(messageGarageEmpty);
        }

        private string CreateOccupancyText()
        {
            return $"{_handler.Capacity - _handler.Count} av {_handler.Capacity} platser lediga";
        }

        private void PrintParkedVehicles()
        {
            PrintPageHeader();
            PrintSubHeader($"Parkerade fordon ({_garage.Count} st)");

            // List parked vehicles
            if (_garage.Count > 0)
            {
                foreach (var item in _garage)
                {
                    _ui.PrintLine(item.ToString());
                }
            }
            else
                _ui.PrintLine(messageGarageEmpty);

        }
        
        private void PrintVehicleByType()
        {
            PrintPageHeader();
            PrintSubHeader($"Parkerade fordon utifrån typ ({CreateOccupancyText()})");

            // Get number of parked vehicles by typ and show result
            Dictionary<string, uint> listTypes = _handler.GetVehicleByType();
            if (listTypes.Count > 0)
            {
                foreach (var vehicle in listTypes)
                {
                    _ui.PrintLine($"{vehicle.Key}: {vehicle.Value} st");
                }
            }
            else
                _ui.PrintLine("Parkerade fordon saknas!");
        }
        
        private void PrintPageHeader()
        {
            _ui.Clear();
            // Print Header
            //_ui.PrintLine($"Välkommen till Garage 1.0          {_handler.Capacity - _handler.Count} av {_handler.Capacity} platser lediga");
            _ui.PrintLine($"Välkommen till Garage 1.0          {CreateOccupancyText()}");
            _ui.PrintLine($"========================================================={Environment.NewLine}");
        }
        
        private void PrintSubHeader(string header)
        {
            _ui.PrintLine($"_______ {header} _______{Environment.NewLine}");
        }


        private uint AskCreateVehicleType(uint min, uint max)
        {
            _ui.PrintLine($"Ange fordonstyp:{Environment.NewLine}");
            _ui.PrintLine("1 Bil");
            _ui.PrintLine("2 Buss");
            _ui.PrintLine("3 Båt");
            _ui.PrintLine("4 Flygplan");
            _ui.PrintLine("5 Motorcykel");
            _ui.PrintLine($"0 Avbryt{Environment.NewLine}");

            return _ui.AskForUInt($"Välj aktiviet ({min} - {max})", min, max);
        }

        private uint AskForGarageSize()
        {
            _ui.PrintLine($"Välkommen till Garage 1.0");
            _ui.PrintLine($"========================={Environment.NewLine}");

            return _ui.AskForUInt($"Ange antal platser i garaget", 1);
        }

        private bool AskForSeedingData()
        {
            string str = _ui.AskForString("Fyll garaget med upp till 6 fordon (j/n)", (input) =>
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

        private void SeedData(IGarage<Vehicle> garage)
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
