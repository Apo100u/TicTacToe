using System.Collections.Generic;
using TicTacToeBackend.Commands;

namespace TicTacToeBackend
{
    public class TicTacToeGame
    {
        public readonly SymbolGrid Grid;
        
        private LinkedList<Command> commandsInOrder;

        public TicTacToeGame(SymbolGrid grid)
        {
            Grid = grid;
            
            Grid.SymbolAdded += OnSymbolAdded;
        }

        public void ExecuteCommand(Command command)
        {
            commandsInOrder.AddLast(command);
            command.Execute(this);
        }

        private void OnSymbolAdded(object sender, SymbolAddedEventArgs args)
        {
            CheckIfAddedSymbolEndsGame(args.Symbol, args.GridPositionX, args.GridPositionY);
        }

        private void CheckIfAddedSymbolEndsGame(Symbol symbol, int gridPositionX, int gridPositionY)
        {
            bool isSymbolInWinningCombination =
                IsColumnWinningCombination(symbol, gridPositionX)
                || IsRowWinningCombination(symbol, gridPositionY)
                || IsDiagonalWinningCombination(symbol, gridPositionX, gridPositionY);
        }

        private bool IsColumnWinningCombination(Symbol symbol, int columnGridPosition)
        {
            bool isWinningCombination = true;

            for (int i = 0; i < Grid.Size; i++)
            {
                Symbol? symbolToCheck = Grid.GetSymbol(columnGridPosition, i);
                
                if (symbolToCheck == null || symbolToCheck != symbol)
                {
                    isWinningCombination = false;
                    break;
                }
            }

            return isWinningCombination;
        }
        
        private bool IsRowWinningCombination(Symbol symbol, int rowGridPosition)
        {
            bool isWinningCombination = true;

            for (int i = 0; i < Grid.Size; i++)
            {
                Symbol? symbolToCheck = Grid.GetSymbol(i, rowGridPosition);
                
                if (symbolToCheck == null || symbolToCheck != symbol)
                {
                    isWinningCombination = false;
                    break;
                }
            }

            return isWinningCombination;
        }
        
        private bool IsDiagonalWinningCombination(Symbol symbol, int gridPositionX, int gridPositionY)
        {
            bool isWinningCombination = false;
            bool isOnIncreasingDiagonal = gridPositionX == gridPositionY;
            bool isOnDecreasingDiagonal = gridPositionY == Grid.Size - gridPositionX - 1;

            if (isOnIncreasingDiagonal)
            {
                isWinningCombination = IsIncreasingDiagonalWinningCombination(symbol);
            }

            if (!isWinningCombination && isOnDecreasingDiagonal)
            {
                isWinningCombination = IsDecreasingDiagonalWinningCombination(symbol);
            }

            return isWinningCombination;
        }

        private bool IsIncreasingDiagonalWinningCombination(Symbol symbol)
        {
            bool isWinningCombination = true;

            for (int i = 0; i < Grid.Size; i++)
            {
                Symbol? symbolToCheck = Grid.GetSymbol(i, i);
                
                if (symbolToCheck == null || symbolToCheck != symbol)
                {
                    isWinningCombination = false;
                    break;
                }
            }

            return isWinningCombination;
        }

        private bool IsDecreasingDiagonalWinningCombination(Symbol symbol)
        {
            bool isWinningCombination = true;

            for (int i = 0; i < Grid.Size; i++)
            {
                Symbol? symbolToCheck = Grid.GetSymbol(i, Grid.Size - i - 1);
                
                if (symbolToCheck == null || symbolToCheck != symbol)
                {
                    isWinningCombination = false;
                    break;
                }
            }

            return isWinningCombination;
        }
    }
}
