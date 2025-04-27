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
        Console.WriteLine("\n Runway Status: \n");
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

        Console.WriteLine("\nAirctaft Status:\n");
        foreach (var aircraft in Aircrafts)
        {
            Console.WriteLine($"{aircraft.ToString()}");
        }
    }

    
    public void AdvanceTick()
    {
        // We go though the AIrcraft list using a foreach
        foreach (var aircraft in Aircrafts)
        {
            if (aircraft.GetStatus() == AircraftStatus.InFlight)
            {
                int distanceToAdvance = aircraft.GetSpeed() / 4;
                int currentDistance = aircraft.GetDistance();
                int newDistance = Math.Max(0, currentDistance - distanceToAdvance);
                // We calculate the distance the aircraft advances in each tick and then the new distance it will have from the airport with the Math.MAx function
                
                double fuelUsed = distanceToAdvance * aircraft.GetFuelConsume();
                double newFuel = Math.Max(0, aircraft.GetActualFuel() - fuelUsed);
                // We do the same for the fuel used, but we use the distance to advance funtion to avoid repeating ourselves

                aircraft.SetDistance(newDistance);
                aircraft.SetActualFuel(newFuel);

                if (newDistance == 0)
                {
                    aircraft.SetStatus(AircraftStatus.Waiting);
                }
            }
        }

        foreach(var Runway in Runways)
            {
                // Now we go through the Runways using a foreach loop as it is easier than the two for loops
                if(Runway.GetStatus() == RunwayStatus.Free)
                {
                    foreach(var Aircraft in Aircrafts)
                    {
                        if(Aircraft.GetStatus() == AircraftStatus.Waiting)
                        {
                            Runway.ReserveRunmway(Aircraft);
                            Aircraft.SetStatus(AircraftStatus.Landing);
                            Runway.DecreaseTicksAvailability();
                            // If the Runway status is free, then we go through each aircraft as before, searching for one that is waiting, reserve the runway, and then changing the status of the aircraft accordingly.
                            Aircraft.SetStatus(AircraftStatus.OnGround);
                            //Then we decrease the tick availability of the eunway and set the aircraft state to Onground
                        }
                    }
                } else if(Runway.GetStatus() == RunwayStatus.Ocupated) {
                    Runway.DecreaseTicksAvailability();
                }
            }
    }

    

    public void LoadAircraftFromFile()
    {
        Console.WriteLine("Please specify the path of the file: ");
        string filePath = Console.ReadLine();
        // We ak for the file of the path

        while (!File.Exists(filePath))
        {
            Console.WriteLine("The file provided does not exist, please provide another one:");
            filePath = Console.ReadLine();
        }
        // We check weather the file exists using the File.Exists function and if it doesnt, we ask for another path

        string separator = ",";

        StreamReader sl = File.OpenText(filePath);
        
        string line;
        // We create a StremReader variable called s1 with which we will read the file and stablish the separator as a comma
        
        while((line = sl.ReadLine()) != null)
        {
            string[] values = line.Split(separator);
            // We create an array to almacenate the values separated by the comma with the Split function

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
                // Depending on the type of aircraft, we create a new one of that specific type.
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
        // Ask what type of Aircraft we wish

        string choice = Console.ReadLine();
        Aircraft newAircraft;
        //We create a new Aircraft

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
        // We ask for the value of every attribute of the aircraft

        double actualFuel = fuelCapacity;
        AircraftStatus status = (distance > 0) ? AircraftStatus.InFlight : AircraftStatus.Waiting;
        //For the state, we evaluate if the distance is greater than 0 with a ternary if

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
            // For each type of Aircraft we create a new one of the specific type using a switch case with the selected option
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
            // It calls the AdvanceTick function for the desired number of ticks, in this case 150. It also clears the Console and waits 3 seconds before each tick, just for clarity.
    }
    }
}