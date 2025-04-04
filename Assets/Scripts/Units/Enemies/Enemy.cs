using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace Units.Enemies
{
    public abstract class Enemy : MonoBehaviour, IDamageable
    {
        public int Health { get; private set; }
        protected int MaxHealth => maxHealth;
        [SerializeField] private int maxHealth;
        [SerializeField] private float speed;
        [SerializeField] private int damage;
        private Transform Target { get; set; }
        private Rigidbody2D _rb;
        [CanBeNull] private Animator _animator;
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Dead = Animator.StringToHash("Dead");

        public void SetTarget(Transform target) => Target = target;
        protected void Start()
        {
            Health = maxHealth;
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
        }

        public virtual void Step()
        {
            Chase();
        }

        private void Chase()
        {
            var direction = (Target.position - transform.position).normalized;
            _rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
        }
        
        public virtual void TakeDamage(int amount)
        {
            if (Health < 0) return; //already dead, in dead animation
                
            if (_animator != null) _animator.SetTrigger(Hit);
            
            Health -= amount;
            
            if (Health <= 0)
                StartCoroutine(Die());
        }
        
        protected virtual IEnumerator Die()
        {
            if (_animator != null) _animator.SetBool(Dead, true);

            Destroy(GetComponent<Collider2D>());
            Destroy(GetComponent<Rigidbody2D>());
            
            yield return new WaitForSeconds(2f);
            
            Destroy(gameObject); // Replace with pooling later
        }

        
        
    }
}
