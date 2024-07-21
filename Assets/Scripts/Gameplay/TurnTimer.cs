using System;
using UnityEngine;

namespace TicTacToe.Gameplay
{
    public class TurnTimer : MonoBehaviour
    {
        public event EventHandler TimeEnded;

        private float timeLeft;

        public void StartCountdown()
        {
            timeLeft = Gameplay.GameSettings.TurnTime;
        }

        public void Stop()
        {
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (timeLeft > 0.0f)
            {
                timeLeft -= Time.deltaTime;

                if (timeLeft <= 0.0f)
                {
                    TimeEnded?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
