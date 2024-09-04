using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] private float time;

    private float timer;

    private void Start()
    {
        timer = time;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
            HandleTimerComplete();
    }

    private void HandleTimerComplete()
    {
        enabled = false;

        // Load Next Scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public float GetCurrentTimer()
    {
        return timer;
    }

    public float GetCurrentCompletionPercent()
    {
        return timer / time;
    }
}
