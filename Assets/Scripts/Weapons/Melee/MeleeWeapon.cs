using Units;
using Units.Enemies;
using UnityEngine;

namespace Weapons.Melee
{
    public abstract class MeleeWeapon : Weapon
    {
        [SerializeField] private Collider2D hitbox;
        [SerializeField] private int damage;

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (!hitbox.enabled) return;

            var enemy = other.GetComponent<IDamageableHostile>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }

        protected void EnableHitbox() => hitbox.enabled = true;
        protected void DisableHitbox() => hitbox.enabled = false;
    }
    
}