using System;
using TicTacToe.Gameplay.MVC.Controller;
using TicTacToe.Gameplay.MVC.Model;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe.Gameplay.MVC.View
{
    public class SymbolWidget : GridWidget
    {
        private const float HintAlpha = 0.5f;
        private const float DefaultAlpha = 1f;
        
        public Symbol? DisplayedSymbol { get; private set; }

        [SerializeField] private Image displayImage;

        public void ChangeSymbol(Symbol? symbol)
        {
            DisplayedSymbol = symbol;

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
                ? HintAlpha
                : DefaultAlpha;

            displayImage.color = imageColor;
        }
    }
}
