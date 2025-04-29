using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuPanel : MonoBehaviour
{
    public void StartButtonPressed()
    {
        SceneManager.LoadScene("Scenes/Game Scene");
    }
    public void QuitButtonPressed()
    {
        Application.Quit();
    }
}
