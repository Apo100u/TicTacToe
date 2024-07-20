using System;
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

        private int participantOnMoveIndex;
        private GameParticipant[] gameParticipants;

        private void Start()
        {
            HumanParticipant test1 = new();
            ComputerParticipant test2 = new();
            
            Init(new GameParticipant[]{test1, test2});
            StartTicTacToeGame();
        }

        public void Init(GameParticipant[] gameParticipants)
        {
            this.gameParticipants = gameParticipants;
            ticTacToeController.Init();

            AssignSymbolsToParticipants();
        }

        public void StartTicTacToeGame()
        {
            ticTacToeController.MoveMade += OnMoveMade;
            participantOnMoveIndex = 0;
            
            StartNextTurn();
        }

        private void OnMoveMade(object sender, EventArgs args)
        {
            participantOnMoveIndex++;

            if (participantOnMoveIndex >= gameParticipants.Length)
            {
                participantOnMoveIndex = 0;
            }
            
            StartNextTurn();
        }

        private void StartNextTurn()
        {
            GameParticipant participantOnMove = gameParticipants[participantOnMoveIndex];
            
            ticTacToeController.SetNextSymbol(participantOnMove.Symbol);
            participantOnMove.StartTurn(ticTacToeController);
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
