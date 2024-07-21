using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe.UI
{
    public class GameplayUtilitiesWidget : MonoBehaviour
    {
        [SerializeField] private Button hintButton;
        [SerializeField] private Button undoButton;
        [SerializeField] private Button restartButton;

        public void SetHintButtonActive(bool active)
        {
            hintButton.gameObject.SetActive(active);
        }
        
        public void SetUndoButtonActive(bool active)
        {
            undoButton.gameObject.SetActive(active);
        }
        
        public void SetRestartButtonActive(bool active)
        {
            restartButton.gameObject.SetActive(active);
        }
    }
}
