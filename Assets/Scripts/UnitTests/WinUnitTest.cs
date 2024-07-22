using TicTacToe.Gameplay.GameParticipants;
using TicTacToe.Gameplay.MVC.Controller;
using TicTacToe.Gameplay.MVC.Model;
using UnityEngine;

namespace TicTacToe.UnitTests
{
    [CreateAssetMenu(fileName = "New Win Unit Test", menuName = "Scriptable Objects/Unit Test/Win")]
    public class WinUnitTest : UnitTest
    {
        private Gameplay.Gameplay gameplay;
        private TicTacToeController controller;
        private GameParticipant winningPlayer;
        private GameParticipant losingPlayer;
        
        public override void Arrange()
        {
            gameplay = FindObjectOfType<Gameplay.Gameplay>();
            controller = FindObjectOfType<TicTacToeController>();

            winningPlayer = new HumanParticipant();
            losingPlayer = new HumanParticipant();
            
            gameplay.Init(new []{ winningPlayer, losingPlayer });
            gameplay.StartNewTicTacToeGame();
        }

        public override void Act()
        {
            CreateWinningCombinationForX();
        }

        public override bool Assert()
        {
            return IsGameWinnerCorrectSymbol();
        }

        private bool IsGameWinnerCorrectSymbol()
        {
            bool isGameWinnerCorrectSymbol = gameplay.Winner == Symbol.X;

            if (!isGameWinnerCorrectSymbol)
            {
                string actualWinner = gameplay.Winner.ToString() ?? "[null]";
                LogFailDetails($"Incorrect winner. Tried to create winning combination for X, but the winner was {actualWinner}.");
            }

            return isGameWinnerCorrectSymbol;
        }

        private void CreateWinningCombinationForX()
        {
            controller.MakeMoveWithCurrentSymbol(0, 0);
            controller.MakeMoveWithCurrentSymbol(0, 2);
            controller.MakeMoveWithCurrentSymbol(1, 1);
            controller.MakeMoveWithCurrentSymbol(2, 0);
            controller.MakeMoveWithCurrentSymbol(2, 2);
        }
    }
}
