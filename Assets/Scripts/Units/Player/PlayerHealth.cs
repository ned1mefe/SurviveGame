using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Units.Player
{
    public class PlayerHealth : MonoBehaviour, IDamageableFriendly
    {
        private int _maxHealth = 100;
        private int _health;
        private int Health
        {
            get => _health;
            set
            {
                _health = value;
                healthBar.value = (float)_health / _maxHealth;
            }
        }
        private bool _invincible = false;
        private const float InvincibleAfterDamageDuration = 2f;
        private Animator _animator;
        [SerializeField] private Slider healthBar;

        private bool Dead => _health <= 0;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            Health = _maxHealth;
        }

        public void TakeDamage(int amount)
        {
            if (Dead || _invincible)
                return;

            if(amount < Health)
                StartCoroutine(MakeInvincible(InvincibleAfterDamageDuration));
            
            Health -= amount;
            
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
            
            UIManager.Instance.OpenGameOverPanel();
        }
        private IEnumerator MakeInvincible(float duration)
        {
            _invincible = true;
            if (_animator is not null) _animator.SetBool(AnimatorHashes.Invincible,true); 
            
            yield return new WaitForSeconds(duration);
            
            _invincible = false;
            if (_animator is not null) _animator.SetBool(AnimatorHashes.Invincible,false); 
        }

        public void Heal(int healPercent)
        {
            Health += _maxHealth * healPercent / 100;
        }

        public void AddMaxHealth(int amount)
        {
            _maxHealth += amount;
            Health += amount;
            var hb = healthBar.GetComponent<RectTransform>();

            float currentWidth = hb.sizeDelta.x;
            float widthPerHealth = currentWidth / (_maxHealth - amount); 
            float newWidth = currentWidth + (amount * widthPerHealth);

            hb.sizeDelta = new Vector2(newWidth, hb.sizeDelta.y);
        }
        
    }
}