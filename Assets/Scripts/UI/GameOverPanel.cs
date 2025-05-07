using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class GameOverPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text highScoreText;

        private void Start()
        {
            var scoreManager = ScoreManager.Instance;
            scoreManager.SetHighScore();
            highScoreText.text = $"High Score: {scoreManager.GetHighScore():D6}";
            scoreText.text = $"Score:      {scoreManager.GetScore():D6}";
        }

        public void MenuButtonPressed()
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene("Scenes/Main Menu");
        }
    }
}