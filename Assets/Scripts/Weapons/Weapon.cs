using Pooling;
using UnityEngine;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private float fireRate;
        [SerializeField] private float spread;
        [SerializeField] private Transform firePoint;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] protected int bulletsPerShot;
        private float _lastFireTime = 0f;

        private void Shoot()
        {
            for (int i = 0; i < bulletsPerShot; i++)
            {
                InstantiateBullet();
            }
        }

        private void InstantiateBullet()
        {
            if (projectilePrefab is not null 
                && firePoint is not null 
                && projectilePrefab.TryGetComponent<IPoolable>(out var poolable))
            {
                var angleOffset = Random.Range(-spread / 2, spread / 2);
                PoolManager.Instance.GetFromPool(poolable.GetPoolTag(), firePoint.position, firePoint.rotation * Quaternion.Euler(0,0,angleOffset));
            }
        }

        public void TryShoot()
        {
            if (!CanShoot) return;
            
            _lastFireTime = Time.time;
            Shoot();
        }

        private bool CanShoot => Time.time >= _lastFireTime + fireRate;

        private void Start()
        {
            firePoint.Rotate(projectilePrefab.transform.rotation.eulerAngles); //Bullets are vertical in their sprites
        }
    }
}
