using System;
using Units.Player;
using UnityEngine;
using Weapons;

namespace Perks
{
    [CreateAssetMenu(menuName = "Perks/GiveGun")]
    public class GiveGunPerk : Perk
    {
        private Weapon CreateWeaponFromPrefab(GameObject prefab) 
        {
            GameObject weapon = Instantiate(prefab);
            return weapon.GetComponent<Weapon>();
        }
        
        public GameObject gunPrefab;

        public override void Apply(GameObject player)
        {
            var weaponManager = player.GetComponent<PlayerWeapon>();
            if (weaponManager is null)
            {
                Debug.LogError("Player does not have a PlayerWeapon script.");
                return;
            }

            weaponManager.PickUpWeapon(CreateWeaponFromPrefab(gunPrefab));
        }
    }
}
