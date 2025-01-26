using Unity.VisualScripting;
using UnityEngine;

namespace Enemies
{
    public abstract class BaseEnemy : MonoBehaviour
    {
        [SerializeField]
        private int maxHealth;
        public int MaxHealth => maxHealth;
        public int Health { get; set; }
        
        [SerializeField]
        private int speed;
        [SerializeField]
        private int damage;

        protected BaseEnemy()
        {
            Health = maxHealth;
        }
        
        public abstract void Step();
    }
}
