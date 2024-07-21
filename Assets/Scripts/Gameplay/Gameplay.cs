using System;
using System.Collections.Generic;
using TicTacToe.Gameplay.GameParticipants;
using TicTacToe.Gameplay.MVC.Controller;
using TicTacToe.Gameplay.MVC.Model;
using TicTacToe.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TicTacToe.Gameplay
{
    public class Gameplay : MonoBehaviour
    {
        [SerializeField] private GameSettings gameSettings;
        [SerializeField] private TicTacToeController ticTacToeController;
        [SerializeField] private TurnTimer turnTimer;
        
        public static GameSettings GameSettings { get; private set; }

        private bool isGameEnded;
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
            GameSettings = gameSettings;
            
            ticTacToeController.Init();
            ticTacToeController.AddCallbackToGameEnded(OnWinOrDraw);

            turnTimer.TimeEnded += OnTurnTimeEnded;
            
            AssignSymbolsToParticipants();
        }

        public void StartTicTacToeGame()
        {
            ticTacToeController.MoveMade += OnMoveMade;
            participantOnMoveIndex = 0;
            
            StartNextTurn();
        }

        private void StartNextTurn()
        {
            GameParticipant participantOnMove = gameParticipants[participantOnMoveIndex];
            
            ticTacToeController.SetNextSymbol(participantOnMove.Symbol);
            turnTimer.StartCountdown();
            participantOnMove.StartTurn(ticTacToeController);
        }

        private void OnMoveMade(object sender, EventArgs args)
        {
            gameParticipants[participantOnMoveIndex].EndTurn(ticTacToeController);
            
            if (!isGameEnded)
            {
                participantOnMoveIndex++;

                if (participantOnMoveIndex >= gameParticipants.Length)
                {
                    participantOnMoveIndex = 0;
                }

                StartNextTurn();
            }
        }
        
        private void OnTurnTimeEnded(object sender, EventArgs args)
        {
            Symbol winner = gameParticipants[participantOnMoveIndex].Symbol == Symbol.O
                ? Symbol.X
                : Symbol.O;

            EndGame(winner);
        }
        
        private void OnWinOrDraw(object sender, GameEndedEventArgs args)
        {
            EndGame(args.Winner);
        }

        private void EndGame(Symbol? winner)
        {
            Debug.Log(winner == null ? $"Game ended with a draw." : $"Game ended. Winner: symbol {winner}.");

            turnTimer.Stop();
            isGameEnded = true;
            gameParticipants[participantOnMoveIndex].EndTurn(ticTacToeController);
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
