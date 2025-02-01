using JetBrains.Annotations;
using UnityEngine;

namespace Units.Enemies
{
    public abstract class Enemy : MonoBehaviour, IDamageble
    {
        [SerializeField]
        private int maxHealth;
        public int MaxHealth => maxHealth;
        public int Health { get; set; }
        
        [SerializeField]
        private int speed;
        [SerializeField]
        private int damage;

        [CanBeNull] private Animator _animator;
        private static readonly int Hit = Animator.StringToHash("Hit");

        protected void Start()
        {
            Health = maxHealth;
            _animator = GetComponent<Animator>();
        }
        
        public abstract void Step();
        
        public virtual void TakeDamage(int amount)
        {
            if (_animator != null) _animator.SetTrigger(Hit);
            
            Health -= amount;
            
            if (Health <= 0)
                Die();
        }

        private void Die()
        {
            Destroy(gameObject); // Replace with pooling later
        }

    }
}
