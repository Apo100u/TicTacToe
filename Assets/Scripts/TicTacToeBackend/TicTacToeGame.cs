using System;
using System.Collections.Generic;
using TicTacToeBackend.Commands;
using TicTacToeBackend.Helpers;

namespace TicTacToeBackend
{
    public class TicTacToeGame
    {
        public event EventHandler<GameEndedEventArgs> GameEnded;

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
            CheckForGameEnd(args.Symbol, args.GridPositionX, args.GridPositionY);
        }

        private void CheckForGameEnd(Symbol symbol, int gridPositionX, int gridPositionY)
        {
            ResultChecker resultChecker = new(Grid);

            bool isSymbolWinning = resultChecker.IsSymbolWinning(symbol, gridPositionX, gridPositionY);
            bool isDraw = !isSymbolWinning && Grid.IsEveryCellOccupied();
            bool isGameEnded = isSymbolWinning || isDraw;

            if (isGameEnded)
            {
                Symbol? winningSymbol = isSymbolWinning ? symbol : null;
                GameEnded?.Invoke(this, new GameEndedEventArgs(winningSymbol));
            }
        }
    }
}
