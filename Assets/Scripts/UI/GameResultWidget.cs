using TicTacToe.Gameplay.MVC.Model;
using TicTacToe.Gameplay.MVC.View;
using UnityEngine;

namespace TicTacToe.UI
{
    public class GameResultWidget : MonoBehaviour
    {
        [SerializeField] private GameObject drawHierarchyParent;
        [SerializeField] private GameObject winnerHierarchyParent;
        [SerializeField] private SymbolWidget winnerSymbolWidget;

        public void ShowResult(Symbol? winner)
        {
            gameObject.SetActive(true);
            drawHierarchyParent.SetActive(winner == null);
            winnerHierarchyParent.SetActive(winner != null);

            if (winner != null)
            {
                winnerSymbolWidget.ChangeSymbol(winner);
            }
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}