using UnityEngine;
using Weapons;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Weapon primaryWeapon;
    [SerializeField] private Weapon secondaryWeapon;
    
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
    private void Update()
    {
        if (HasWeapon)
        {
            HandleAim();
            HandleWeaponSwitch();
            HandleShoot();
        }
    }

    private void HandleShoot()
    {
        if (InputManager.Instance.IsShooting)
            primaryWeapon.TryShoot();
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
        if (Camera.main is null)
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
        
        (primaryWeapon, secondaryWeapon) = (secondaryWeapon, primaryWeapon); //Swaps
        
        primaryWeapon.GetComponent<SpriteRenderer>().sortingOrder = PrimarySortOrder;
        secondaryWeapon.GetComponent<SpriteRenderer>().sortingOrder = SecondarySortOrder;
        
        var primaryTransform = primaryWeapon.transform;
        var secondaryTransform = secondaryWeapon.transform;
        
        // default secondaryWeapon rotation, should be different if looking left
        secondaryTransform.rotation = new Quaternion(0,0,SecondaryWeaponRotationZ * transform.localScale.x, SecondaryWeaponRotationW); 
        
        primaryTransform.localScale = PrimaryScale;
        secondaryTransform.localScale = SecondaryScale;

        primaryTransform.localPosition = PrimaryPosition;
        secondaryTransform.localPosition = SecondaryPosition;
    }
    private void Flip()
    {
        Vector3 charScale = transform.localScale;
        transform.localScale = new Vector3(-charScale.x, charScale.y, charScale.z);
    }
}
