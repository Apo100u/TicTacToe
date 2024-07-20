using TicTacToe.Gameplay.MVC.Controller;

namespace TicTacToe.Gameplay.GameParticipants
{
    public class ComputerParticipant : GameParticipant
    {
        public override void StartTurn(TicTacToeController ticTacToeController)
        {
            base.StartTurn(ticTacToeController);
            
            // TODO: Wait a bit to make it more clear?
            ticTacToeController.InteractWithRandomEmptyCell();
        }
    }
}
