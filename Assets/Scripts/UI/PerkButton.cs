using Perks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PerkButton : MonoBehaviour
    {
        private Image _iconImage;
        private TextMeshProUGUI _nameText;
        private TextMeshProUGUI _descriptionText;
        private GameObject _player;
        private PerkSelectPanel _perkSelectPanel;

        private Perk _assignedPerk;
    
        private void Awake()
        {
            if (_player is null)
                Initialize();
            _perkSelectPanel = GetComponentInParent<PerkSelectPanel>();
        }
        private void Initialize()
        {
            _player = GameObject.FindWithTag("Player");
            if (_player is null)
            {
                Debug.LogError("Could not find player.");
                return;
            }
            _iconImage = transform.Find("Image").GetComponent<Image>();
            _nameText = transform.Find("Name").GetComponent<TextMeshProUGUI>();
            _descriptionText = transform.Find("Description").GetComponent<TextMeshProUGUI>();
        }
        public void Setup(Perk perk)
        {
            if (_player is null)
                Initialize();

            _assignedPerk = perk;
            _iconImage.sprite = perk.icon;
            _nameText.text = perk.perkName;
            _descriptionText.text = perk.description;
        }

        public void OnClick()
        {
            _assignedPerk.Apply(_player);
            _perkSelectPanel.RegisterToDictionary(_assignedPerk);
            
            UIManager.Instance.ClosePerkSelectPanel();
        }
    }
}