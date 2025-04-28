using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class PausePanel : MonoBehaviour
    {
        private void OnDisable()
        {
            Time.timeScale = 1;
        }

        private void OnEnable()
        {
            Time.timeScale = 0;
        }

        public void ContinueButtonPressed()
        {
            gameObject.SetActive(false);
        }
        public void QuitButtonPressed()
        {
            SceneManager.LoadScene("Scenes/Main Menu");
        }

        public void Toggle()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}
