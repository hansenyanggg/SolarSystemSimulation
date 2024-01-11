//MP 
//This file contains the SimulatorMain with the Main method for the program.

//Complete the Main method. Do not add any new using directive, methods, or fields.

using System;

namespace SolarSystemSimulation
{
    class SimulatorMain
    {
        public static void Main()
        {
            PlanetarySystem planets = new PlanetarySystem(0);
            SimulationTimer timer = new SimulationTimer();
            bool running = false; //set to true when a simulation is running
            bool paused = false; //set to true when a simulation is paused

            Console.WriteLine("Welcome to the solar planet simulator! Select from the menu.");
            while (true)
            {
                Console.WriteLine();
                Console.Write("(s)tart, (p)ause, (r)esume, (g)et status, (q)uit? ");

                string choice;
                try
                {
                    choice = Console.ReadLine().Trim().ToLower();
                }
                catch (System.IO.IOException e)
                {
                    Console.WriteLine($"IO Exception: {e.Message}");
                    continue;
                }

                string result = "Please choose from the menu";

                if (choice.StartsWith("s"))
                {

                    string str = "";

                    Console.Write("\nHow many planets (1 to 9) [In addition to the Sun]? ");
                    if (GetInt(out int numOfPlanets, 1, 9, ref str) == true)
                    {
                        Console.Write("Simulation dt (ms) [1 to 1000]? (1 simulation dt (ms) = 1 day of actual time) ");
                        if (GetInt(out int duration, 1, 1000, ref str) == true)
                        {
                            Console.Write("Simulation duration (how many dt's) [1 to 1000]? ");
                            if (GetInt(out int simTimeInt, 1, 1000, ref str) == true)
                            {

                                paused = false;
                                running = true;

                                planets = new PlanetarySystem(numOfPlanets);
                                timer = new SimulationTimer();
                                timer.SetTimer(planets, duration, simTimeInt);

                                Console.WriteLine($"\nA new simulation of {numOfPlanets} initiated.");
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine(str);
                            }
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine(str);
                        }
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine(str);
                    }
                }
                else if (choice.StartsWith("p"))
                {
                    if (running == true && paused == false)
                    {

                        timer.Pause();
                        Console.WriteLine("\nPaused.");
                        paused = true;
                    }
                    else if (paused == true)
                    {
                        Console.WriteLine("\nSimulation is already paused");
                    }

                    else
                    {
                        Console.WriteLine("\nNo simulation is running to be pasued");
                    }

                }
                else if (choice.StartsWith("r"))
                {
                    if (paused == true)
                    {
                        timer.Resume();
                        Console.WriteLine("\nResumed.");

                        paused = false;
                    }
                    else
                    {
                        Console.WriteLine("\nNo simulation is paused to be resumed.");
                    }

                }
                else if (choice.StartsWith("g"))
                {
                    if (running == true)
                    {

                        Console.WriteLine($"\nAt time: {timer.GetSimulationTime()}");
                        planets.GetCurrentState(out string statusResult);
                        Console.WriteLine(statusResult);
                    }
                    else
                    {
                        Console.WriteLine("\nNo simulation is running.");
                    }
                }
                else if (choice.StartsWith("q"))
                {
                    break;
                }
                Console.WriteLine();
                Console.WriteLine(result);
            }
        }

        /// <summary>
        /// Gets an int from the command line within the range [min, max]. 
        /// If the provided num is not acceptable, str will contain an error message.
        /// </summary>
        /// <param name="num">Nummber received from the user and returned through the out argument.</param>
        /// <param name="min">Min acceptable range for num.</param>
        /// <param name="max">Max acceptable range for num.</param>
        /// <param name="str">Contains an error message if not successful.</param>
        /// <returns>true if successful, false otherwise.</returns>
        private static bool GetInt(out int num, int min, int max, ref string str)
        {
            if (int.TryParse(Console.ReadLine().Trim(), out num))
            {
                if (num >= min && num <= max)
                {
                    return true;
                }
                str = "Number outside range";
            }
            else
            {
                str = "Not an acceptable number";
            }
            return false;
        }
    }
}