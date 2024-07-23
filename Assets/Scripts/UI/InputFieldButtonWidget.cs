using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe.UI
{
    public class InputFieldButtonClickedEventArgs : EventArgs
    {
        public readonly string InputFieldValue;

        public InputFieldButtonClickedEventArgs(string inputFieldValue)
        {
            InputFieldValue = inputFieldValue;
        }
    }
    
    public class InputFieldButtonWidget : MonoBehaviour
    {
        public event EventHandler<InputFieldButtonClickedEventArgs> ButtonClicked;

        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button button;

        private void OnEnable()
        {
            button.onClick.AddListener(NotifyButtonClicked);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(NotifyButtonClicked);
        }

        private void NotifyButtonClicked()
        {
            ButtonClicked?.Invoke(this, new InputFieldButtonClickedEventArgs(inputField.text));
        }
    }
}
