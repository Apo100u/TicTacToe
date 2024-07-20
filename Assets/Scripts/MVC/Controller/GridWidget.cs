using UnityEngine;

namespace TicTacToe.MVC.Controller
{
    public class GridWidget : MonoBehaviour
    {
        [field: SerializeField] public int GridPositionX { get; private set; }
        [field: SerializeField] public int GridPositionY { get; private set; }
    }
}