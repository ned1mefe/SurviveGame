using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public bool IsShooting { get; private set; }
    public event Action OnSwitchWeaponPressed;
    public event Action OnPausePressed;

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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPausePressed?.Invoke();
        }

        IsShooting = Input.GetMouseButton(0);
    }
}