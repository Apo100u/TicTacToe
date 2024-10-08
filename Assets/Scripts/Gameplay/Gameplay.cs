using System;
using System.Collections.Generic;
using TicTacToe.Gameplay.GameParticipants;
using TicTacToe.Gameplay.MVC.Controller;
using TicTacToe.Gameplay.MVC.Model;
using TicTacToe.ScriptableObjects.HelperStructs;
using TicTacToe.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TicTacToe.Gameplay
{
    public class Gameplay : MonoBehaviour
    {
        [SerializeField] private TicTacToeController ticTacToeController;
        [SerializeField] private TurnTimer turnTimer;
        [SerializeField] private GameplayUtilities utilities;
        [SerializeField] private GameResultWidget gameResultWidget;
        [SerializeField] private SpriteRenderer background;
        
        public static Balance Balance { get; private set; }
        public static Visuals Visuals { get; private set; }
        
        public Symbol? Winner { get; private set; }
        public bool IsGameEnded { get; private set; }

        private int participantOnMoveIndex;
        private GameParticipant[] gameParticipants;

        public void Init(GameParticipant[] gameParticipants, Balance balance, Visuals visuals)
        {
            this.gameParticipants = gameParticipants;
            Balance = balance;
            Visuals = visuals;

            background.sprite = Visuals.Background;

            utilities.Init(ticTacToeController, gameParticipants.Length);

            ticTacToeController.MoveMade += OnMoveMade;
            turnTimer.TimeEnded += OnTurnTimeEnded;
            utilities.RestartRequested += OnRestartRequested;
        }

        public void StartNewTicTacToeGame()
        {
            gameResultWidget.Hide();
            IsGameEnded = false;
            
            ticTacToeController.CreateNewGame();
            ticTacToeController.AddCallbackToGameWonOrTied(OnWinOrDraw);
            
            utilities.SetHintAndUndoAllowed(IsPlayingAgainstComputer());

            AssignSymbolsToParticipants();
            participantOnMoveIndex = GetIndexOfPlayerWithSymbol(Symbol.X);
            
            StartNextTurn();
        }

        private void StartNextTurn()
        {
            GameParticipant participantOnMove = gameParticipants[participantOnMoveIndex];
            
            ticTacToeController.SetSymbolOnMove(participantOnMove.Symbol);
            turnTimer.StartCountdown();
            participantOnMove.StartTurn(ticTacToeController);
        }

        private void OnMoveMade(object sender, EventArgs args)
        {
            gameParticipants[participantOnMoveIndex].EndTurn(ticTacToeController);
            
            if (!IsGameEnded)
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
            Winner = winner;
            gameResultWidget.ShowResult(winner);
            turnTimer.Stop();
            utilities.SetHintAndUndoAllowed(false);
            IsGameEnded = true;
            gameParticipants[participantOnMoveIndex].EndTurn(ticTacToeController);
        }
        
        private void OnRestartRequested(object sender, EventArgs args)
        {
            RestartTicTacToeGame();
        }

        private void RestartTicTacToeGame()
        {
            ticTacToeController.RemoveCallbackFromGameWonOrTied(OnWinOrDraw);
            StartNewTicTacToeGame();
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

        private int GetIndexOfPlayerWithSymbol(Symbol symbol)
        {
            int index = int.MinValue;
            
            for (int i = 0; i < gameParticipants.Length; i++)
            {
                if (gameParticipants[i].Symbol == symbol)
                {
                    index = i;
                }
            }

            return index;
        }

        private bool IsPlayingAgainstComputer()
        {
            bool isPlayingAgainstComputer = false;

            for (int i = 0; i < gameParticipants.Length; i++)
            {
                if (gameParticipants[i] is ComputerParticipant)
                {
                    isPlayingAgainstComputer = true;
                    break;
                }
            }

            return isPlayingAgainstComputer;
        }
    }
}
