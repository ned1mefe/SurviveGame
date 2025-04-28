using UnityEngine;

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

        public void Toggle()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}
