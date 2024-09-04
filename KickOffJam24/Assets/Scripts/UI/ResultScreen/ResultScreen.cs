using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultScreen : MonoBehaviour
{
    public void OnReplayClicked()
    {
        SceneManager.LoadSceneAsync("Environment");
        SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);
    }

    public void OnReturnToMainMenuClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
