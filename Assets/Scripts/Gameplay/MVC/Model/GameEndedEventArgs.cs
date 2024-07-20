using System;

namespace TicTacToe.Gameplay.MVC.Model
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
