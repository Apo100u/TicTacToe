using TicTacToe.MVC.Model;
using UnityEngine;

namespace TicTacToe.MVC.View
{
    public class TicTacToeView : MonoBehaviour
    {
        private SymbolWidget[,] symbolWidgets;
        
        public void Init(SymbolWidget[] symbolWidgets, int gridSize)
        {
            this.symbolWidgets = new SymbolWidget[gridSize, gridSize];
            
            AssignSymbolWidgets(symbolWidgets);
        }

        private void AssignSymbolWidgets(SymbolWidget[] symbolWidgets)
        {
            for (int i = 0; i < symbolWidgets.Length; i++)
            {
                int gridPositionX = symbolWidgets[i].GridPositionX;
                int gridPositionY = symbolWidgets[i].GridPositionY;

                this.symbolWidgets[gridPositionX, gridPositionY] = symbolWidgets[i];
            }
        }

        public void UpdateSymbolWidget(int gridPositionX, int gridPositionY, Symbol? symbol)
        {
            symbolWidgets[gridPositionX, gridPositionY].ChangeSymbol(symbol);
        }
    }
}