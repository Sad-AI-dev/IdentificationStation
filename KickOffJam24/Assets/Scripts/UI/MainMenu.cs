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
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
