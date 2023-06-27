using System;

namespace Lab1EuDiffusion
{
    class Program
    {
        const int MAX_X = 10;
        const int MAX_Y = 10;

        const string DESIRED_FILE_PATH = "desired_output.txt";
        const string OUTPUT_FILE_PATH = "output.txt";

        static void Main(string[] args)
        {
            DiffusionManager diffusionManager = new DiffusionManager();
            diffusionManager.RunSimulation();

            bool areEqual = OutputComparer.CompareOutputWithDesired(OUTPUT_FILE_PATH, DESIRED_FILE_PATH);

            Console.WriteLine("Simulation Results:");
            Console.WriteLine(diffusionManager.SimulationResults);
            Console.WriteLine("Are the simulation results equal to the desired output? " + areEqual);
            Console.ReadKey();
        }
    }
}
