using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }

        private int _score = 0;
        private TMP_Text _scoreText;

        private void Awake()
        {
            _scoreText = GetComponent<TMP_Text>();
            if (Instance is null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        public void AddScore(int amount)
        {
            _score += amount;
            _scoreText.text = $"Score: {_score:D6}";
        }
    }
}