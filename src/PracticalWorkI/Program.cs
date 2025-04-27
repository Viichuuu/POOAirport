using System;

namespace PracticalWorkI
{
    public class Program
    {
        public static void Main()
        {
            bool exit = false;
            string option = "";

            Airport AirUFV = new Airport(2,2);
            
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

                 option = Console.ReadLine();

               switch(option)
               {
                    case "1":
                       AirUFV.LoadAircraftFromFile();
                       break;  
                    case "2":
                       AirUFV.AddAircraft();
                       break;
                    case "3":
                       AirUFV.RunSIM();
                       break;
                    case "4":
                       exit = true;
                       break;
                    default:
                        Console.WriteLine("Please select one of the options");
                        break;
               }
            }
        }
    }
}

