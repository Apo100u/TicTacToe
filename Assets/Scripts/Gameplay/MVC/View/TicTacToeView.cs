using TicTacToe.Gameplay.MVC.Model;
using UnityEngine;

namespace TicTacToe.Gameplay.MVC.View
{
    public class TicTacToeView : MonoBehaviour
    {
        private SymbolWidget[,] symbolWidgets;
        private bool isShowingHint;
        private (int x, int y) currentHintGridPosition;

        public void Init(SymbolWidget[] symbolWidgets, int gridSize)
        {
            this.symbolWidgets = new SymbolWidget[gridSize, gridSize];
            
            AssignSymbolWidgets(symbolWidgets);
        }

        public void ShowSymbol(int gridPositionX, int gridPositionY, Symbol? symbol, bool showAsHint = false)
        {
            symbolWidgets[gridPositionX, gridPositionY].ChangeSymbol(symbol);
            symbolWidgets[gridPositionX, gridPositionY].SetAsHint(showAsHint);
        }
        
        public void HideSymbol(int gridPositionX, int gridPositionY)
        {
            symbolWidgets[gridPositionX, gridPositionY].ChangeSymbol(null);
        }
        
        public void ShowHint(int gridPositionX, int gridPositionY, Symbol symbol)
        {
            ShowSymbol(gridPositionX, gridPositionY, symbol, true);
            isShowingHint = true;
            currentHintGridPosition = (gridPositionX, gridPositionY);
        }

        public void ClearHint()
        {
            if (isShowingHint)
            {
                HideSymbol(currentHintGridPosition.x, currentHintGridPosition.y);
                isShowingHint = false;
            }
        }

        private void AssignSymbolWidgets(SymbolWidget[] symbolWidgets)
        {
            for (int i = 0; i < symbolWidgets.Length; i++)
            {
                symbolWidgets[i].ChangeSymbol(null);
                
                int gridPositionX = symbolWidgets[i].GridPositionX;
                int gridPositionY = symbolWidgets[i].GridPositionY;

                this.symbolWidgets[gridPositionX, gridPositionY] = symbolWidgets[i];
            }
        }
    }
}
