using TicTacToe.Gameplay.MVC.Controller;

namespace TicTacToe.Gameplay.GameParticipants
{
    public class HumanParticipant : GameParticipant
    {
        public override void StartTurn(TicTacToeController ticTacToeController)
        {
            base.StartTurn(ticTacToeController);

            ticTacToeController.SetButtonsInteractable(true);
        }

        public override void EndTurn(TicTacToeController ticTacToeController)
        {
            base.EndTurn(ticTacToeController);
            
            ticTacToeController.SetButtonsInteractable(false);
        }
    }
}
