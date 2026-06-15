using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_exercise_csharp_5_garage_1.Classes
{
    internal class Motorcycle : Vehicle
    {
        private uint _cylinderVolume;
        private uint _seats;

        public uint CylinderVolume => _cylinderVolume;
        public uint Seats => _seats;

        public Motorcycle(string regNbr, string color, uint wheels, uint nbrOfSeats, uint cylinderVolume)
            : base(regNbr, color, wheels)
        {
            _cylinderVolume = cylinderVolume;
            _seats = nbrOfSeats;
        }

        public override string ToString()
        {
            return $"Motorcykel: {base.ToString()}, {Seats} säten, {CylinderVolume} cc";

        }
    }
}
