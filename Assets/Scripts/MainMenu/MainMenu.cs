using System;
using System.Collections.Generic;
using TicTacToe.UI;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        private const string HumanPlayerChoiceName = "Human";
        private const string ComputerPlayerChoiceName = "Computer";

        [SerializeField] private Button startGameButton;
        [SerializeField] private ParticipantChoiceWidget[] participantChoiceWidgets;

        private void Start()
        {
            SetUpParticipantsChoiceWidgets();
        }

        private void OnEnable()
        {
            startGameButton.onClick.AddListener(StartGame);
        }

        private void OnDisable()
        {
            startGameButton.onClick.RemoveListener(StartGame);
        }

        private void StartGame()
        {
            
        }

        private void SetUpParticipantsChoiceWidgets()
        {
            List<string> participantsOptions = new()
            {
                HumanPlayerChoiceName,
                ComputerPlayerChoiceName
            };
            
            for (int i = 0; i < participantChoiceWidgets.Length; i++)
            {
                participantChoiceWidgets[i].SetOptions(participantsOptions);
            }
        }
    }
}
