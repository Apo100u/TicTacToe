using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TicTacToe.UI
{
    public class ParticipantChoiceWidget : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown dropdown;

        public void SetOptions(List<string> options)
        {
            dropdown.ClearOptions();
            dropdown.AddOptions(options);
        }

        public string GetChosenOption()
        {
            return dropdown.options[dropdown.value].text;
        }
    }
}
