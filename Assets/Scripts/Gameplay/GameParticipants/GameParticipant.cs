using TicTacToe.Gameplay.MVC.Controller;
using TicTacToe.Gameplay.MVC.Model;

namespace TicTacToe.Gameplay.GameParticipants
{
    public abstract class GameParticipant
    {
        public Symbol Symbol { get; private set; }
        
        public void AssignSymbol(Symbol symbol)
        {
            Symbol = symbol;
        }

        public virtual void StartTurn(TicTacToeController ticTacToeController)
        {
            
        }
    }
}
