using System;
using TicTacToe.Gameplay.MVC.Controller;
using TicTacToe.UI;
using UnityEngine;

namespace TicTacToe.Gameplay
{
    public class GameplayUtilities : MonoBehaviour
    {
        public event EventHandler RestartRequested;

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
            widget.AddListenerToRestartButton(Restart);
        }

        private void ShowHint()
        {
            ticTacToeController.ShowHint();
        }

        private void UndoLastTurn()
        {
            ticTacToeController.UndoLastTurn(participantsCount);
        }

        private void Restart()
        {
            RestartRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
