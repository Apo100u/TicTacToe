using System;
using UnityEngine;

namespace TicTacToe.ScriptableObjects.HelperStructs
{
    [Serializable]
    public struct Balance
    {
        [field: Header("Balance")]
        [field: SerializeField] public float TurnTime { get; private set; }
    }
}
