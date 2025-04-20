using System;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Units.Player
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] private Weapon primaryWeapon;
        [SerializeField] private Weapon secondaryWeapon;
        private event Action WeaponUpdateActions;

        [SerializeField] private List<GameObject> prefabs;
        
        #region Constants

        private const float SecondaryWeaponRotationZ = -0.258819014f;
        private const float SecondaryWeaponRotationW = 0.965925872f;
        private const int PrimarySortOrder = 1;
        private const int SecondarySortOrder = 3;
        private static readonly Vector3 PrimaryScale = new (0.9f, 0.9f, 1);
        private static readonly Vector3 PrimaryPosition = new (0.3f,-0.1f,0);
        private static readonly Vector3 SecondaryScale = new (0.75f,0.75f,1);
        private static readonly Vector3 SecondaryPosition = new Vector3(-0.2f,-0.28f,0);
    
        #endregion
        public bool HasWeapon => primaryWeapon is not null;

        private void Start()
        {
            PickUpWeapon(CreateWeaponFromPrefab(prefabs[1]));
            //PickUpWeapon(CreateWeaponFromPrefab(prefabs[2]));

            if (HasWeapon)
            {
                WeaponUpdateActions += HandleAim;
                WeaponUpdateActions += HandleWeaponSwitch;
                WeaponUpdateActions += HandleShoot;
            }
        }

        public void Update()
        {
            WeaponUpdateActions?.Invoke();
        }
        public void DropWeapon(bool both = false) // drops the primary gun
        {
            if(!HasWeapon)
                return;
            if (secondaryWeapon is not null) // 2 guns
            {
                SwapWeapons();
                Destroy(secondaryWeapon.gameObject);
                if (both)
                {
                    WeaponUpdateActions = null;
                    Destroy(primaryWeapon.gameObject);
                }
            }
            else // 1 gun
            {
                Destroy(primaryWeapon.gameObject);
                WeaponUpdateActions = null;
            }
        }

        public void PickUpWeapon(Weapon rangedWeapon)
        {
            if (!HasWeapon)
            {
                SetToPrimary(rangedWeapon);
                WeaponUpdateActions += HandleAim;
                WeaponUpdateActions += HandleWeaponSwitch;
                WeaponUpdateActions += HandleShoot;
                return;
            }
            if (secondaryWeapon is not null)
            {
                SetToPrimary(rangedWeapon);
                return;
            }
            SetToSecondary(rangedWeapon);
        }

        private Weapon CreateWeaponFromPrefab(GameObject prefab) // for testing purposes, probably will delete
        {
            GameObject weapon = Instantiate(prefab, transform);
            return weapon.GetComponent<Weapon>();
        }

        private void HandleShoot()
        {
            if (InputManager.Instance.IsShooting)
                primaryWeapon.TryUse();
        }
        private void HandleWeaponSwitch()
        {
            if (InputManager.Instance.SwitchWeaponPressed) 
            {
                SwapWeapons();
            }
        }
        private void HandleAim()
        {
            if (!HasWeapon || primaryWeapon.OverrideRotation || Camera.main is null )
            {
                return;
            }
                       
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
        
            Vector3 direction = mousePosition - primaryWeapon.transform.position;
        
            if ((mousePosition.x < transform.position.x && (transform.localScale.x > 0)) ||
                (mousePosition.x > transform.position.x && (transform.localScale.x < 0)))
            {
                Flip();
            }
        
            primaryWeapon.transform.right = direction * transform.localScale.x;
        }
        private void SwapWeapons()
        {
            if (secondaryWeapon is null)
                return;

            var previousPrimary = primaryWeapon;
            SetToPrimary(secondaryWeapon);
            SetToSecondary(previousPrimary);

        }
        private void Flip()
        {
            var tr = transform;
            Vector3 charScale = tr.localScale;
            tr.localScale = new Vector3(-charScale.x, charScale.y, charScale.z);
        }
        private void SetToPrimary(Weapon weapon)
        {
            var weaponTransform = weapon.transform;
            //weaponTransform.localScale = PrimaryScale;
            weaponTransform.localPosition = PrimaryPosition;
            weapon.GetComponent<SpriteRenderer>().sortingOrder = PrimarySortOrder;
            primaryWeapon = weapon;
        }
        private void SetToSecondary(Weapon weapon)
        {
            var weaponTransform = weapon.transform;
            weaponTransform.localRotation = new Quaternion(0,0,SecondaryWeaponRotationZ, SecondaryWeaponRotationW);
            weaponTransform.localScale = SecondaryScale;
            weaponTransform.localPosition = SecondaryPosition;
            weapon.GetComponent<SpriteRenderer>().sortingOrder = SecondarySortOrder;
            secondaryWeapon = weapon;
        }
    }
}
