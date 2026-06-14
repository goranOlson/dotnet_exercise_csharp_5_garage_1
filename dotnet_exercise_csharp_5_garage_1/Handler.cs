using dotnet_exercise_csharp_5_garage_1.Classes;
using dotnet_exercise_csharp_5_garage_1.UI;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace dotnet_exercise_csharp_5_garage_1
{
    internal class Handler
    {
        private ConsoleUI _ui;
        private Garage<Vehicle> _garage;

        public uint Capacity => _garage.Capacity;
        public uint Count => _garage.Count;
        public bool IsFull => _garage.IsFull;
        private ConsoleUI UI => _ui;


        public Handler(ConsoleUI ui, Garage<Vehicle> garage)
        {
            _garage = garage;
            _ui = ui;
        }


        public void ParkVehicle()
        {
            string message;  // = "";

            if (!_garage.IsFull)
            {
                Vehicle? vehicle = CreateVehicle();
                if (vehicle != null && !_garage.ParkCar(vehicle!))
                    message = "Kunde inte parkera fordonet - okänt fel!";
                else
                    message = "Fordonet är nu parkerat!";
            }
            else
                message = "Kan inte parkera fordonet - garaget är tyvärr fullt";

            UI.PrintLine($"{Environment.NewLine}{message}");
        }
        public void PrintParked()
        {
            if (_garage.Count > 0)
            {
                foreach (var veichle in _garage)
                {
                    UI.PrintLine(veichle.ToString());
                }
            }
            else
                UI.PrintLine("Garaget är tomt!");
        }
        public void PrintByType()
        {
            if (_garage.Count > 0)
            {
                Dictionary<string, uint> types = _garage.ListVehicleTypes();

                if (types.Count > 0) {
                    foreach (var vehicle in types)
                    {
                        UI.PrintLine($"{vehicle.Key}: {vehicle.Value} st");
                    }
                }
            }
            else
                UI.PrintLine("Garaget är tomt!");
        }


        public void RemoveVehicle()
        {
            string message;
            string regNbr = UI.AskForString("Ange reg.nummer");
            Vehicle? vehicle = _garage.GetVehicleByRegNbr(regNbr);
            
            if (vehicle != null)
            {
                if (_garage.UnparkVehicle(vehicle))
                    message = "Fordonet är nu utcheckat!";
                else
                    message = "Fel - kunde inte checka ut fordonet!?";
            }
            else
                message = "Hittade tyvärr inget fordon med reg.nummer: " + regNbr;

            UI.PrintLine(message);
        }
        public void SearchVehicleByRegNbr()
        {
            string regNbr = UI.AskForString("Ange fordonets reg.nummer");

            Vehicle? vehicle = _garage.GetVehicleByRegNbr(regNbr);

            if (vehicle != null)
            {
                UI.PrintLine($"{Environment.NewLine}Fordonsuppgifter:{Environment.NewLine}");
                UI.PrintLine(vehicle.ToString());
            }
            else
                UI.PrintLine($"{Environment.NewLine}Hittade inget fordon med reg.nummer: {regNbr}");
        }



        private Vehicle? CreateVehicle()
        {
            Vehicle? vehicle = null;

            UI.PrintLine("Ange fordonstyp:");
            UI.PrintLine("");
            UI.PrintLine("1 Bil");
            UI.PrintLine("2 Buss");
            UI.PrintLine("3 Båt");
            UI.PrintLine("4 Flygplan");
            UI.PrintLine("5 Motorcykel");
            UI.PrintLine($"0 Avbryt{Environment.NewLine}");

            uint menuChoice = UI.AskForUInt("Välj aktiviet (0 - 5)", 0, 5);  // Min/Max

            // PrintPageHeader();

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

        
        private (string regNbr, string color, uint wheels) AskBaseVehicleData()
        {
            UI.PrintLine($"{Environment.NewLine}Ange fordonsuppgifer:{Environment.NewLine}");

            string regNbr = UI.AskForString("Reg.nummer");
            string color = UI.AskForString("Färg");
            uint wheels = UI.AskForUInt("Antal hjul", 0);

            return (regNbr, color, wheels);
        }
    }
}
