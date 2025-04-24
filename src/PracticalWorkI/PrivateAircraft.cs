using System;

namespace PracticalWorkI
{
    public class PrivateAircraft : Aircraft
    {
        private string owner;

        public PrivateAircraft(string id, AircraftStatus status, int distance, int speed, double fuelCapacity, double fuelConsume, double actualFuel, string owner)
            : base(id, status, distance, speed, fuelCapacity, fuelConsume, actualFuel)
        {
            this.owner = owner;
        }

        public string GetOwner() => owner;

        public override string ToString()
        {
            return base.ToString() + $", Owner: {owner}";
        }
    }
}