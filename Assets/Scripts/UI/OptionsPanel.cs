using Audio;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class OptionsPanel : MonoBehaviour
    {
        [SerializeField] private Slider sfxSlider;
        [SerializeField] private Slider musicSlider;
        private void Start()
        {
            sfxSlider.value = SoundManager.Instance.SfxVolume;
            musicSlider.value = SoundManager.Instance.MusicVolume;
        }

        public void BackButtonPressed()
        {
            gameObject.SetActive(false);
        }

        public void OnSFXValueChange(float val)
        {
            SoundManager.Instance.SetSfxVolume(val);
        }
        
        public void OnMusicValueChange(float val)
        {
            SoundManager.Instance.SetMusicVolume(val);
        }
    }
}
