using System.Collections;

namespace Units.Enemies
{
    public class GreenZombie : Enemy
    {
        protected override IEnumerator Die()
        {
            transform.GetChild(0).gameObject.SetActive(false); // destroy rake before dead animation
            return base.Die();
        }
        public override void OnGetFromPool()
        {
            base.OnGetFromPool();
            
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}