using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1EuDiffusion
{
    class DiffusionWorker
    {
        const int MAX_NUMBER_OF_DAYS = 10000;
        const int MAX_NUMBER_OF_COUNTRIES = 44; //By the conventional definition, there are 44 sovereign states or nations in Europe.

        private readonly int _countryCount;
        private readonly CoinWorker[] _countryArray;
        private readonly bool[,] _countryMatrix;

        public DiffusionWorker(CoinWorker[] countries, int maxX, int maxY)
        {
            if (countries.Length > MAX_NUMBER_OF_COUNTRIES) //check input data
            {
                throw new Exception("Number of countries should not exceed the maximum value.");
            }

            _countryCount = countries.Length;
            this._countryArray = countries;

            _countryMatrix = new bool[maxX, maxY];
            foreach (CoinWorker country in countries) //fill our matrix
            {
                for (int x = country.Xl; x <= country.Xh; x++)
                {
                    for (int y = country.Yl; y <= country.Yh; y++)
                    {
                        _countryMatrix[x, y] = true;
                    }
                }

                country.SetMatrixOfCountries(_countryMatrix);
            }
        }

        public int GetNumberOfCountries()
        {
            return _countryCount;
        }

        public void RunSimulation()
        {
            int day = 0;
            while (!IsEnd())
            {
                day++;
                for (int i = 0; i < _countryCount; i++)
                {
                    _countryArray[i].NextDay();
                }
                if (day > MAX_NUMBER_OF_DAYS)
                    throw new Exception("The countries should be connected");
            }
        }

        private bool IsEnd()
        {
            bool result = true;
            for (int i = 0; i < _countryCount; i++)
            {
                if (!_countryArray[i].isComplete && !CheckCountryComplete(_countryArray[i]))
                    result = false;
            }
            return result;
        }

        // Checks if a country represented by the CoinWorker object is complete
        private bool CheckCountryComplete(CoinWorker country)
        {
            // Generates a range of X and Y coordinates within the given country's boundaries
            var isComplete = Enumerable
                .Range(country.Xl, country.Xh - country.Xl + 1)
                .SelectMany(
                    x =>
                        Enumerable
                            .Range(country.Yl, country.Yh - country.Yl + 1)
                            .Select(y => new { x, y })
                )
                // Checks if all the coordinates in the range have non-zero coins in all the countries
                .All(coords => _countryArray.All(c => c.GetCityCoins(coords.x, coords.y) != 0));

            country.SetComplete(isComplete);

            return isComplete;
        }

        public string GetResults()
        {
            // Sorts the countries based on the number of days and then the country name
            var sortedCountries = _countryArray
                .OrderBy(c => c.numberOfDays)
                .OrderBy(c => c.countryName)
                .ToList();
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < _countryCount; i++)
            {
                stringBuilder
                    .Append(sortedCountries[i].countryName)
                    .Append(" ")
                    .Append(sortedCountries[i].numberOfDays)
                    .Append("\n");
            }
            return stringBuilder.ToString();
        }
    }
}
