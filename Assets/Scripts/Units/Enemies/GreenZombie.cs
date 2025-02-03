using System.Collections;

namespace Units.Enemies
{
    public class GreenZombie : Enemy
    {
        protected override IEnumerator Die()
        {
            Destroy(transform.GetChild(0).gameObject); // destroy rake
            return base.Die();
        }
        
    }
}