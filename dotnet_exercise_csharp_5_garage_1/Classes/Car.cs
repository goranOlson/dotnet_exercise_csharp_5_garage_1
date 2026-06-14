using System.Runtime.CompilerServices;

namespace dotnet_exercise_csharp_5_garage_1.Classes
{
    internal class Car : Vehicle
    {
        private double _cylinderVolume;
        private string _fuleType;

        public double CylinderVolume => _cylinderVolume;
        public string FuleType => _fuleType;

        public Car(string regNbr, string color, uint wheels, string fuleType, double cylinderVolume)
            : base(regNbr, color, wheels)
        {
            _fuleType = fuleType;
            _cylinderVolume = cylinderVolume;
        }

        public override string ToString()
        {
            // return $" Car, " + base.ToString() + $", bränsle: {FuleType}, cylindervolym: {CylinderVolume}";
            return base.ToString() + $" Car, bränsle: {FuleType}, cylindervolym: {CylinderVolume}";
        }
    }
}
