using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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
            List<DiffusionWorker> euroDiffusionSimulations = ParseDiffusionInput("input.txt");

            foreach (DiffusionWorker euroDiffusionSimulation in euroDiffusionSimulations)
                euroDiffusionSimulation.RunSimulation();

            string simulationResults = GenerateSimulationResults(euroDiffusionSimulations);

            WriteToFile("output.txt", simulationResults);

            

            bool areEqual = OutputComparer.CompareOutputWithDesired(OUTPUT_FILE_PATH, DESIRED_FILE_PATH);

            Console.WriteLine("Simulation Results:");
            Console.WriteLine(simulationResults);
            Console.WriteLine("Are the simulation results equal to the desired output? " + areEqual);
            Console.ReadKey();
        }

        private static List<DiffusionWorker> ParseDiffusionInput(string input)
        {
            List<DiffusionWorker> result = new List<DiffusionWorker>();
            int countryCount = 0;

            using (StreamReader sr = new StreamReader(input))
            {
                while (!sr.EndOfStream)
                {
                    countryCount = Convert.ToInt32(sr.ReadLine());

                    if (countryCount < 0)
                        throw new Exception("Number of countries should be positive.");

                    CoinWorker[] countries = new CoinWorker[countryCount];

                    for (int i = 0; i < countryCount; i++)
                    {
                        string[] splitLine = sr.ReadLine()
                            .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        countries[i] = new CoinWorker(
                            splitLine[0],
                            Convert.ToInt32(splitLine[1]),
                            Convert.ToInt32(splitLine[2]),
                            Convert.ToInt32(splitLine[3]),
                            Convert.ToInt32(splitLine[4]),
                            MAX_X,
                            MAX_Y
                        );
                    }

                    DiffusionWorker euroDiffusionSimulation = new DiffusionWorker(
                        countries,
                        MAX_X,
                        MAX_Y
                    );
                    result.Add(euroDiffusionSimulation);
                }
            }

            return result;
        }

        private static string GenerateSimulationResults(List<DiffusionWorker> euroDiffusionSimulations)
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int caseNumber = 1; caseNumber <= euroDiffusionSimulations.Count; caseNumber++)
            {
                DiffusionWorker euroDiffusionSimulation = euroDiffusionSimulations[caseNumber - 1];

                stringBuilder
                    .Append("Case Number: ")
                    .Append(caseNumber)
                    .AppendLine()
                    .AppendLine(euroDiffusionSimulation.GetResults().Trim()) // Trim to remove leading/trailing whitespace
                    .AppendLine();
            }

            return stringBuilder.ToString().TrimEnd(); // Trim to remove trailing whitespace
        }

        private static void WriteToFile(string filePath, string content)
        {
            File.WriteAllText(filePath, content);
        }

        
    }
}
