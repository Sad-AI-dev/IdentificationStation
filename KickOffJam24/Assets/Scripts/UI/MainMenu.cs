using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string sceneName;

    public void OnStartClicked()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
