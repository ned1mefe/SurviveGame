using System.Collections;
using UnityEngine;

namespace Weapons.Melee
{
    public class Scythe : MeleeWeapon
    {
        [SerializeField] private Transform slashPivot;
        [SerializeField] private float slashSpeed = 720f;
        [SerializeField] private float totalAngle = 180f;

        private bool _isSlashing;

        protected override void Use()
        {
            if (!_isSlashing)
                StartCoroutine(SlashRoutine());
        }

        private IEnumerator SlashRoutine()
        {
            _isSlashing = true;
            OverrideRotation = true;
            EnableHitbox();

            float rotated = 0f;
            slashPivot.Rotate(0, 0, totalAngle/2);
            while (rotated < totalAngle)
            {
                float step = slashSpeed * Time.deltaTime;
                slashPivot.Rotate(0, 0, -step);
                rotated += step;
                yield return null;
            }

            _isSlashing = false;
            DisableHitbox();
            OverrideRotation = false;
            slashPivot.localRotation = Quaternion.identity;
        }
    }
}