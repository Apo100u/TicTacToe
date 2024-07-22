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

        [field: SerializeField] public int GridSize { get; private set; }
        
        [SerializeField] private TicTacToeView view;
        [SerializeField] private SymbolWidget[] symbolWidgets;
        [SerializeField] private GridWidget[] gridButtons;
        
        private TicTacToeGame ticTacToeGame;
        private Symbol nextSymbol;

        public void CreateNewGame()
        {
            view.Init(symbolWidgets, GridSize);
            ticTacToeGame = new TicTacToeGame(new SymbolGrid(GridSize));
            
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
        
        public void RemoveCallbackFromGameEnded(EventHandler<GameEndedEventArgs> callback)
        {
            ticTacToeGame.GameEnded -= callback;
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

        public bool IsCellOccupied(int gridPositionX, int gridPositionY)
        {
            return ticTacToeGame.Grid.IsCellOccupied(gridPositionX, gridPositionY);
        }

        public Symbol? GetSymbol(int gridPositionX, int gridPositionY)
        {
            return ticTacToeGame.Grid.GetSymbol(gridPositionX, gridPositionY);
        }

        private void SetUpGridButtonsInteractions()
        {
            for (int i = 0; i < gridButtons.Length; i++)
            {
                int gridPositionX = gridButtons[i].GridPositionX;
                int gridPositionY = gridButtons[i].GridPositionY;
                Button button = gridButtons[i].GetComponent<Button>();

                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => InteractWithCell(gridPositionX, gridPositionY));
            }
        }

        private void InteractWithCell(int gridPositionX, int gridPositionY)
        {
            if (!IsCellOccupied(gridPositionX, gridPositionY))
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
