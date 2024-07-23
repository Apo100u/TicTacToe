using System;
using UnityEngine;

namespace TicTacToe.ScriptableObjects.HelperStructs
{
    [Serializable]
    public struct Visuals
    {
        [field: SerializeField] public Sprite Background { get; private set; }
        [field: SerializeField] public Sprite SymbolX { get; private set; }
        [field: SerializeField] public Sprite SymbolO { get; private set; }
    }
}
