using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenOptions()
    {
        if (OptionsManager.Instance != null)
            OptionsManager.Instance.OpenOptions();
    }

    public void OpenCredits()
    {
        Application.OpenURL("https://juanclassy.itch.io/no-humans-allowed");
    }
}
