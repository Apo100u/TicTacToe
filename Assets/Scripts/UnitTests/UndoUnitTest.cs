using TicTacToe.Gameplay.GameParticipants;
using TicTacToe.Gameplay.MVC.Controller;
using TicTacToe.Gameplay.MVC.Model;
using UnityEngine;

namespace TicTacToe.UnitTests
{
    [CreateAssetMenu(fileName = "New Undo Unit Test", menuName = "Scriptable Objects/Unit Test/Undo")]
    public class UndoUnitTest : UnitTest
    {
        private Symbol?[,] gridStateBeforeUndo;
        private Symbol?[,] gridStateAfterUndo;
        private Gameplay.Gameplay gameplay;
        private TicTacToeController controller;
        private GameParticipant[] gameParticipants;

        public override void Arrange()
        {
            gameplay = FindObjectOfType<Gameplay.Gameplay>();
            controller = FindObjectOfType<TicTacToeController>();

            gameParticipants = new GameParticipant[] { new HumanParticipant(), new ComputerParticipant() };
            
            gameplay.Init(gameParticipants, gameSettingsToUse.Balance, gameSettingsToUse.Visuals);
            gameplay.StartNewTicTacToeGame();
        }

        public override void Act()
        {
            gridStateBeforeUndo = CopyGridState();
            controller.MakeRandomMoveWithCurrentSymbol();
            controller.UndoLastTurn(gameParticipants.Length);
            gridStateAfterUndo = CopyGridState();
        }

        public override bool Assert()
        {
            return IsGridStateTheSame();
        }

        private bool IsGridStateTheSame()
        {
            bool isGridStateTheSame = true;
            (int x, int y) incorrectState = default;
            
            for (int i = 0; i < controller.GridSize; i++)
            {
                for (int j = 0; j < controller.GridSize; j++)
                {
                    if (gridStateBeforeUndo[i, j] != gridStateAfterUndo[i, j])
                    {
                        incorrectState = (i, j);
                        isGridStateTheSame = false;
                        break;
                    }
                }

                if (!isGridStateTheSame)
                {
                    break;
                }
            }

            if (!isGridStateTheSame)
            {
                LogFailDetails($"Grid state was different after one turn and undo. Detected incorrect cell: [{incorrectState.x}, {incorrectState.y}].");
            }

            return isGridStateTheSame;
        }

        private Symbol?[,] CopyGridState()
        {
            Symbol?[,] gridState = new Symbol?[controller.GridSize, controller.GridSize];

            for (int i = 0; i < controller.GridSize; i++)
            {
                for (int j = 0; j < controller.GridSize; j++)
                {
                    gridState[i, j] = controller.GetSymbol(i, j);
                }
            }

            return gridState;
        }
    }
}