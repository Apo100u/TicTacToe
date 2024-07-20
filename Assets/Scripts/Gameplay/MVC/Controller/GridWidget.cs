using UnityEngine;

namespace TicTacToe.Gameplay.MVC.Controller
{
    public class GridWidget : MonoBehaviour
    {
        [field: SerializeField] public int GridPositionX { get; private set; }
        [field: SerializeField] public int GridPositionY { get; private set; }
    }
}