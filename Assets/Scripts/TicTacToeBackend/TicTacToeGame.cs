using System.Collections.Generic;
using TicTacToeBackend.Commands;

namespace TicTacToeBackend
{
    public class TicTacToeGame
    {
        private LinkedList<Command> commandsInOrder;

        public void ExecuteCommand(Command command)
        {
            commandsInOrder.AddLast(command);
            command.Execute(this);
        }
    }
}
