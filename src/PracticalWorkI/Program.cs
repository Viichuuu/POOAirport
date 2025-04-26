using System;

namespace PracticalWorkI
{
    class Program : Airport
    {
        static void Main()
        {
            bool exit = false;

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
                        Airport.LoadAircraftFromFile();
                        break;

                    case "2":
                        Airport.AddAircraft();
                        break;
                    case "3":
                        Airport.AdvanceTick();
                    case "4":
                        exit = true;
                }
            }
        }
    }
}
