using System.Collections;
using Audio;
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
        private string _bulletPoolTag;
        
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
            if (projectilePrefab.TryGetComponent<IPoolable>(out var poolable))
            {
                _bulletPoolTag = poolable.GetPoolTag();
            }
        }

        public void AddDelay()
        {
            _lastFireTime = Time.time - (fireRate * 2 / 3);
        }

        public void IncreaseAttackSpeed(int percent)
        {
            var cdReductionPercent = GetCooldownReductionPercent(percent);
            fireRate -= fireRate * cdReductionPercent / 100;
        }
        
        private float GetCooldownReductionPercent(float asIncreasePercent)
        {
            return 100f * (1f - 1f / (1f + asIncreasePercent / 100f));
        }
        
        private void Shoot()
        {
            SoundManager.Instance.PlaySound(GetType().Name);
            for (int i = 0; i < bulletsPerShot; i++)
            {
                InstantiateBullet();
            }
        }
        private void InstantiateBullet()
        {
            if (projectilePrefab is not null 
                && firePoint is not null)
            {
                var angleOffset = Random.Range(-spread / 2, spread / 2);
                PoolManager.Instance.GetFromPool(_bulletPoolTag, firePoint.position, firePoint.rotation * Quaternion.Euler(0,0,angleOffset));
            }
        }
    }
}
