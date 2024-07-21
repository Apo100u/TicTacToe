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
                widget.UpdateTimePercentage(timeLeft / Gameplay.GameSettings.TurnTime);

                if (timeLeft <= 0.0f)
                {
                    TimeEnded?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
