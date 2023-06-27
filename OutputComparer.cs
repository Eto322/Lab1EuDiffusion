using System.IO;

namespace Lab1EuDiffusion
{
    public static class OutputComparer
    {
        public static bool CompareOutputWithDesired(string outputFilePath, string desiredFilePath)
        {
            string output = ReadFromFile(outputFilePath);
            string desiredOutput = ReadFromFile(desiredFilePath);

            string normalizedOutput = NormalizeString(output);
            string normalizedDesiredOutput = NormalizeString(desiredOutput);

            return normalizedOutput.Equals(normalizedDesiredOutput);
        }

        private static string ReadFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Dispose();
                return string.Empty;
            }

            string fileContent = File.ReadAllText(filePath);
            return fileContent.Trim(); // Trim to remove leading/trailing whitespace
        }

        private static string NormalizeString(string input)
        {
            return input.Replace("\r\n", "\n").TrimEnd(); // Normalize line endings and remove trailing whitespace
        }
    }
}
