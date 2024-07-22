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
        private Symbol symbolOnMove;

        public void CreateNewGame()
        {
            view.Init(symbolWidgets, GridSize);
            ticTacToeGame = new TicTacToeGame(new SymbolGrid(GridSize));
            
            SetUpGridButtonsInteractions();
        }

        public void SetSymbolOnMove(Symbol symbol)
        {
            symbolOnMove = symbol;
        }
        
        public void MakeMoveWithCurrentSymbol(int gridPositionX, int gridPositionY)
        {
            if (!IsCellOccupied(gridPositionX, gridPositionY))
            {
                AddSymbolCommand addSymbolCommand = new(symbolOnMove, gridPositionX, gridPositionY);

                view.ClearHint();
                ticTacToeGame.ExecuteCommand(addSymbolCommand);
                view.ShowSymbol(gridPositionX, gridPositionY, symbolOnMove);

                MoveMade?.Invoke(this, EventArgs.Empty);
            }
        }

        public void MakeRandomMoveWithCurrentSymbol()
        {
            ticTacToeGame.Grid.GetRandomEmptyCellGridPosition(out int gridPositionX, out int gridPositionY);
            MakeMoveWithCurrentSymbol(gridPositionX, gridPositionY);
        }

        public void AddCallbackToGameWonOrTied(EventHandler<GameEndedEventArgs> callback)
        {
            ticTacToeGame.GameWonOrTied += callback;
        }
        
        public void RemoveCallbackFromGameWonOrTied(EventHandler<GameEndedEventArgs> callback)
        {
            ticTacToeGame.GameWonOrTied -= callback;
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
            view.ShowHint(gridPositionX, gridPositionY, symbolOnMove);
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
                button.onClick.AddListener(() => MakeMoveWithCurrentSymbol(gridPositionX, gridPositionY));
            }
        }
    }
}
