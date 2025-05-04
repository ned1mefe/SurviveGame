using Units.Player;
using UnityEngine;

namespace Perks
{
    [CreateAssetMenu(menuName = "Perks/Speed")]
    public class SpeedPerk : Perk
    {
        public int percentage;
        public override void Apply(GameObject player)
        {
            player.GetComponent<PlayerMovement>().IncreaseSpeed(percentage);
        }
    }
}
