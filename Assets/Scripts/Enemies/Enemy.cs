using Unity.VisualScripting;
using UnityEngine;

namespace Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField]
        private int maxHealth;
        public int MaxHealth => maxHealth;
        public int Health { get; set; }
        
        [SerializeField]
        private int speed;
        [SerializeField]
        private int damage;

        protected Enemy()
        {
            Health = maxHealth;
        }
        
        public abstract void Step();
    }
}
