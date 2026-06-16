namespace dotnet_exercise_csharp_5_garage_1.Classes
{
    public class Car : Vehicle
    {
        private uint _cylinderVolume;
        private string _fuleType;

        public uint CylinderVolume => _cylinderVolume;
        public string FuleType => _fuleType;

        public Car(string regNbr, string color, uint wheels, string fuleType, uint cylinderVolume)
            : base(regNbr, color, wheels)
        {
            _fuleType = fuleType;
            _cylinderVolume = cylinderVolume;
        }

        public override string ToString()
        {
            return $"Bil: {base.ToString()}, {FuleType}, {CylinderVolume} cc";
        }
    }
}
