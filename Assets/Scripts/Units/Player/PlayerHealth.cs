using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Units.Player
{
    public class PlayerHealth : MonoBehaviour, IDamageableFriendly
    {
        private readonly int _maxHealth = 100;
        private int _health;
        private bool _invincible = false;
        private const float InvincibleAfterDamageDuration = 2f;
        private Animator _animator;
        [SerializeField] private Slider healthBar;

        public bool Dead => _health <= 0;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _health = _maxHealth;
        }

        public void TakeDamage(int amount)
        {
            if (Dead || _invincible)
                return;

            if(amount < _health)
                StartCoroutine(MakeInvincible(InvincibleAfterDamageDuration));
            
            _health -= amount;
            healthBar.value = (float)_health / _maxHealth;
            
            if (_animator != null) _animator.SetTrigger(AnimatorHashes.Hit);

            if (Dead) //after the health decrease
                Die();
        }
        
        private void Die()
        {
            if (_animator != null) _animator.SetBool(AnimatorHashes.Dead, true);

            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerWeapon>().enabled = false;
        }
        private IEnumerator MakeInvincible(float duration)
        {
            _invincible = true;
            if (_animator is not null) _animator.SetBool(AnimatorHashes.Invincible,true); 
            
            yield return new WaitForSeconds(duration);
            
            _invincible = false;
            if (_animator is not null) _animator.SetBool(AnimatorHashes.Invincible,false); 
        }
    }
}