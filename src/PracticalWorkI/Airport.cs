using PracticalWorkI;
using System;
using System.IO;


namespace PracticalWork1
{
    public abstract class Airport
    {
        private Runway[][] Runways {get; set;}

        private List<Aircraft> Aircrafts {get; set;}

        public Airport(int runwayRows, int runwayColumns)
        {
            Runways = new Runways[runwayRows, runwayColumns];
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
        Console.WriteLine("Runway Status:");
        for(int r = 0; r < Runways.GetLength(0); r++)
        {
            for(int c = 0; c < Runways.GetLength(1); c++)
            {
                Runways[r][c].ToString()
            }
        } 
        Console.WriteLine("Airctaft Status:");
        foreach (var Airctaft in Aircrafts)
        {
            Console.WriteLine($"{Aircraft.ToString()}");
        }
    }

    public void AdvanceTick()
    {
        int currentTick = 1;

        while(currentTick < 150)
        {
            currentTick++;
            int simulationTime = currentTick * 15;

            foreach(var Aircraft in Aircrafts)
            {
                
                if(Aircraft.GetStatus(AircraftStatus.Flight))
                {
                    Aircraft.SetDistance((simulationTime/4) / Aircraft.GetSpeed());
                    Aircraft.SetActualFuel(Aircraft.fuelCapacity - (Aircraft.fuelConsume / Aircraft.GetDistance()));
                    if(Aircraft.GetDistance == 0)
                    {
                        Aircraft.SetStatus(AircraftStatus.Waiting);
                    }
                } else if(Aircraft.GetStatus(AircraftStatus.Waiting)){
                    
                }
            } 
        
            foreach(var Runway in Runways)
            {
                if(Runway.GetStatus(RunwayStatus.Occupied) && Runway.GetTickAvailability() > 0){
                    Runway.DecreaseTickAvailability();
                } else if(Runway.GetTickAvailability() == 0){
                    Runway.SetStatus(RunwayStatus.Free);
                }
            }
        }
    }

    public void LoadAircraftFromFile(string filePath)
    {
        if (File.Exists(filePath) == false)
        {
            Console.WriteLine("The file provided does not exist, please provide another one.");
            return;
        }

        string separator = ",";

        StreamReader sl = File.OpenText(filePath);
        
        string line = sl.ReadLine();

        
        while((line = sl.ReadLine()) != null)
        {
            string[] values = line.Split(separator);

            if(values.Length == 8)
            {
                for(int i = 0; i < values.Length; i++)
                {
                    // We go through each line of the file and set the parameters for each aircraft
                    // We use Trim to eliminate the spaces at the start or end of the line
                    string id = values[0].Trim();
                    AircraftStatus status = Enum.Parse<AircraftStatus>(values[1].Trim());
                    int distance = int.Parse(values[2].Trim());
                    int speed = int.Parse(values[3].Trim());
                    string type = values[4].Trim();
                    double fuelCapacity = double.Parse(values[5].Trim());
                    double fuelConsume = double.Parse(values[6].Trim());
                    string extraData = values[7].Trim();
                    double actualFuel = fuelCapacity;

                    Aircraft newAircraft;
                    if(type == "Commercial" || type == "commercial")
                    {
                        int passengers = int.Parse(values[8].Trim());
                        newAircraft = new CommercialAircraft(id, status, distance, speed, fuelCapacity, fuelConsume, actualFuel, passengers);
                    } else if(type == "Cargo" || type == "cargo"){
                        double maxLoad = double.Parse(values[8].Trim());
                        newAircraft = new CargoAircraft(id, status, distance, speed, fuelCapacity, fuelConsume, actualFuel, maxLoad);
                    } else if(type == "Private" || type == "private"){
                        string owner = values[8].Trim();
                        newAircraft = new PrivateAircraft(id, status, distance, speed, fuelCapacity, fuelConsume, actualFuel, owner);
                    }
                }
            }

        }
        sl.Close();
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
                break;
            case "2":
                Console.Write("Enter Maximum Load (kg): ");
                double maxLoad = double.Parse(Console.ReadLine());
                newAircraft = new CargoAircraft(id, status, distance, speed, fuelCapacity, fuelConsume, actualFuel, maxLoad);
                break;
            case "3":
                Console.Write("Enter Owner's Name: ");
                string owner = Console.ReadLine();
                newAircraft = new PrivateAircraft(id, status, distance, speed, fuelCapacity, fuelConsume, actualFuel, owner);
                break;
            default:
                Console.WriteLine("Invalid choice, select one of the options.");
                return;
        }

        Aircrafts.Add(newAircraft);
        Console.WriteLine($"Aircraft {newAircraft.GetID()} was added\n");
    }

    }
}