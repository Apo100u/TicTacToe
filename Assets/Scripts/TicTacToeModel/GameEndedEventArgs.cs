using System;

namespace TicTacToeModel
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
