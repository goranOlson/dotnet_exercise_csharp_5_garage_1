namespace dotnet_exercise_csharp_5_garage_1.Classes
{
    internal abstract class Vehicle
    {
        public string Color { get; }
        public string RegNbr { get; }
        public uint NbrOfWheels { get; }

        protected Vehicle(string regNbr, string color, uint wheels)
        {
            RegNbr = regNbr;
            Color = color;
            NbrOfWheels = wheels;
        }

        public override string ToString()
        {
            return $"{RegNbr}, {Color}, {NbrOfWheels} hjul";
        }
    }
}
