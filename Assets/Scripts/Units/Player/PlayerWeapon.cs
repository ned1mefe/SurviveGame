using System;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Units.Player
{
    public class PlayerWeapon : MonoBehaviour
    {
        private Weapon _primaryWeapon;
        private Weapon _secondaryWeapon;
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
        public bool HasWeapon => _primaryWeapon is not null;

        private void Start()
        {
            PickUpWeapon(CreateWeaponFromPrefab(prefabs[1]));
            PickUpWeapon(CreateWeaponFromPrefab(prefabs[2]));

            InputManager.Instance.OnSwitchWeaponPressed += SwapWeapons;
            
            if (HasWeapon)
            {
                WeaponUpdateActions += HandleAim;
                WeaponUpdateActions += HandleShoot;
            }
        }

        public void Update()
        {
            WeaponUpdateActions?.Invoke();
        }
        public void OnDisable()
        {
            if(!HasWeapon)
                return;
            
            _primaryWeapon.gameObject.SetActive(false);
            if (_secondaryWeapon is not null) // 2 guns
            {
                _secondaryWeapon.gameObject.SetActive(false);
            }
            
            WeaponUpdateActions = null;
        }
        
        public void PickUpWeapon(Weapon weapon)
        {
            if (!HasWeapon)
            {
                SetToPrimary(weapon);
                WeaponUpdateActions += HandleAim;
                WeaponUpdateActions += HandleShoot;
                return;
            }
            if (_secondaryWeapon is not null)
            {
                SetToPrimary(weapon);
                return;
            }
            SetToSecondary(weapon);
        }

        private Weapon CreateWeaponFromPrefab(GameObject prefab) // for testing purposes, probably will delete
        {
            GameObject weapon = Instantiate(prefab, transform);
            return weapon.GetComponent<Weapon>();
        }

        private void HandleShoot()
        {
            if (InputManager.Instance.IsShooting)
                _primaryWeapon.TryShoot();
        }
        
        private void HandleAim()
        {
            if (Camera.main is null)
            {
                return;
            }
            
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
        
            Vector3 direction = mousePosition - _primaryWeapon.transform.position;
        
            if ((mousePosition.x < transform.position.x && (transform.localScale.x > 0)) ||
                (mousePosition.x > transform.position.x && (transform.localScale.x < 0)))
            {
                Flip();
            }
        
            _primaryWeapon.transform.right = direction * transform.localScale.x;
        }
        private void SwapWeapons()
        {
            if (_secondaryWeapon is null)
                return;

            var previousPrimary = _primaryWeapon;
            SetToPrimary(_secondaryWeapon);
            SetToSecondary(previousPrimary);
            
            _primaryWeapon.AddDelay(); 

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
            weaponTransform.localScale = PrimaryScale;
            weaponTransform.localPosition = PrimaryPosition;
            weapon.GetComponent<SpriteRenderer>().sortingOrder = PrimarySortOrder;
            _primaryWeapon = weapon;
        }

        private void SetToSecondary(Weapon weapon)
        {
            var weaponTransform = weapon.transform;
            weaponTransform.localRotation = new Quaternion(0,0,SecondaryWeaponRotationZ, SecondaryWeaponRotationW);
            weaponTransform.localScale = SecondaryScale;
            weaponTransform.localPosition = SecondaryPosition;
            weapon.GetComponent<SpriteRenderer>().sortingOrder = SecondarySortOrder;
            _secondaryWeapon = weapon;
        }
    }
}
