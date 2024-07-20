using TicTacToe.Gameplay.MVC.Model;
using TicTacToe.Gameplay.MVC.Model.Commands;
using TicTacToe.Gameplay.MVC.View;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe.Gameplay.MVC.Controller
{
    public class TicTacToeController : MonoBehaviour
    {
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
            SetUpButtonsInteractions();
        }

        public void SetNextSymbol(Symbol symbol)
        {
            nextSymbol = symbol;
        }

        private void SetUpButtonsInteractions()
        {
            for (int i = 0; i < gridButtons.Length; i++)
            {
                int gridButtonIndex = i;
                Button button = gridButtons[i].GetComponent<Button>();
                
                button.onClick.AddListener(() => OnInteractionButtonClicked(gridButtons[gridButtonIndex]));
            }
        }

        private void OnInteractionButtonClicked(GridWidget gridButton)
        {
            AddSymbolCommand addSymbolCommand = new(nextSymbol, gridButton.GridPositionX, gridButton.GridPositionY);
            
            ticTacToeGame.ExecuteCommand(addSymbolCommand);
            view.UpdateSymbolWidget(gridButton.GridPositionX, gridButton.GridPositionY, nextSymbol);
        }
    }
}
