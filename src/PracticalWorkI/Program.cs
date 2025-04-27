using System;

namespace PracticalWorkI
{
    public class Program
    {
        public static void Main()
        {
            bool exit = false;

            Airport AirUFV = new Airport(2,2)

            while(!exit)
            {
                Console.WriteLine("___________________________________________________");
                Console.WriteLine("|========== AIRPORT LANDING SIMULATOR  ===========|");
                Console.WriteLine("|_________________________________________________|");
                Console.WriteLine("|1. Load flights from file                        |");
                Console.WriteLine("|2. Load a flight manually                        |");
                Console.WriteLine("|3. Start simulation                              |");
                Console.WriteLine("|4. Exit                                          |");
                Console.WriteLine("|_________________________________________________|");

                string option = IntConsole.ReadLine();

                switch(option)
                {
                    case "1":
                        AirUFV.LoadAircraftFromFile();
                        break;

                    case "2":
                        AirUFV.AddAircraft();
                        break;
                    case "3":
                        AirUFV.ShowStatus();
                        AirUFV.AdvanceTick();
                    case "4":
                        exit = true;
                }
            }
        }
    }
}
