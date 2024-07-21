using TicTacToe.UI;
using UnityEngine;

namespace TicTacToe.Gameplay
{
    public class GameplayUtilities : MonoBehaviour
    {
        [SerializeField] private GameplayUtilitiesWidget widget;
        
        public void Init(bool isPlayingAgainstComputer)
        {
            ShowAllowedUtilities(isPlayingAgainstComputer);
        }

        private void ShowAllowedUtilities(bool isPlayingAgainstComputer)
        {
            widget.SetHintButtonActive(isPlayingAgainstComputer);
            widget.SetUndoButtonActive(isPlayingAgainstComputer);
            widget.SetRestartButtonActive(true);
        }
    }
}