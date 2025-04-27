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
    
        public string GetID(){return this.id;}
        public AircraftStatus GetStatus(){ return this.status; }
        public int GetDistance(){ return this.distance; }
        public int GetSpeed() {return this.speed; }
        public double GetFuelCapacity() {return fuelCapacity; }
        public double GetFuelConsume() {return fuelConsume; }
        public double GetActualFuel() {return actualFuel; }
        public void SetStatus(AircraftStatus status) { this.status = status; }
        public void SetDistance(int distance) { this.distance = distance; }
        public void SetActualFuel(double actualFuel) { this.actualFuel = actualFuel; }

        public override string ToString()
        {
            return $"ID: {this.id}, Status: {this.status}, Distance: {this.distance}km, Speed: {this.speed}km/h, Fuel: {this.actualFuel}/{this.fuelCapacity}L";
        }
    }
}