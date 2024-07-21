using TicTacToe.Gameplay.MVC.Controller;
using TicTacToe.UI;
using UnityEngine;

namespace TicTacToe.Gameplay
{
    public class GameplayUtilities : MonoBehaviour
    {
        [SerializeField] private GameplayUtilitiesWidget widget;

        private TicTacToeController ticTacToeController;
        
        public void Init(TicTacToeController ticTacToeController, bool isPlayingAgainstComputer)
        {
            this.ticTacToeController = ticTacToeController;
            ShowAllowedUtilities(isPlayingAgainstComputer);
            SetUpButtons();
        }

        private void ShowAllowedUtilities(bool isPlayingAgainstComputer)
        {
            widget.SetHintButtonActive(isPlayingAgainstComputer);
            widget.SetUndoButtonActive(isPlayingAgainstComputer);
            widget.SetRestartButtonActive(true);
        }

        private void SetUpButtons()
        {
            widget.AddListenerToHintButton(ShowHint);
        }

        private void ShowHint()
        {
            ticTacToeController.ShowHint();
        }
    }
}
