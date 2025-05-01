using Units.Player;
using UnityEngine;

namespace Perks
{
    [CreateAssetMenu(menuName = "Perks/AddMaxHealth")]
    public class AddMaxHealthPerk:Perk
    {
        public int amount;
        public override void Apply(GameObject player)
        {
            player.GetComponent<PlayerHealth>().AddMaxHealth(amount);
        }
    }
}