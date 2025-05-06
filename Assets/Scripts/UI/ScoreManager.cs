using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }

        private int _score = 0;
        public int playerLevel = 1;
        private int _levelUpAt = 150;
        private int NextLevelUpAt => _levelUpAt + 300 + playerLevel * 100;
        private TMP_Text _scoreText;

        private void Awake()
        {
            _scoreText = GetComponent<TMP_Text>();
            if (Instance is null)
            {
                Instance = this;
            }
            else
                Destroy(gameObject);
        }

        public void AddScore(int amount)
        {
            _score += amount;
            _scoreText.text = $"Score: {_score:D6}";

            if (_score > _levelUpAt)
                StartCoroutine(LevelUp());
        }

        private IEnumerator LevelUp()
        {
            _levelUpAt = NextLevelUpAt;
            playerLevel++;

            yield return new WaitForSeconds(0.1f);

            UIManager.Instance.OpenPerkSelectPanel();
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
    }
}