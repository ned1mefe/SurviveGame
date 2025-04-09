﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Units.Player
{
    public class PlayerHealth : MonoBehaviour, IDamageableFriendly
    {
        private readonly int _maxHealth = 100;
        private int _health;
        private Animator _animator;
        [SerializeField] private Slider healthBar; 

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _health = _maxHealth;
        }

        public void TakeDamage(int amount)
        {
            _health -= amount;
            healthBar.value = (float)_health / _maxHealth;
            
            if (_animator != null) _animator.SetTrigger(AnimatorHashes.Hit);

            if (_health <= 0)
                StartCoroutine(Die());
        }
        
        private IEnumerator Die()
        {
            if (_animator != null) _animator.SetBool(AnimatorHashes.Dead, true);

            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            GetComponent<PlayerMovement>().enabled = false;
            var weaponManager = GetComponent<WeaponManager>();
            weaponManager.DropWeapon(both: true);
            
            
            yield return new WaitForSeconds(2f);
            
            //Destroy(gameObject); // Replace with pooling later
        }
    }
}