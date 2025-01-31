using System;
using UnityEngine;

namespace Weapons.Projectiles
{
    public abstract class Projectile : MonoBehaviour
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

        private void Start()
        {
            _startTime = Time.time;
            Vector3 direction = transform.up;
            _rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
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
                Destroy(gameObject);
            }
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
                Destroy(gameObject); // should be replaced with pooling system later
        }
    }
}