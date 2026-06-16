namespace dotnet_exercise_csharp_5_garage_1.Classes
{
    public class Bus : Vehicle
    {
        private uint _length;
        private uint _seats;

        public uint Length => _length;
        public uint Seats => _seats;

        public Bus(string regNbr, string color, uint wheels, uint length, uint nbrSeats) 
            : base(regNbr, color, wheels)
        {
            _length = length;
            _seats = nbrSeats;
        }


        public override string ToString()
        {
            return $"Buss: {base.ToString()}, {Length} m, {Seats} säten";
        }
    }
}
