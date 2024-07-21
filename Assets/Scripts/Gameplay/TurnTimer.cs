using System;
using TicTacToe.UI;
using UnityEngine;

namespace TicTacToe.Gameplay
{
    public class TurnTimer : MonoBehaviour
    {
        public event EventHandler TimeEnded;

        [SerializeField] private TurnTimerWidget widget;

        private float timeLeft;
        private bool isStopped;

        public void StartCountdown()
        {
            isStopped = false;
            timeLeft = Gameplay.GameSettings.TurnTime;
        }

        public void Stop()
        {
            isStopped = true;
        }

        private void Update()
        {
            if (!isStopped && timeLeft > 0.0f)
            {
                timeLeft -= Time.deltaTime;
                widget.UpdateTimePercentage(timeLeft / Gameplay.GameSettings.TurnTime);

                if (timeLeft <= 0.0f)
                {
                    TimeEnded?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
