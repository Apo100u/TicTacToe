using System;

namespace TicTacToe.Model
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
