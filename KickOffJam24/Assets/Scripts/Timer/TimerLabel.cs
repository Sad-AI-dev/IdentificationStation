using TMPro;
using UnityEngine;

public class TimerLabel : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private TMP_Text label;

    private void Update()
    {
        float currentTimer = timer.GetCurrentTimer();
        int currentTimerSeconds = Mathf.CeilToInt(currentTimer);

        label.text = currentTimerSeconds.ToString();
    }
}
