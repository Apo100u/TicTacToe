using System;

namespace TicTacToe.MVC.Model
{
    public class SymbolAddedEventArgs : EventArgs
    {
        public readonly Symbol Symbol;
        public readonly int GridPositionX;
        public readonly int GridPositionY;

        public SymbolAddedEventArgs(Symbol symbol, int gridPositionX, int gridPositionY)
        {
            Symbol = symbol;
            GridPositionX = gridPositionX;
            GridPositionY = gridPositionY;
        }
    }
}
