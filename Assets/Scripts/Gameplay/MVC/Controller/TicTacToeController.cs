using System;
using TicTacToe.Gameplay.MVC.Model;
using TicTacToe.Gameplay.MVC.Model.Commands;
using TicTacToe.Gameplay.MVC.View;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe.Gameplay.MVC.Controller
{
    public class TicTacToeController : MonoBehaviour
    {
        public event EventHandler MoveMade;

        [SerializeField] private TicTacToeView view;
        [SerializeField] private SymbolWidget[] symbolWidgets;
        [SerializeField] private GridWidget[] gridButtons;
        [SerializeField] private int gridSize;
        
        private TicTacToeGame ticTacToeGame;
        private Symbol nextSymbol;

        public void Init()
        {
            view.Init(symbolWidgets, gridSize);
            ticTacToeGame = new TicTacToeGame(new SymbolGrid(gridSize));
            
            SetUpGridButtonsInteractions();
        }

        public void SetNextSymbol(Symbol symbol)
        {
            nextSymbol = symbol;
        }

        public void InteractWithRandomEmptyCell()
        {
            ticTacToeGame.Grid.GetRandomEmptyCellGridPosition(out int gridPositionX, out int gridPositionY);
            InteractWithCell(gridPositionX, gridPositionY);
        }

        public void AddCallbackToGameEnded(EventHandler<GameEndedEventArgs> callback)
        {
            ticTacToeGame.GameEnded += callback;
        }
        
        public void SetButtonsInteractable(bool interactable)
        {
            for (int i = 0; i < gridButtons.Length; i++)
            {
                Button button = gridButtons[i].GetComponent<Button>();
                button.interactable = interactable;
            }
        }
        
        public void ShowHint()
        {
            ticTacToeGame.Grid.GetRandomEmptyCellGridPosition(out int gridPositionX, out int gridPositionY);
            view.ShowHint(gridPositionX, gridPositionY, nextSymbol);
        }

        public void UndoLastTurn(int participantsCount)
        {
            bool atLeastOneFullTurnPassed = ticTacToeGame.MovesCount >= participantsCount;

            if (atLeastOneFullTurnPassed)
            {
                for (int i = 0; i < participantsCount; i++)
                {
                    ticTacToeGame.UndoLastCommand(out Command undoneCommand);

                    if (undoneCommand is AddSymbolCommand addSymbolCommand)
                    {
                        view.HideSymbol(addSymbolCommand.GridPositionX, addSymbolCommand.GridPositionY);
                    }
                }
            }
        }

        private void SetUpGridButtonsInteractions()
        {
            for (int i = 0; i < gridButtons.Length; i++)
            {
                int gridPositionX = gridButtons[i].GridPositionX;
                int gridPositionY = gridButtons[i].GridPositionY;
                Button button = gridButtons[i].GetComponent<Button>();
                
                button.onClick.AddListener(() => InteractWithCell(gridPositionX, gridPositionY));
            }
        }

        private void InteractWithCell(int gridPositionX, int gridPositionY)
        {
            if (!ticTacToeGame.Grid.IsCellOccupied(gridPositionX, gridPositionY))
            {
                AddSymbolCommand addSymbolCommand = new(nextSymbol, gridPositionX, gridPositionY);

                view.ClearHint();
                ticTacToeGame.ExecuteCommand(addSymbolCommand);
                view.ShowSymbol(gridPositionX, gridPositionY, nextSymbol);

                MoveMade?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
