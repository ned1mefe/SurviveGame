using UnityEngine;

namespace Units.Enemies
{
    public class Slime : Enemy
    {
        public override void TakeDamage(int amount)
        {
            base.TakeDamage(amount);

            var scale = (maxHealth + ((float)Health * 3 / 2)) / maxHealth;
            transform.localScale = new Vector3(scale,scale,1);
        }

        public override void OnGetFromPool()
        {
            transform.localScale = new Vector3(2.5f,2.5f,1);
            base.OnGetFromPool();
        }
    }
}