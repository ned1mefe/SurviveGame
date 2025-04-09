using UnityEngine;
using Utils;

namespace Units.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        private Animator _animator;
        private Rigidbody2D _rigidBody;
        private WeaponManager _weaponManager;

        private void Start()
        {
            _weaponManager = GetComponent<WeaponManager>();
            _animator = GetComponent<Animator>();
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
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
        
            _animator.SetFloat(AnimatorHashes.Speed,Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            _rigidBody.velocity = new Vector2(horizontal, vertical) * speed; 
        }
    }
}
