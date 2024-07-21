using UnityEngine;
using UnityEngine.Events;
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

        public void AddListenerToHintButton(UnityAction listener)
        {
            hintButton.onClick.AddListener(listener);
        }
        
        public void SetUndoButtonActive(bool active)
        {
            undoButton.gameObject.SetActive(active);
        }

        public void AddListenerToUndoButton(UnityAction listener)
        {
            undoButton.onClick.AddListener(listener);
        }
        
        public void SetRestartButtonActive(bool active)
        {
            restartButton.gameObject.SetActive(active);
        }

        public void AddListenerToRestartButton(UnityAction listener)
        {
            restartButton.onClick.AddListener(listener);
        }
    }
}
