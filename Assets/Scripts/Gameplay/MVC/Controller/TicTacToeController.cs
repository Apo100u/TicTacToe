using TicTacToe.Gameplay.MVC.Model;
using TicTacToe.Gameplay.MVC.View;
using TicTacToe.Gameplay.MVC.Model.Commands;
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

        public void Init(TicTacToeGame ticTacToeGame)
        {
            this.ticTacToeGame = ticTacToeGame;
            view.Init(symbolWidgets, gridSize);

            SetUpInteractions(gridButtons);
        }

        private void SetUpInteractions(GridWidget[] gridButtons)
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
            
        }
    }
}
