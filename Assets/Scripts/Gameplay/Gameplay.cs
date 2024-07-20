using System.Collections.Generic;
using TicTacToe.Gameplay.GameParticipants;
using TicTacToe.Gameplay.MVC.Controller;
using TicTacToe.Gameplay.MVC.Model;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TicTacToe.Gameplay
{
    public class Gameplay : MonoBehaviour
    {
        [SerializeField] private TicTacToeController ticTacToeController;

        private GameParticipant participantOnMove;
        private GameParticipant[] gameParticipants;

        public void Init(GameParticipant[] gameParticipants)
        {
            this.gameParticipants = gameParticipants;
            ticTacToeController.Init();

            AssignSymbolsToParticipants();
        }

        public void StartTicTacToeGame()
        {
            participantOnMove = gameParticipants[0];
            
            ticTacToeController.SetNextSymbol(participantOnMove.Symbol);
        }

        private void AssignSymbolsToParticipants()
        {
            List<Symbol> availableSymbols = new() { Symbol.X, Symbol.O };

            for (int i = 0; i < gameParticipants.Length; i++)
            {
                int randomSymbolIndex = Random.Range(0, availableSymbols.Count);
                
                gameParticipants[i].AssignSymbol(availableSymbols[randomSymbolIndex]);

                availableSymbols.RemoveAt(randomSymbolIndex);
                
                if (availableSymbols.Count == 0)
                {
                    availableSymbols = new List<Symbol> { Symbol.X, Symbol.O };
                }
            }
        }
    }
}
