using System;

namespace PracticalWorkI
{
    class Program
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

                string option = Console.ReadLine();
            }
        }
    }
}
