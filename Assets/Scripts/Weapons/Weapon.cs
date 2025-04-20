using UnityEngine;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private float cooldown;
        private float _lastUseTime = 0f;
        public bool OverrideRotation { get; protected set; } = false;

        private bool CanUse => Time.time >= _lastUseTime + cooldown;

        public void TryUse()
        {
            if (!CanUse) return;
            
            _lastUseTime = Time.time;
            Use();
        }

        protected abstract void Use();
    }
}