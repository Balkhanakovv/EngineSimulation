using System;
using System.Collections.Generic;

namespace Engine_simulation
{
    class Program
    {
        static void Main(string[] args)
        {
            double ambientTemperature = 0;
            List<double> torqueList = new List<double>() { 20, 75, 100, 105, 75, 0 };
            List<double> velocityList = new List<double>() { 0, 75, 150, 200, 250, 300 };
            Greeting();

            try
            {
                ambientTemperature = double.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("P.S. Try enter correct value (etc. 25 or 12,5) =)");
            }

            IEngine test = new ICEngine(10, torqueList, velocityList, 110, 0.01, 0.0001, 0.01, ambientTemperature);

            GetInfo(test as ICEngine, 300);
            Console.ReadKey();
        }

        private static void Greeting()
        {
            Console.WriteLine("\t\t\t||=======================================||");
            Console.WriteLine("\t\t\t||    Program for simulation of engine   ||");
            Console.WriteLine("\t\t\t||  and calculation of superheating time ||");
            Console.WriteLine("\t\t\t||=======================================||");
            Console.WriteLine("\t\t\t\n\n@@github: https://https://github.com/Balkhanakovv/Engine_simulation.git");
            Console.Write("\n\n\nInput ambient temperature (engine temperature = ambient temperature) in Celcius: ");
        }

        private static void GetInfo(ICEngine test, int testTime)
        {
            int result = test.GetSuperheatTime(testTime);

            if (result == testTime)
                Console.WriteLine("\n\n\nYour engine doesn't superheat! ! !");
            else
            {
                Console.WriteLine($"\n\n\nYour engine overheated in {result} seconds.");
                Console.WriteLine($"Ambient temperature: {test.ambientTemperature} Celcius.");
                Console.WriteLine($"Engine temperature at start: {test.ambientTemperature} Celcius.");
                Console.WriteLine($"Engine temperature at superheat: {Math.Round(test.engineTemperature, 2)} Celcius.");
            }
        }
    }
}
