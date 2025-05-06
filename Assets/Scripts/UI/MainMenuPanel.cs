using System;
using Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenuPanel : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log(SoundManager.Instance.MusicVolume);
            SoundManager.Instance.PlayMusic("MainMenuTheme");
            Debug.Log(SoundManager.Instance.MusicVolume);
        }

        public void StartButtonPressed()
        {
            SceneManager.LoadScene("Scenes/Game Scene");
            SoundManager.Instance.StopMusic();
        }
        public void QuitButtonPressed()
        {
            Application.Quit();
        }
    }
}
