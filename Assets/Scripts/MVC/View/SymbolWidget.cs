using System;
using TicTacToe.MVC.Controller;
using TicTacToe.MVC.Model;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe.MVC.View
{
    public class SymbolWidget : GridWidget
    {
        [SerializeField] private Image displayImage;

        private TicTacToeView view;
        private Symbol? currentlyDisplayedSymbol;

        public void Init(TicTacToeView view)
        {
            this.view = view;
        }
        
        public void ChangeSymbol(Symbol? symbol)
        {
            currentlyDisplayedSymbol = symbol;

            displayImage.sprite = symbol switch
            {
                Symbol.X => view.GameSettings.SpriteForSymbolX,
                Symbol.O => view.GameSettings.SpriteForSymbolO,
                null => null,
                _ => throw new ArgumentOutOfRangeException(nameof(symbol), symbol, null)
            };

            if (symbol == null)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
