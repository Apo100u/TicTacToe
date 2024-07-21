using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe.UI
{
    public class TurnTimerWidget : MonoBehaviour
    {
        [SerializeField] private Slider timeSlider;

        public void UpdateTimePercentage(float currentTime)
        {
            timeSlider.value = currentTime;
        }
    }
}