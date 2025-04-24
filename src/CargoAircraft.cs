using System;

namespace PracticalWorkI
{
    public class CargoAircraft : Aircraft
    {
        private double maxLoad;

        public CargoAircraft(string id, AircraftStatus status, int distance, int speed, double fuelCapacity, double fuelConsume, double actualFuel, double maxLoad)
            : base(id, status, distance, speed, fuelCapacity, fuelConsume, actualFuel)
        {
            this.maxLoad = maxLoad;
        }

        public double GetMaxLoad() => maxLoad;

        public override string ToString()
        {
            raturn base.ToString() + $", Max Load: {maxLoad}kg";
        }
    }
}