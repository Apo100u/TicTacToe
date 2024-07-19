using TicTacToe.MVC.Model;
using UnityEngine;

namespace TicTacToe.MVC.View
{
    public class SymbolWidget : MonoBehaviour
    {
        [field: SerializeField] public int GridPositionX { get; private set; }
        [field: SerializeField] public int GridPositionY { get; private set; }

        public void ChangeSymbol(Symbol? symbol)
        {
            
        }
    }
}