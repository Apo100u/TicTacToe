using TicTacToe.Gameplay.MVC.Model;
using UnityEngine;

namespace TicTacToe.UnitTests
{
    [CreateAssetMenu(fileName = "New Lose Unit Test", menuName = "Scriptable Objects/Unit Test/Lose")]
    public class LoseUnitTest : WinUnitTest
    {
        public override bool Assert()
        {
            return IsGameWinnerNotO();
        }

        private bool IsGameWinnerNotO()
        {
            bool isGameWinnerNotO = gameplay.Winner != Symbol.O;

            if (!isGameWinnerNotO)
            {
                LogFailDetails($"Unsuccessful lose. Tried to create winning combination for X, but the winner was {Symbol.O}.");
            }

            return isGameWinnerNotO;
        }
    }
}