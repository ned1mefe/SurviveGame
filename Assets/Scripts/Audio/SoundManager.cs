using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private List<Sound> sfxEntries;
        [SerializeField] private List<Sound> musicEntries;

        private Dictionary<string, AudioClip> _sfxDict;
        private Dictionary<string, AudioClip> _musicDict;

        public static SoundManager Instance;

        private AudioSource _sfxSource;
        private AudioSource _musicSource;
        
        private const string SfxVolumeKey = "SfxVolume";
        private const string MusicVolumeKey = "MusicVolume";
        
        private const float DefaultVolume = 0.7f;

        public float SfxVolume { get; private set; }
        public float MusicVolume { get; private set; }

        private void Awake()
        {
            if(Instance is null)
            {
                Instance = this;
                DontDestroyOnLoad(Instance);
            }
            else
            {
                Destroy(gameObject);
                return;
            }
            
            _sfxSource = GetComponent<AudioSource>();
            _musicSource = gameObject.AddComponent<AudioSource>();
            _musicSource.loop = true;

            _sfxDict = new Dictionary<string, AudioClip>();
            foreach (var entry in sfxEntries)
                _sfxDict[entry.name] = entry.clip;

            _musicDict = new Dictionary<string, AudioClip>();
            foreach (var entry in musicEntries)
                _musicDict[entry.name] = entry.clip;

            LoadVolumes();
        }

        public void PlaySound(string soundName)
        {
            if (_sfxDict != null && _sfxDict.TryGetValue(soundName, out var clip) && clip != null)
            {
                _sfxSource.PlayOneShot(clip, Mathf.Pow(SfxVolume, 2));
            }
            else
            {
                Debug.LogWarning($"SFX '{soundName}' not found.");
            }
        }

        public void PlayMusic(string musicName)
        {
            if (_musicDict != null && _musicDict.TryGetValue(musicName, out var clip) && clip != null)
            {
                _musicSource.clip = clip;
                _musicSource.volume = Mathf.Pow(MusicVolume, 2);
                _musicSource.Play();
            }
            else
            {
                Debug.LogWarning($"Music '{musicName}' not found.");
            }
        }
        
        public void StopMusic()
        {
            _musicSource.Stop();
        }

        public void SetSfxVolume(float volume)
        {
            if(volume is > 1 or < 0)
                Debug.LogWarning($"Volume out of range: {volume}");
            
            SfxVolume = Mathf.Clamp01(volume);
            _sfxSource.volume = Mathf.Pow(SfxVolume, 2);
            
            PlayerPrefs.SetFloat(SfxVolumeKey, SfxVolume);
            PlayerPrefs.Save();
        }

        public void SetMusicVolume(float volume)
        {
            if(volume is > 1 or < 0)
                Debug.LogWarning($"Volume out of range: {volume}");

            MusicVolume = Mathf.Clamp01(volume);
            _musicSource.volume = Mathf.Pow(MusicVolume, 2);
            
            PlayerPrefs.SetFloat(MusicVolumeKey, MusicVolume);
            PlayerPrefs.Save();
        }
        
        private void LoadVolumes()
        {
            SfxVolume = PlayerPrefs.GetFloat(SfxVolumeKey, DefaultVolume);
            MusicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, DefaultVolume);
            
            _sfxSource.volume = Mathf.Pow(SfxVolume, 2);
            _musicSource.volume = Mathf.Pow(MusicVolume, 2);
        }
    }
}