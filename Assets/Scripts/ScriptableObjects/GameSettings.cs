using UnityEngine;

namespace TicTacToe.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Game Settings", menuName = "Scriptable Objects/Game Settings")]
    public class GameSettings : ScriptableObject
    {
        [field: Header("Visuals")]
        [field: SerializeField] public Sprite SpriteForSymbolX { get; private set; }
        [field: SerializeField] public Sprite SpriteForSymbolO { get; private set; }
    }
}
