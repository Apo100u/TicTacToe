using System;
using UnityEngine;

namespace TicTacToe.ScriptableObjects.HelperStructs
{
    [Serializable]
    public struct Visuals
    {
        [field: SerializeField] public Sprite Background { get; set; }
        [field: SerializeField] public Sprite SymbolX { get; set; }
        [field: SerializeField] public Sprite SymbolO { get; set; }
    }
}
