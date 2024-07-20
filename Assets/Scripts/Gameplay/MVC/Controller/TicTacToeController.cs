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
            OnGridCellInteracted(gridPositionX, gridPositionY);
        }

        private void SetUpGridButtonsInteractions()
        {
            for (int i = 0; i < gridButtons.Length; i++)
            {
                int gridPositionX = gridButtons[i].GridPositionX;
                int gridPositionY = gridButtons[i].GridPositionY;
                Button button = gridButtons[i].GetComponent<Button>();
                
                button.onClick.AddListener(() => OnGridCellInteracted(gridPositionX, gridPositionY));
            }
        }

        private void OnGridCellInteracted(int gridPositionX, int gridPositionY)
        {
            AddSymbolCommand addSymbolCommand = new(nextSymbol, gridPositionX, gridPositionY);
            
            ticTacToeGame.ExecuteCommand(addSymbolCommand);
            view.UpdateSymbolWidget(gridPositionX, gridPositionY, nextSymbol);

            MoveMade?.Invoke(this, EventArgs.Empty);
        }
    }
}
