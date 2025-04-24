using System;

namespace PracticalWorkI
{
    public abstract class Aircraft
    {
        private AircraftStatus status;
        private string id;
        private int distance, speed;
        private double fuelCapacity, fuelConsume, actualFuel;

        public Aircraft(string id, AircraftStatus status, int distance, int speed, double fuelCapacity, double fuelConsume, double actualFuel)
        {
            this.id = id;
            this.status = status;
            this.distance = distance;
            this.speed = speed;
            this.fuelCapacity = fuelCapacity;
            this.fuelConsume = fuelConsume;
            this.actualFuel = actualFuel;
        }
    
        public string GetID() => id;
        public AircraftStatus GetStatus() => status;
        public int GetDistance() => distance;
        public int GetSpeed() => speed;
        public double GetFuelCapacity() => fuelCapacity;
        public double GetFuelConsume() => fuelConsume;
        public double GetActualFuel() => actualFuel;
        public void SetStatus(AircraftStatus status) { this.status = status; }
        public void SetDistance(int distance) { this.distance = distance; }
        public void SetActualFuel(double actualFuel) { this.actualFuel = actualFuel; }

        public override string ToString()
        {
            return $"ID: {id}, Status: {status}, Distance: {distance}km, Speed: {speed}km/h, Fuel: {actualFuel}/{fuelCapacity}L";
        }
    }
}