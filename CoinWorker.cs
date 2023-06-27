using System;

namespace Lab1EuDiffusion
{
    class CoinWorker : IComparable
    {
        public string countryName { get; }
        public int numberOfDays { get; private set; }
        public bool isComplete { get; private set; }
        public int Xl { get; }
        public int Yl { get; }
        public int Xh { get; }
        public int Yh { get; }

        private int _max_X;
        private int _max_Y;
        private int[,] _currentMatrix;
        private bool[,] _countryMatrix;

        public CoinWorker(string name, int xl, int yl, int xh, int yh, int maxX, int maxY)
        {
            if (name.Length > 48) //United Kingdom of Great Britain and Northern Ireland (47 letters)
                throw new Exception("Name should not contain more than 48 letters.");

            if (xl >= maxX || yl >= maxY || xh >= maxX || yh >= maxY)
                throw new Exception("Coordinates should be less than the maximum value.");

            countryName = name;
            Xl = xl - 1;
            Yl = yl - 1;
            Xh = xh - 1;
            Yh = yh - 1;
            _max_X = maxX;
            _max_Y = maxY;

            _currentMatrix = new int[maxX, maxY];

            for (int x = Xl; x <= Xh; x++)
            {
                for (int y = Yl; y <= Yh; y++)
                {
                    _currentMatrix[x, y] = 1000000;
                }
            }
        }

        public void NextDay()
        {
            int[,] result = new int[10, 10];

            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    int amount = _currentMatrix[x, y] / 1000;
                    int transportationCount = TransportToNeighbors(result, x, y, amount);
                    result[x, y] += _currentMatrix[x, y] - transportationCount * amount;
                }
            }

            if (!isComplete)
                numberOfDays++;

            _currentMatrix = result;
        }

        private int TransportToNeighbors(int[,] matrix, int x, int y, int amount)
        {
            int transportationCount = 0;

            transportationCount += TryUpdateNeighborCoins( //left check (x-1)
                matrix,
                x - 1,
                y,
                amount
            );

            transportationCount += TryUpdateNeighborCoins( //right check (x+1)
                matrix,
                x + 1,
                y,
                amount
            );

            transportationCount += TryUpdateNeighborCoins( //down check (y-1)
                matrix,
                x,
                y - 1,
                amount
            );
            transportationCount += TryUpdateNeighborCoins( //up check (y+1)
                matrix,
                x,
                y + 1,
                amount
            );

            return transportationCount;
        }

        private int TryUpdateNeighborCoins(int[,] matrix, int x, int y, int amount)
        {
            if (CheckIsCityAvailable(x, y))
            {
                return UpdateNeighborCoins(matrix, x, y, amount);
            }

            return 0;
        }

        private int UpdateNeighborCoins(int[,] matrix, int x, int y, int amount)
        {
            matrix[x, y] += amount;
            return 1;
        }

        private bool CheckIsCityAvailable(int x, int y)
        {
            return x >= 0 && y >= 0 && x < _max_X && y < _max_Y && _countryMatrix[x, y];
        }

        public int CompareTo(object obj)
        {
            if (obj is CoinWorker other)
            {
                int comparison = numberOfDays.CompareTo(other.numberOfDays);
                return comparison != 0 ? comparison : countryName.CompareTo(other.countryName);
            }

            return -1;
        }

        public int GetCityCoins(int x, int y)
        {
            return _currentMatrix[x, y];
        }

        public void SetComplete(bool complete)
        {
            isComplete = complete;
        }

        public void SetMatrixOfCountries(bool[,] matrix)
        {
            _countryMatrix = matrix;
        }
    }
}
