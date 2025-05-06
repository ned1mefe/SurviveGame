using Audio;
using UnityEngine;
using Utils;

namespace Units.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        private Animator _animator;
        private Rigidbody2D _rigidBody;
        private PlayerWeapon _playerWeapon;
        private Vector2 _movement;

        private void Start()
        {
            _playerWeapon = GetComponent<PlayerWeapon>();
            _animator = GetComponent<Animator>();
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            _movement.x = Input.GetAxis("Horizontal"); 
            _movement.y = Input.GetAxis("Vertical");
            Vector3 scale = transform.localScale;
        
            if (!_playerWeapon.HasWeapon)
            {
                if (_movement.x * scale.x < 0) // means should flip
                {
                    transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
                }
            }

            var mag = _movement.magnitude;
        
            _animator.SetFloat(AnimatorHashes.Speed,mag);
            _rigidBody.velocity = mag <= 1 ? _movement * speed : _movement.normalized * speed; 
        }

        public void IncreaseSpeed(int percentage)
        {
            speed += speed * percentage / 100;
        }

        public void RightStep()
        {
            SoundManager.Instance.PlaySound("RightStep");
        }
        public void LeftStep()
        {
            SoundManager.Instance.PlaySound("LeftStep");
        }
        
    }
}
