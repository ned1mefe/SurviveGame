using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator animator;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private WeaponManager _weaponManager;

    private void Start()
    {
        _weaponManager = GetComponent<WeaponManager>();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal"); //Unnecessary to get these from the inputManager 
        float vertical = Input.GetAxis("Vertical");
        Vector3 scale = transform.localScale;
        
        if (!_weaponManager.HasWeapon)
        {
            if (horizontal * scale.x < 0) // means should flip
            {
                transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
            }
        }
        
        animator.SetFloat(Speed,Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        rigidBody.velocity = new Vector2(horizontal, vertical) * speed; 
    }
    
}
