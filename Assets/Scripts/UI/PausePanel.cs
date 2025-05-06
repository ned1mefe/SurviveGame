using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class PausePanel : MonoBehaviour
    {
        public void ContinueButtonPressed()
        {
            UIManager.Instance.TogglePausePanel();
        }

        public void OptionsButtonPressed()
        {
            UIManager.Instance.OpenOptionsPanel();
        }
        public void QuitButtonPressed()
        {
            SceneManager.LoadScene("Scenes/Main Menu");
        }
    }
}
