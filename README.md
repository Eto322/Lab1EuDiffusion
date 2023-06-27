# Lab1EuDiffusion

This repository contains a simulation program for the diffusion of coins among European countries. The program takes input data specifying the initial conditions for each country and simulates the diffusion process until all countries are fully connected.

## Program Overview

The program consists of the following files:

- `CoinWorker.cs`: Contains the `CoinWorker` class, which represents an individual country and implements the coin diffusion logic.
- `DiffusionWorker.cs`: Contains the `DiffusionWorker` class, which manages the simulation of coin diffusion among multiple countries.
- `Program.cs`: Contains the main entry point of the program, where the simulation is executed and the results are generated.
- `OutputComparer.cs`: Contains the `OutputComparer` class, which is used to compare the program's output with the desired output.
- `input.txt`: Sample input file containing the initial conditions for the simulation.
- `output.txt`: Sample output file generated by the program.
- `desired_output.txt`: Sample desired output file for comparison.

## How to Use

To use the program, you need to have the .NET SDK installed on your machine. Follow these steps to run the program:

1. Clone the repository to your local machine.
   ```bash
   git clone https://github.com/Eto322/Lab1EuDiffusion.git
   ```

2. Navigate to the project directory.
   ```bash
   cd Lab1EuDiffusion
   ```

3. Build the solution using the .NET SDK.
   ```bash
   dotnet build
   ```

4. Modify the `input.txt` file to specify the initial conditions for the simulation, if desired.

5. Run the program using the .NET CLI.
   ```bash
   dotnet run
   ```

6. The simulation results will be displayed in the console, and the output will be saved to the `output.txt` file.

7. The program will automatically compare the generated output with the desired output in the `desired_output.txt` file and display whether they are equal.

You can customize the initial conditions for the simulation by modifying the `input.txt` file. Make sure to follow the specified format for defining the countries and their boundaries.

Note: The program requires the .NET SDK to be installed on your machine. If you don't have it installed, you can download it from the official [.NET website](https://dotnet.microsoft.com/download) and follow the installation instructions for your operating system.

## Dependencies

The program does not have any external dependencies and is implemented in C# using the .NET framework.
