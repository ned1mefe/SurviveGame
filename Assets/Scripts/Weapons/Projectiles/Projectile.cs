
using Pooling;
using UnityEngine;

namespace Weapons.Projectiles
{
    public abstract class Projectile : MonoBehaviour, IPoolable
    {
        [SerializeField] private int damage;
        [SerializeField] private float range;
        [SerializeField] private int speed;
        private float _startTime;
        private Rigidbody2D _rb;
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        private void FixedUpdate()
        {
            CheckRange();
        }

        private void CheckRange()
        {
            float elapsedTime = Time.time - _startTime;
            float traveledDistance = speed * elapsedTime;

            if (traveledDistance >= range)
            {
                PoolManager.Instance.ReturnToPool(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var damageable = other.GetComponent<Units.IDamageableHostile>();
            if (damageable == null) return;
            
            damageable.TakeDamage(damage);
            PoolManager.Instance.ReturnToPool(gameObject);
        }

        public void OnReturnToPool() { }

        public void OnGetFromPool()
        {
            _startTime = Time.time;
            Vector3 direction = transform.up;
            _rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
        }
    }
}