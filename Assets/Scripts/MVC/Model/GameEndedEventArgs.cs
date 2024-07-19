using System;

namespace TicTacToe.MVC.Model
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
