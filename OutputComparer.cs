using System;
using System.IO;

namespace Lab1EuDiffusion
{
    public static class OutputComparer
    {
        public static bool CompareOutputWithDesired(string outputFilePath, string desiredFilePath)
        {
            try
            {
                string output = ReadFromFile(outputFilePath);
                string desiredOutput = ReadFromFile(desiredFilePath);

                string normalizedOutput = NormalizeString(output);
                string normalizedDesiredOutput = NormalizeString(desiredOutput);

                return normalizedOutput.Equals(normalizedDesiredOutput);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while comparing output files:");
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private static string ReadFromFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Dispose();
                    return string.Empty;
                }

                string fileContent = File.ReadAllText(filePath);
                return fileContent.Trim(); // Trim to remove leading/trailing whitespace
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading the file: {filePath}");
                Console.WriteLine(ex.Message);
                return string.Empty;
            }
        }

        private static string NormalizeString(string input)
        {
            return input.Replace("\r\n", "\n").TrimEnd(); // Normalize line endings and remove trailing whitespace
        }
    }
}
