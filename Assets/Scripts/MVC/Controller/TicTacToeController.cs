using TicTacToe.MVC.View;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe.MVC.Controller
{
    public class TicTacToeController : MonoBehaviour
    {
        [SerializeField] private TicTacToeView view;

        [SerializeField] private SymbolWidget[] symbolWidgets;
        [SerializeField] private GridWidget[] gridButtons;
        [SerializeField] private int gridSize;

        public void Init()
        {
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
