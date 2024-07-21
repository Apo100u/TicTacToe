using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TicTacToe.Gameplay.MVC.Model
{
    public class SymbolGrid
    {
        public event EventHandler<SymbolAddedEventArgs> SymbolAdded;

        public readonly int Size;

        private readonly Symbol?[,] grid;
        
        private int emptyCellsCount;
        private bool[,] occupiedCells;

        public SymbolGrid(int size)
        {
            Size = size;

            grid = new Symbol?[Size, Size];
            occupiedCells = new bool[Size, Size];
            emptyCellsCount = Size * Size;
        }

        public bool IsEveryCellOccupied()
        {
            return emptyCellsCount == 0;
        }

        public Symbol? GetSymbol(int gridPositionX, int gridPositionY)
        {
            return grid[gridPositionX, gridPositionY];
        }

        public bool IsCellOccupied(int gridPositionX, int gridPositionY)
        {
            return GetSymbol(gridPositionX, gridPositionY) != null;
        }

        public void AddSymbol(Symbol symbol, int gridPositionX, int gridPositionY)
        {
            if (ValidateCoordinates(gridPositionX, gridPositionY))
            {
                if (grid[gridPositionX, gridPositionY] == null)
                {
                    grid[gridPositionX, gridPositionY] = symbol;
                    occupiedCells[gridPositionX, gridPositionY] = true;
                    emptyCellsCount--;
                    
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
                    occupiedCells[gridPositionX, gridPositionY] = false;
                    emptyCellsCount++;
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

        public void GetRandomEmptyCellGridPosition(out int gridPositionX, out int gridPositionY)
        {
            List<(int x, int y)> emptyCellsGridPositions = new();

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (!occupiedCells[i, j])
                    {
                        emptyCellsGridPositions.Add((i, j));
                    }
                }
            }

            (int x, int y) = emptyCellsGridPositions[Random.Range(0, emptyCellsGridPositions.Count)];
            gridPositionX = x;
            gridPositionY = y;
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
