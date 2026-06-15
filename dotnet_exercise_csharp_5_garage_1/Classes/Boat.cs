namespace dotnet_exercise_csharp_5_garage_1.Classes
{
    internal class Boat : Vehicle
    {
        private uint _length;
        private uint _engines;

        public uint Length => _length;
        public uint Engines => _engines;

        public Boat(string regNbr, string color, uint nbrWheels, uint lenghtInFoot, uint numberOfEngines)
            : base(regNbr, color, nbrWheels)
        {
            _length = lenghtInFoot;
            _engines = numberOfEngines;
        }

        public override string ToString()
        {
            return $"Båt: {base.ToString()}, {Length} fot, {Engines} motorer";
        }
    }

}
