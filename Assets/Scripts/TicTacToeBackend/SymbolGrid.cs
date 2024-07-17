using UnityEngine;

namespace TicTacToeBackend
{
    public class SymbolGrid
    {
        public readonly int SizeX;
        public readonly int SizeY;
        
        private readonly Symbol?[,] grid;

        public SymbolGrid(int sizeX, int sizeY)
        {
            SizeX = sizeX;
            SizeY = sizeY;

            grid = new Symbol?[sizeX, sizeY];
        }

        public Symbol? GetSymbolAt(int gridPositionX, int gridPositionY)
        {
            return grid[gridPositionX, gridPositionY];
        }

        public void AddSymbolAt(Symbol symbol, int gridPositionX, int gridPositionY)
        {
            if (ValidateCoordinates(gridPositionX, gridPositionY))
            {
                if (grid[gridPositionX, gridPositionY] == null)
                {
                    grid[gridPositionX, gridPositionY] = symbol;
                }
                else
                {
                    Debug.LogError(
                        $"Tried to place a symbol on a cell [{gridPositionX}, {gridPositionY}] that already contains a symbol. This shouldn't be possible.");
                }
            }
        }

        public void RemoveSymbolAt(int gridPositionX, int gridPositionY)
        {
            if (ValidateCoordinates(gridPositionX, gridPositionY))
            {
                if (grid[gridPositionX, gridPositionY] != null)
                {
                    grid[gridPositionX, gridPositionY] = null;
                }
                else
                {
                    Debug.LogError(
                        $"Tried to remove a symbol from a cell [{gridPositionX}, {gridPositionY}] that is already empty. This shouldn't be possible.");
                }
            }
        }

        private bool ValidateCoordinates(int xToValidate, int yToValidate)
        {
            bool isEveryCoordinateValid = xToValidate < SizeX && yToValidate < SizeY;

            if (!isEveryCoordinateValid)
            {
                Debug.LogError($"Invalid {nameof(SymbolGrid)} coordinates. Input was [{xToValidate}, {yToValidate}] but the grid size is [{SizeX}, {SizeY}].");
            }
            
            return isEveryCoordinateValid;
        }
    }
}
