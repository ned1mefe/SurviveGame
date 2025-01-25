using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator animator;
    private static readonly int Speed = Animator.StringToHash("Speed");

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 scale = transform.localScale;
        if (horizontal * scale.x < 0) // means should flip
        {
            transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
        }
        
        animator.SetFloat(Speed,Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        rigidBody.velocity = new Vector2(horizontal, vertical) * speed; 
    }
    
}
