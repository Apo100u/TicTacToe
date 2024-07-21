using TicTacToe.Gameplay.MVC.Model;
using UnityEngine;

namespace TicTacToe.Gameplay.MVC.View
{
    public class TicTacToeView : MonoBehaviour
    {
        public (int x, int y) CurrentHintGridPosition { get; private set; }
        
        private SymbolWidget[,] symbolWidgets;
        private bool isShowingHint;

        public void Init(SymbolWidget[] symbolWidgets, int gridSize)
        {
            this.symbolWidgets = new SymbolWidget[gridSize, gridSize];
            
            SetUpSymbolWidgets(symbolWidgets);
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
            if (!isShowingHint)
            {
                ShowSymbol(gridPositionX, gridPositionY, symbol, true);
                isShowingHint = true;
                CurrentHintGridPosition = (gridPositionX, gridPositionY);
            }
        }

        public void ClearHint()
        {
            if (isShowingHint)
            {
                HideSymbol(CurrentHintGridPosition.x, CurrentHintGridPosition.y);
                isShowingHint = false;
            }
        }

        public Symbol? GetDisplayedSymbol(int gridPositionX, int gridPositionY)
        {
            return symbolWidgets[gridPositionX, gridPositionY].DisplayedSymbol;
        }

        private void SetUpSymbolWidgets(SymbolWidget[] symbolWidgets)
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
