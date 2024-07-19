using System;
using UnityEngine;

namespace TicTacToeBackend
{
    public class SymbolGrid
    {
        public event EventHandler<SymbolAddedEventArgs> SymbolAdded;

        public readonly int Size;
        
        private readonly Symbol?[,] grid;

        public SymbolGrid(int size)
        {
            Size = size;

            grid = new Symbol?[Size, Size];
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
                    SymbolAdded?.Invoke(this, new SymbolAddedEventArgs(symbol, gridPositionX, gridPositionY));
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
            return xToValidate < Size && yToValidate < Size;
        }

        private void LogInvalidCoordinates(int gridPositionX, int gridPositionY)
        {
                Debug.LogError($"Invalid {nameof(SymbolGrid)} coordinates. Input was [{gridPositionX}, {gridPositionY}] but the grid size is [{Size}, {Size}].");
        }
    }
}
