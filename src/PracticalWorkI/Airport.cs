using Microsoft.VisualBasic;
using PracticalWorkI;
using System;
using System.IO;


namespace PracticalWorkI
{
    public class Airport
    {
        private Runway[,] Runways {get; set;}

        private List<Aircraft> Aircrafts {get; set;}

        public Airport(int runwayRows, int runwayColumns)
        {
            Runways = new Runway[runwayRows, runwayColumns];
            Aircrafts = new List<Aircraft>();

            int id = 1;
            for (int r = 0; r < runwayRows; r++)
            {
                for (int c = 0; c < runwayColumns; c++)
                {
                    Runways[r,c] = new Runway($"Runway{id++}");
                }
            }
        }

    public void ShowStatus()
    {
        Console.WriteLine("Runway Status: \n");
        for(int r = 0; r < Runways.GetLength(0); r++)
        {
            for(int c = 0; c < Runways.GetLength(1); c++)
            {
                Runway runway = Runways[r,c];
                    if (runway.GetStatus() == RunwayStatus.Free)
                    {
                        Console.WriteLine($"{Runways[r,c].GetID()} is free\n");
                    } else {
                        Console.WriteLine($"{Runways[r,c].GetID()} is occupied by {Runways[r,c].GetCurrentAircraft()}\n");
                    }
            }
        } 

        Console.WriteLine("Airctaft Status: \n");
        foreach (var aircraft in Aircrafts)
        {
            Console.WriteLine($"{aircraft.ToString()}");
        }
    }

    
    public void AdvanceTick()
{
    // Avanza 1 tick (15 minutos)
    foreach (var aircraft in Aircrafts)
    {
        if (aircraft.GetStatus() == AircraftStatus.InFlight)
        {
            int distanceToAdvance = aircraft.GetSpeed() / 4;
            int currentDistance = aircraft.GetDistance();
            int newDistance = Math.Max(0, currentDistance - distanceToAdvance);

            double fuelUsed = distanceToAdvance * aircraft.GetFuelConsume();
            double newFuel = Math.Max(0, aircraft.GetActualFuel() - fuelUsed);

            aircraft.SetDistance(newDistance);
            aircraft.SetActualFuel(newFuel);

            if (newDistance == 0)
            {
                aircraft.SetStatus(AircraftStatus.Waiting);
            }
        }
    }

    for (int r = 0; r < Runways.GetLength(0); r++)
    {
        for (int c = 0; c < Runways.GetLength(1); c++)
        {
            var runway = Runways[r, c];
            if (runway.GetStatus() == RunwayStatus.Ocupated)
            {
                runway.DecreaseTicksAvailability();
            }
        }
    }

    ShowStatus();
}

    

    public void LoadAircraftFromFile()
    {
        Console.WriteLine("Please specify the path of the file: ");
        string filePath = Console.ReadLine();

        while (!File.Exists(filePath))
        {
            Console.WriteLine("The file provided does not exist, please provide another one:");
            filePath = Console.ReadLine();
        }

        string separator = ",";

        StreamReader sl = File.OpenText(filePath);
        
        string line;
        
        while((line = sl.ReadLine()) != null)
        {
            string[] values = line.Split(separator);

            if(values.Length == 8)
            {
                
                // We go through each line of the file and set the parameters for each aircraft
                string id = values[0];
                AircraftStatus status = Enum.Parse<AircraftStatus>(values[1]);
                int distance = int.Parse(values[2]);
                int speed = int.Parse(values[3]);
                string type = values[4];
                double fuelCapacity = double.Parse(values[5]);
                double fuelConsume = double.Parse(values[6]);
                string extraData = values[7];
                double actualFuel = fuelCapacity;

                Aircraft newAircraft;
                if(type == "Commercial" || type == "commercial")
                {
                    int passengers = int.Parse(values[7]);
                    newAircraft = new CommercialAircraft(id, status, distance, speed, fuelCapacity, fuelConsume, actualFuel, passengers);
                    Aircrafts.Add(newAircraft);
                    Console.WriteLine($"Commercial Aircraft {newAircraft.GetID()} was added\n");
                } else if(type == "Cargo" || type == "cargo"){
                    double maxLoad = double.Parse(values[7]);
                    newAircraft = new CargoAircraft(id, status, distance, speed, fuelCapacity, fuelConsume, actualFuel, maxLoad);
                    Aircrafts.Add(newAircraft);
                    Console.WriteLine($"Cargo Aircraft {newAircraft.GetID()} was added\n");
                } else if(type == "Private" || type == "private"){
                    string owner = values[7];
                    newAircraft = new PrivateAircraft(id, status, distance, speed, fuelCapacity, fuelConsume, actualFuel, owner);
                    Aircrafts.Add(newAircraft);
                    Console.WriteLine($"Private Aircraft {newAircraft.GetID()} was added\n");
                }

            }
        }
        sl.Close();
        return;
    }
        

     
    public void AddAircraft()
    {
        Console.WriteLine("\n Add a new Aircraft");

        Console.WriteLine("Select the type of Aircraft:");
        Console.WriteLine("1. Commercial Aircraft");
        Console.WriteLine("2. Cargo Aircraft");
        Console.WriteLine("3. Private Aircraft");

        string choice = Console.ReadLine();
        Aircraft newAircraft;

        Console.Write("Enter Aircraft ID: ");
        string id = Console.ReadLine();

        Console.Write("Enter Distance to Airport (km): ");
        int distance = int.Parse(Console.ReadLine());

        Console.Write("Enter Speed (km/h): ");
        int speed = int.Parse(Console.ReadLine());

        Console.Write("Enter Fuel Capacity (liters): ");
        double fuelCapacity = double.Parse(Console.ReadLine());

        Console.Write("Enter Fuel Consume (liters/km): ");
        double fuelConsume = double.Parse(Console.ReadLine());

        double actualFuel = fuelCapacity;
        AircraftStatus status = (distance > 0) ? AircraftStatus.InFlight : AircraftStatus.Waiting;

        switch (choice)
        {
            case "1":
                Console.Write("Enter Number of Passengers: ");
                int passengers = int.Parse(Console.ReadLine());
                newAircraft = new CommercialAircraft(id, status, distance, speed, fuelCapacity, fuelConsume, actualFuel, passengers);
                Aircrafts.Add(newAircraft);
                Console.WriteLine($"Aircraft {newAircraft.GetID()} was added");
                return;
            case "2":
                Console.Write("Enter Maximum Load (kg): ");
                double maxLoad = double.Parse(Console.ReadLine());
                newAircraft = new CargoAircraft(id, status, distance, speed, fuelCapacity, fuelConsume, actualFuel, maxLoad);
                Aircrafts.Add(newAircraft);
                Console.WriteLine($"Aircraft {newAircraft.GetID()} was added");
                return;
            case "3":
                Console.Write("Enter Owner's Name: ");
                string owner = Console.ReadLine();
                newAircraft = new PrivateAircraft(id, status, distance, speed, fuelCapacity, fuelConsume, actualFuel, owner);
                Aircrafts.Add(newAircraft);
                Console.WriteLine($"Aircraft {newAircraft.GetID()} was added");
                return;
            default:
                Console.WriteLine("Invalid choice, select one of the options.");
                return;
        }

    }


    public void RunSIM()
    {
            for(int i = 0; i < 150; i++)
            {
                this.ShowStatus();
                this.AdvanceTick();
                Thread.Sleep(3000);
            }
    }
    }
}