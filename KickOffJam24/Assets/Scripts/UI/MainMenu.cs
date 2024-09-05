using DevKit;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string environmentSceneName;
    public string gameSceneName;

    public void OnStartClicked()
    {
        SceneManager.LoadSceneAsync(environmentSceneName);
        SceneManager.LoadSceneAsync(gameSceneName, LoadSceneMode.Additive);
        AudioManager.instance.PlayOneShot("Blue");
    }

    public void OnHowToPlayClicked()
    {
        AudioManager.instance.PlayOneShot("Blue");
    }

    public void OnQuitClicked()
    {
        AudioManager.instance.PlayOneShot("Red");
        Application.Quit();
    }
}
