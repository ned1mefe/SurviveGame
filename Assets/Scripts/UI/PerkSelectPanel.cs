using System.Collections.Generic;
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
        private Dictionary<Perk,int> _selectedPerks;

        private void Awake()
        {
            _perkButtons = GetComponentsInChildren<PerkButton>();
            _selectedPerks = new();
            foreach (var perk in allPerks)
            {
                _selectedPerks[perk] = 0;
            }
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

        public void RegisterToDictionary(Perk p)
        {
            _selectedPerks[p]++;
        }

        private void GetGiveGunPerks()
        {
            GiveGunPerk[] gunPerks = allPerks.OfType<GiveGunPerk>().ToArray();
            for (int i = 0; i < 3; i++)
            {
                var p = gunPerks[i];
                _perkButtons[i].Setup(p);
                if (_selectedPerks[p] >= p.limit)
                {
                    _perkButtons[i].GetComponent<Button>().interactable = false;
                }
            }
        }
        private void GetRandomPerks()
        {
            var availablePerks = allPerks
                .Where(perk => perk is not GiveGunPerk && _selectedPerks[perk] < perk.limit)
                .ToList();

            for (int i = 0; i < 3; i++)
            {
                if (availablePerks.Count == 0)
                    break;

                var randomPerk = availablePerks[Random.Range(0, availablePerks.Count)];
                _perkButtons[i].Setup(randomPerk);
        
                availablePerks.Remove(randomPerk);
            }
        }
    }
}