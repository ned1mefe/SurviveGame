using Units.Player;
using UnityEngine;

namespace Perks
{
    [CreateAssetMenu(menuName = "Perks/Heal")]
    public class HealPerk: Perk
    {
        public int healPercentage;
        public override void Apply(GameObject player)
        {
            player.GetComponent<PlayerHealth>().Heal(healPercentage);
        }
    }
}