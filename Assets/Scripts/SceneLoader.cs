using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void HandlePlayButton()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void HandleOptionsButton()
    {

    }
    public void HandleQuitButton()
    {
        Application.Quit();
    }
}
