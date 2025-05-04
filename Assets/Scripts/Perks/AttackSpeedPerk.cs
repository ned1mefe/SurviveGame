using Units.Player;
using UnityEngine;

namespace Perks
{
    [CreateAssetMenu(menuName = "Perks/AttackSpeed")]
    public class AttackSpeedPerk : Perk
    {
        public int percentage;
        public override void Apply(GameObject player)
        {
            player.GetComponent<PlayerWeapon>().IncreaseAllWeaponsAttackSpeed(percentage);
        }
    }
}