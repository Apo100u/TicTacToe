using TicTacToe.Gameplay.GameParticipants;
using TicTacToe.Gameplay.MVC.Controller;
using TicTacToe.Gameplay.MVC.Model;
using TicTacToe.Gameplay.MVC.View;
using UnityEngine;

namespace TicTacToe.UnitTests
{
    [CreateAssetMenu(fileName = "New Hint Unit Test", menuName = "Scriptable Objects/Unit Test/Hint")]
    public class HintUnitTest : UnitTest
    {
        private Gameplay.Gameplay gameplay;
        private TicTacToeController controller;
        private TicTacToeView view;
        private GameParticipant humanPlayer;
        private GameParticipant computerPlayer;
        
        public override void Arrange()
        {
            gameplay = FindObjectOfType<Gameplay.Gameplay>();
            controller = FindObjectOfType<TicTacToeController>();
            view = FindObjectOfType<TicTacToeView>();

            humanPlayer = new HumanParticipant();
            computerPlayer = new ComputerParticipant();
            
            gameplay.Init(new []{ humanPlayer, computerPlayer }, gameSettingsToUse.Balance, gameSettingsToUse.Visuals);
            gameplay.StartNewTicTacToeGame();
        }

        public override void Act()
        {
            controller.ShowHint();
        }

        public override bool Assert()
        {
            return IsHintCellEmpty() && IsHintCorrectSymbol();
        }

        private bool IsHintCellEmpty()
        {
            (int hintGridPositionX, int hintGridPositionY) = view.CurrentHintGridPosition;
            
            bool isHintCellEmpty = !controller.IsCellOccupied(hintGridPositionX, hintGridPositionY);

            if (!isHintCellEmpty)
            {
                LogFailDetails($"Hint was given in occupied cell [{hintGridPositionX}, {hintGridPositionY}].");
            }

            return isHintCellEmpty;
        }

        private bool IsHintCorrectSymbol()
        {
            (int hintGridPositionX, int hintGridPositionY) = view.CurrentHintGridPosition;

            Symbol? displayedSymbol = view.GetDisplayedSymbol(hintGridPositionX, hintGridPositionY);
            bool isHintCorrectSymbol = humanPlayer.Symbol == displayedSymbol;

            if (!isHintCorrectSymbol)
            {
                LogFailDetails($"Hint symbol was {displayedSymbol}, but the player symbol was {humanPlayer.Symbol}.");
            }

            return isHintCorrectSymbol;
        }
    }
}