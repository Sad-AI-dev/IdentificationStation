using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultScreen : MonoBehaviour
{
    public void OnReplayClicked()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnReturnToMainMenuClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
