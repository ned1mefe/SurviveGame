using System.Linq;
using Perks;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI
{
    public class PerkSelectPanel : MonoBehaviour
    {
        public Perk[] allPerks;
        private PerkButton[] _perkButtons;

        private void Awake()
        {
            _perkButtons = GetComponentsInChildren<PerkButton>();
        }
        private void OnEnable()
        {
            if (ScoreManager.Instance.playerLevel <= 2)
            {
                GetGiveGunPerks();
                return;
            }

            if (ScoreManager.Instance.playerLevel == 3)
            {
                foreach (var btn in _perkButtons)
                {
                    btn.GetComponent<Button>().interactable = true;
                }
            }                
            GetRandomPerks();
        }

        private void GetGiveGunPerks()
        {
            GiveGunPerk[] gunPerks = allPerks.OfType<GiveGunPerk>().ToArray();
            for (int i = 0; i < 3; i++)
            {
                _perkButtons[i].Setup(gunPerks[i]);
            }
        }
        private void GetRandomPerks()
        {
            var otherPerks = allPerks.Where(perk => perk is not GiveGunPerk).ToArray();

            for (int i = 0; i < 3; i++)
            {
                _perkButtons[i].Setup(otherPerks[Random.Range(0, otherPerks.Length)]);
            }
        }
    }
}