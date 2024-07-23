using System;
using System.Collections.Generic;
using TicTacToe.Gameplay.GameParticipants;
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

        private GameBase.GameBase gameBase;
        
        public void Init(GameBase.GameBase gameBase)
        {
            this.gameBase = gameBase;
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
            gameBase.GameParticipants = GetChosenParticipants();
            gameBase.LoadGameplay();
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

        private GameParticipant[] GetChosenParticipants()
        {
            GameParticipant[] chosenParticipants = new GameParticipant[participantChoiceWidgets.Length];
            
            for (int i = 0; i < participantChoiceWidgets.Length; i++)
            {
                GameParticipant participant = participantChoiceWidgets[i].GetChosenOption() switch
                {
                    HumanPlayerChoiceName => new HumanParticipant(),
                    ComputerPlayerChoiceName => new ComputerParticipant(),
                    _ => throw new ArgumentOutOfRangeException()
                };

                chosenParticipants[i] = participant;
            }

            return chosenParticipants;
        }
    }
}
