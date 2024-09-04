using UnityEngine;
using UnityEngine.UI;

public class TimerRadialImage : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private Image image;

    private void Update()
    {
        image.fillAmount = timer.GetCurrentCompletionPercent();
    }
}
