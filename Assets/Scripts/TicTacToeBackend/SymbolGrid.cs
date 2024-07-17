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

        public Symbol? GetSymbol(int gridPositionX, int gridPositionY)
        {
            return grid[gridPositionX, gridPositionY];
        }

        public void AddSymbol(Symbol symbol, int gridPositionX, int gridPositionY)
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
            else
            {
                LogInvalidCoordinates(gridPositionX, gridPositionY);
            }
        }

        public void RemoveSymbol(int gridPositionX, int gridPositionY)
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
            else
            {
                LogInvalidCoordinates(gridPositionX, gridPositionY);
            }
        }

        private bool ValidateCoordinates(int xToValidate, int yToValidate)
        {
            return xToValidate < SizeX && yToValidate < SizeY;
        }

        private void LogInvalidCoordinates(int gridPositionX, int gridPositionY)
        {
                Debug.LogError($"Invalid {nameof(SymbolGrid)} coordinates. Input was [{gridPositionX}, {gridPositionY}] but the grid size is [{SizeX}, {SizeY}].");
        }
    }
}
