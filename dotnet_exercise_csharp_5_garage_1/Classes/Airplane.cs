namespace dotnet_exercise_csharp_5_garage_1.Classes
{
    internal class Airplane : Vehicle
    {
        private uint _seats;
        private uint _engines;

        public uint Engines => _engines;
        public uint Seats => _seats;

        public Airplane(string regNbr, string color, uint wheels, uint nbrSeats, uint nbrEngines) 
            : base(regNbr, color, wheels)
        {
            _seats = nbrSeats;
            _engines = nbrEngines;
        }

        public override string ToString()
        {
            return $"Typ: Airplane, " + base.ToString() + $", antal motorer: {Engines}, antal säten: {Seats}";
        }
    }
}
