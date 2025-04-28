using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {

        [SerializeField] private PausePanel pausePanel;
        void Start()
        {
            InputManager.Instance.OnPausePressed += pausePanel.Toggle;
        }


    }
}
