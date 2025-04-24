using System;

namespace PracticalWorkI
{
    public class CommercialAircraft : Aircraft
    {
        private int passengers; //Esta es la variable caracteristica de este tipo de avion

        public CommercialAircraft(string id, AircraftStatus status, int distance, int speed, double fuelCapacity, double fuelConsume, double actualFuel, int passengers)
            : base(id, status, distance, speed, fuelCapacity, fuelConsume, actualFuel)
        {
            this.passengers = passengers;
        }
        public int GetPassengers() => passengers;

        public override string ToString()
        {
            return base.ToString() + $", Passengers: {passengers}";
        }
    }
}