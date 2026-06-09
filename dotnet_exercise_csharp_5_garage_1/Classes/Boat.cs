namespace dotnet_exercise_csharp_5_garage_1.Classes
{
    internal class Boat : Vehicle
    {
        private double _length;
        private uint _engines;

        public double Length => _length;
        public uint Engines => _engines;

        public Boat(string regNbr, string color, uint nbrWheels, double lenghtInFoot, uint numberOfEngines)
            : base(regNbr, color, nbrWheels)
        {
            _length = lenghtInFoot;
            _engines = numberOfEngines;
        }

        public override string ToString()
        {
            return $"Typ: Boat, " + base.ToString() + $", längd: {Length} fot, antal motorer: {Engines}";
        }
    }

}
