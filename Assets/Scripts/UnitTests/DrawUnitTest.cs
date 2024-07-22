using TicTacToe.Gameplay.GameParticipants;
using TicTacToe.Gameplay.MVC.Controller;
using UnityEngine;

namespace TicTacToe.UnitTests
{
    [CreateAssetMenu(fileName = "New Draw Unit Test", menuName = "Scriptable Objects/Unit Test/Draw")]
    public class DrawUnitTest : UnitTest
    {
        protected Gameplay.Gameplay gameplay;
        private TicTacToeController controller;

        public override void Arrange()
        {
            gameplay = FindObjectOfType<Gameplay.Gameplay>();
            controller = FindObjectOfType<TicTacToeController>();

            gameplay.Init(new GameParticipant[] {new HumanParticipant(), new HumanParticipant()});
            gameplay.StartNewTicTacToeGame();
        }

        public override void Act()
        {
            CreateDrawCombination();
        }

        public override bool Assert()
        {
            return IsGameEnded() && IsResultDraw();
        }

        private void CreateDrawCombination()
        {
            controller.MakeMoveWithCurrentSymbol(0, 0);
            controller.MakeMoveWithCurrentSymbol(1, 0);
            controller.MakeMoveWithCurrentSymbol(2, 0);
            controller.MakeMoveWithCurrentSymbol(1, 1);
            controller.MakeMoveWithCurrentSymbol(0, 1);
            controller.MakeMoveWithCurrentSymbol(0, 2);
            controller.MakeMoveWithCurrentSymbol(2, 1);
            controller.MakeMoveWithCurrentSymbol(2, 2);
            controller.MakeMoveWithCurrentSymbol(1, 2);
        }

        private bool IsGameEnded()
        {
            bool isGameEnded = gameplay.IsGameEnded;

            if (!isGameEnded)
            {
                LogFailDetails("Created drawing combination but the game was not marked as ended");
            }

            return isGameEnded;
        }

        private bool IsResultDraw()
        {
            bool isResultDraw = gameplay.Winner == null;

            if (!isResultDraw)
            {
                LogFailDetails($"Created drawing combination but there was a winner: {gameplay.Winner}.");
            }

            return isResultDraw;
        }
    }
}
