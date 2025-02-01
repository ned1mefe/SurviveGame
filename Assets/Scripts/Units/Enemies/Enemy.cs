using System.Collections;
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
        private float speed;
        [SerializeField]
        private int damage;

        [CanBeNull] private Animator _animator;
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Dead = Animator.StringToHash("Dead");

        protected void Start()
        {
            Health = maxHealth;
            _animator = GetComponent<Animator>();
        }
        
        public abstract void Step();
        
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
