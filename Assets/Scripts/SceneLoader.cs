using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void HandlePlayButton()
    {
        SceneManager.LoadScene("Level1Scene");
    }
    public void HandleOptionsButton()
    {

    }
    public void HandleQuitButton()
    {
        Application.Quit();
    }

    public void HandleMainMenuButton()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
