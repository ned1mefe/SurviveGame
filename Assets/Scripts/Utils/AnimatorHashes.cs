using UnityEngine;

namespace Utils
{
    public static class AnimatorHashes
    {
        public static readonly int Hit = Animator.StringToHash("Hit");
        public static readonly int Speed = Animator.StringToHash("Speed");
        public static readonly int Dead = Animator.StringToHash("Dead");
        public static readonly int Invincible = Animator.StringToHash("Invincible");
    }
}