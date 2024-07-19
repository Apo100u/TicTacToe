using System;

namespace TicTacToeBackend
{
    public class GameEndedEventArgs : EventArgs
    {
        public readonly Symbol? Winner;

        public GameEndedEventArgs(Symbol? winner)
        {
            Winner = winner;
        }
    }
}
