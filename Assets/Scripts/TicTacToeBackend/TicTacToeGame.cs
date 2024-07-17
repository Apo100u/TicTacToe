using System.Collections.Generic;
using TicTacToeBackend.Commands;

namespace TicTacToeBackend
{
    public class TicTacToeGame
    {
        public readonly SymbolGrid Grid;
        
        private LinkedList<Command> commandsInOrder;

        public TicTacToeGame(int gridSizeX, int gridSizeY)
        {
            Grid = new SymbolGrid(gridSizeX, gridSizeY);
        }

        public void ExecuteCommand(Command command)
        {
            commandsInOrder.AddLast(command);
            command.Execute(this);
        }
    }
}
