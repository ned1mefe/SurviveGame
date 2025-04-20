using UnityEngine;

namespace Weapons.Ranged
{
    public abstract class RangedWeapon : Weapon
    {
        [SerializeField] private float spread;
        [SerializeField] private Transform firePoint;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] protected int bulletsPerShot;

        protected override void Use() => Shoot();
        
        private void Shoot()
        {
            for (int i = 0; i < bulletsPerShot; i++)
            {
                InstantiateBullet();
            }
        }
        private void Start()
        {
            firePoint.Rotate(projectilePrefab.transform.rotation.eulerAngles); //Bullets are vertical in their sprites
        }
        private void InstantiateBullet()
        {
            if (projectilePrefab != null && firePoint != null)
            {
                var angleOffset = Random.Range(-spread / 2, spread / 2);
                Instantiate(projectilePrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0,0,angleOffset));
            }
        }
    }
}
