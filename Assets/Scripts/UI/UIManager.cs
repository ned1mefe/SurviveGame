using System;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {

        [SerializeField] private PausePanel pausePanel;
        [SerializeField] private PerkSelectPanel perkSelectPanel;
        [SerializeField] private OptionsPanel optionsPanel;
        [SerializeField] private GameOverPanel gameOverPanel;

        public event Action OnGamePause;
        public event Action OnGameContinue;
        private bool ShouldPause => pausePanel.isActiveAndEnabled || perkSelectPanel.isActiveAndEnabled || gameOverPanel.isActiveAndEnabled;

        public static UIManager Instance;
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            InputManager.Instance.OnPausePressed += TogglePausePanel;
            OpenPerkSelectPanel();
        }


        public void TogglePausePanel()
        {
            pausePanel.transform.parent.gameObject.SetActive(!pausePanel.isActiveAndEnabled); // because of the blur
            CheckTimeScale();
        }

        public void OpenPerkSelectPanel()
        {
            perkSelectPanel.gameObject.SetActive(true);
            CheckTimeScale();
        }

        public void ClosePerkSelectPanel()
        {
            perkSelectPanel.gameObject.SetActive(false);
            CheckTimeScale();
        }
        
        private void CheckTimeScale()
        {
            var prevTime = Time.timeScale;
            Time.timeScale = ShouldPause ? 0 : 1;
            
            if(prevTime > Time.timeScale)
                OnGamePause?.Invoke();
            
            if(prevTime < Time.timeScale)
                OnGameContinue?.Invoke();
        }

        public void OpenOptionsPanel()
        {
            optionsPanel.gameObject.SetActive(true);
        }

        public void OpenGameOverPanel()
        {
            gameOverPanel.gameObject.SetActive(true);
            CheckTimeScale();
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
