using TicTacToe.Gameplay.MVC.Controller;
using TicTacToe.UI;
using UnityEngine;

namespace TicTacToe.Gameplay
{
    public class GameplayUtilities : MonoBehaviour
    {
        [SerializeField] private GameplayUtilitiesWidget widget;

        private TicTacToeController ticTacToeController;
        private int participantsCount;
        
        public void Init(TicTacToeController ticTacToeController, int participantsCount, bool isPlayingAgainstComputer)
        {
            this.ticTacToeController = ticTacToeController;
            this.participantsCount = participantsCount;
            SetHintAndUndoAllowed(isPlayingAgainstComputer);
            SetUpButtons();
        }

        public void SetHintAndUndoAllowed(bool allowed)
        {
            widget.SetHintButtonActive(allowed);
            widget.SetUndoButtonActive(allowed);
        }

        private void SetUpButtons()
        {
            widget.AddListenerToHintButton(ShowHint);
            widget.AddListenerToUndoButton(UndoLastTurn);
        }

        private void ShowHint()
        {
            ticTacToeController.ShowHint();
        }

        private void UndoLastTurn()
        {
            ticTacToeController.UndoLastTurn(participantsCount);
        }
    }
}
