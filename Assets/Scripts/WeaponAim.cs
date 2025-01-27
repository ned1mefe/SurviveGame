using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAim : MonoBehaviour
{
    [SerializeField]
    private Transform character;
    void FixedUpdate()
    {
        if (Camera.main is null)
            return;
        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        
        Vector3 direction = mousePosition - transform.position;
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        if (character != null)
        {
            if ((mousePosition.x < character.position.x && (transform.localScale.x > 0)) ||
                (mousePosition.x > character.position.x && (transform.localScale.x < 0)))
            {
                Flip();
            }
        }
        
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        Vector3 charScale = character.localScale;
        
        transform.localScale = new Vector3(-scale.x, -scale.y, scale.z);
        character.localScale = new Vector3(-charScale.x, charScale.y, charScale.z);
    }
}
