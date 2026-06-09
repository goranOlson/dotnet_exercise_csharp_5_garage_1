using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_exercise_csharp_5_garage_1.Classes
{
    internal class Bus : Vehicle
    {
        private double _length;
        private uint _seats;

        public double Length => _length;
        public uint Seats => _seats;

        public Bus(string regNbr, string color, uint wheels, double length, uint nbrSeats) 
            : base(regNbr, color, wheels)
        {
            _length = length;
            _seats = nbrSeats;
        }


        public override string ToString()
        {
            return $"Typ: Bus, " + base.ToString() + $", längd: {Length}, antal säten: {Seats}";
        }
    }
}
