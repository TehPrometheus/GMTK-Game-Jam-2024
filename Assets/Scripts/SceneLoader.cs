using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void HandlePlayButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("testScene");
    }
    public void HandleOptionsButton()
    {

    }

    public void HandleTutorialButton()
    {
        SceneManager.LoadScene("TutorialScene");
    }
    public void HandleQuitButton()
    {
        Application.Quit();
    }

    public void HandleMainMenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenuScene");
    }
}
