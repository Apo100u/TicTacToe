using System;
using System.Collections.Generic;
using TicTacToe.Gameplay.MVC.Model.Commands;
using TicTacToe.Gameplay.MVC.Model.Helpers;

namespace TicTacToe.Gameplay.MVC.Model
{
    public class TicTacToeGame
    {
        public event EventHandler<GameEndedEventArgs> GameWonOrTied;

        public readonly SymbolGrid Grid;

        public int MovesCount => commandsInOrder.Count;

        private LinkedList<Command> commandsInOrder = new();

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

        public void UndoLastCommand(out Command undoneCommand)
        {
            undoneCommand = null;
            
            if (commandsInOrder.Count > 0)
            {
                undoneCommand = commandsInOrder.Last.Value;
                commandsInOrder.Last.Value.Undo(this);
                commandsInOrder.RemoveLast();
            }
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
                GameWonOrTied?.Invoke(this, new GameEndedEventArgs(winningSymbol));
            }
        }
    }
}
