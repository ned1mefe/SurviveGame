using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class PausePanel : MonoBehaviour
    {
        public void ContinueButtonPressed()
        {
            gameObject.SetActive(false);
        }
        public void QuitButtonPressed()
        {
            SceneManager.LoadScene("Scenes/Main Menu");
        }
    }
}
