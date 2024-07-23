using TicTacToe.ScriptableObjects;
using UnityEngine;

namespace TicTacToe.UnitTests
{
    public abstract class UnitTest : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public string StartingSceneName { get; private set; }

        [SerializeField] protected GameSettings gameSettingsToUse;
        
        public abstract void Arrange();
        public abstract void Act();
        public abstract bool Assert();

        protected void LogFailDetails(string message)
        {
            Debug.Log($"<color=red>Unit test [{Name}] fail details:</color> {message}");
        }
    }
}
