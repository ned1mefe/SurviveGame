using System;
using UnityEngine;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance;
        public bool IsShooting { get; private set; }
        public event Action OnSwitchWeaponPressed;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.Q))
            {
                OnSwitchWeaponPressed?.Invoke();
            }

            IsShooting = Input.GetMouseButton(0);
        }
    }
}