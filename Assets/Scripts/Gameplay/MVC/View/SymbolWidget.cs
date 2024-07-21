using System;
using TicTacToe.Gameplay.MVC.Controller;
using TicTacToe.Gameplay.MVC.Model;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe.Gameplay.MVC.View
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
                Symbol.X => Gameplay.GameSettings.SpriteForSymbolX,
                Symbol.O => Gameplay.GameSettings.SpriteForSymbolO,
                null => null,
                _ => throw new ArgumentOutOfRangeException(nameof(symbol), symbol, null)
            };

            gameObject.SetActive(symbol != null);
        }

        public void SetAsHint(bool isHint)
        {
            Color imageColor = displayImage.color;

            imageColor.a = isHint
                ? 0.5f
                : 1f;

            displayImage.color = imageColor;
        }
    }
}
