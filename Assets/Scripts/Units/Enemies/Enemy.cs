using System.Collections;
using JetBrains.Annotations;
using Pooling;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Units.Enemies
{
    public abstract class Enemy : MonoBehaviour, IDamageableHostile, IPoolable
    {
        private int _health;
        public int Health
        {
            get => _health;
            private set
            {
                _health = value;
                healthBar.value = (float)_health / maxHealth;
            }
        }

        [SerializeField] protected int maxHealth;
        [SerializeField] private float speed;
        [SerializeField] private int damage;
        private Transform Target { get; set; }
        private Rigidbody2D _rb;
        private Collider2D _col;
        [SerializeField] private Slider healthBar;
        [CanBeNull] private Animator _animator;
        

        protected void Start()
        {
            Health = maxHealth;
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

            healthBar.gameObject.SetActive(false);
            
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
            healthBar.gameObject.SetActive(true);
        }

        public virtual void OnGetFromPool()
        {
            Health = maxHealth;
            
            if (_col is not null)
                _col.enabled = true;

            if (_rb is not null)
            {
                _rb.simulated = true;
                _rb.velocity = Vector2.zero;
            }
        }

        // Used excel to find a fair score value function: 37 40 57 77 107
        public int ScoreValue => (int)((maxHealth + 500) * (speed + 3.5) * (damage + 45) / 5000) ;
        
    }
}
