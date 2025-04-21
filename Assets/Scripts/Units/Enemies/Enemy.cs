using System.Collections;
using JetBrains.Annotations;
using Pooling;
using UnityEngine;
using Utils;

namespace Units.Enemies
{
    public abstract class Enemy : MonoBehaviour, IDamageableHostile, IPoolable
    {
        public int Health { get; private set; }
        protected int MaxHealth => maxHealth;
        [SerializeField] private int maxHealth;
        [SerializeField] private float speed;
        [SerializeField] private int damage;
        private Transform Target { get; set; }
        private Rigidbody2D _rb;
        private Collider2D _col;
        [CanBeNull] private Animator _animator;

        protected void Start()
        {
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
            _col = GetComponent<Collider2D>();
        }
        public void SetTarget(Transform target) => Target = target;

        public virtual void Step()
        {
            Chase();
        }

        private void Chase()
        {
            if (Target is null)
                return;
            
            var direction = (Target.position - transform.position).normalized;
            _rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
        }
        
        public virtual void TakeDamage(int amount)
        {
            if (Health < 0) return; 
                
            if (_animator != null) _animator.SetTrigger(AnimatorHashes.Hit);
            
            Health -= amount;
            
            if (Health <= 0)
                StartCoroutine(Die());
        }
        
        protected virtual IEnumerator Die()
        {
            if (_animator != null) _animator.SetBool(AnimatorHashes.Dead, true);

            if (_col is not null)
                _col.enabled = false;
            
            if (_rb is not null)
                _rb.simulated = false;
            
            yield return new WaitForSeconds(2f);
            
            PoolManager.Instance.ReturnToPool(gameObject);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            {
                var damageable = other.gameObject.GetComponent<IDamageableFriendly>();
                if (damageable == null) return;
            
                damageable.TakeDamage(damage);
            }
        }

        public void OnReturnToPool()
        {
            if (_animator is not null) _animator.SetBool(AnimatorHashes.Dead, false);
        }

        public virtual void OnGetFromPool()
        {
            Health = MaxHealth;
            
            if (_col is not null)
                _col.enabled = true;

            if (_rb is not null)
            {
                _rb.simulated = true;
                _rb.velocity = Vector2.zero;
            }
        }
    }
}
