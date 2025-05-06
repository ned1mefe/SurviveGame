using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {

        [SerializeField] private PausePanel pausePanel;
        [SerializeField] private PerkSelectPanel perkSelectPanel;
        [SerializeField] private OptionsPanel optionsPanel;

        private bool ShouldPause => pausePanel.isActiveAndEnabled || perkSelectPanel.isActiveAndEnabled;

        public static UIManager Instance;
        void Start()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
            
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
            Time.timeScale = ShouldPause ? 0 : 1;
        }

        public void OpenOptionsPanel()
        {
            optionsPanel.gameObject.SetActive(true);
        }


    }
}
