using TicTacToe.ScriptableObjects.HelperStructs;
using UnityEngine;

namespace TicTacToe.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Game Settings", menuName = "Scriptable Objects/Game Settings")]
    public class GameSettings : ScriptableObject
    {
        [field: SerializeField] public Balance Balance;
        [field: SerializeField] public Visuals Visuals;
    }
}
