using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;

namespace dotnet_exercise_csharp_5_garage_1.Classes
{
    internal class Garage
    {
        private Vehicle[] _parking = new Vehicle[10];

        private int _capacity;  // Available parkings
        private int _count;  // Occupied seats

        public int Capacity => _capacity;
        public int Count => _count;

        public bool IsFull => _count >= _capacity;

        public Garage(int capacity)
        {
            _capacity = capacity;
            Console.WriteLine($"Garage({_capacity})");
        }

        public bool ParkCar(Vehicle vehicle)
        {
            bool success = false;

            if (!IsFull)
            {
                int freePos = GetEmptyParking();
                if (freePos >= 0)
                {
                    Console.WriteLine($"freePos: {freePos}");
                    _parking[freePos] = vehicle;
                    _count++;
                    success = true;
                }
                else
                {
                    Console.WriteLine("No free space");
                }
            }

            return success;
        }

        public bool UnparkCar(Vehicle vehicle)
        {
            bool success = false;

            for (int i = 0; i < _parking.Length; i++)
            {
                if (_parking[i] != null)
                {
                    // Console.WriteLine($" # {i} - {_parking[i].RegNbr}");
                    if (_parking[i].RegNbr.ToUpper() == vehicle.RegNbr.ToUpper())
                    {
                         Console.WriteLine("Unparking RegNbr.: " + vehicle.RegNbr);

                        // ToDo - null, 
                        _parking[i] = null;  
                        _count--;
                        success = true;
                    }
                }
            }

            return success;
        }
        

        public void PrintParkedVehicles()
        {
            Console.WriteLine($"{Environment.NewLine}___ Parked vehicles ___ ({Count}/{Capacity})");
            if (_count > 0)
            {
                for (int i = 0; i < _parking.Length; i++)
                {
                    if (_parking[i] != null)
                    {
                        Console.WriteLine("- " + _parking[i].ToString());
                    }
                }
            }
            else
            {
                //Console.WriteLine($"{Environment.NewLine}Garaget är tomt!");
                Console.WriteLine($"Garaget är tomt!");
            }
            
            Console.WriteLine();
        }

        private int GetEmptyParking()
        {
            int n = -1;

            // int numIndex = Array.IndexOf(numbers, numToRemove);
            for (int i = 0; i < _parking.Length; i++)
            {
                if (_parking[i] is null)
                {
                    n = i;
                    break;
                }
            }


            return n;
        }
    }
}
