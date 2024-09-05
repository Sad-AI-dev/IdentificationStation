using DevKit;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultScreen : MonoBehaviour
{
    public void OnReplayClicked()
    {
        AudioManager.instance.PlayOneShot("Blue");
        SceneManager.LoadSceneAsync("Environment");
        SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);
    }

    public void OnReturnToMainMenuClicked()
    {
        AudioManager.instance.PlayOneShot("Blue");
        SceneManager.LoadScene("MainMenu");
    }
}
