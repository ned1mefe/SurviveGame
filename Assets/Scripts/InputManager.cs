using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    private bool _switchWeaponPressed;
    private bool _pickUpPressed;
    public static InputManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject inputManagerObject = new GameObject("InputManager");
                _instance = inputManagerObject.AddComponent<InputManager>();
                DontDestroyOnLoad(inputManagerObject);
            }
            return _instance;
        }
    }

    public bool SwitchWeaponPressed
    {
        get
        {
            var temp = _switchWeaponPressed;
            _switchWeaponPressed = false;
            return temp;  
        } 
        private set => _switchWeaponPressed = value || _switchWeaponPressed;
    }
    public bool PickUpPressed
    {
        get
        {
            var temp = _pickUpPressed;
            _pickUpPressed = false;
            return temp;
        }
        private set => _pickUpPressed = value || _pickUpPressed;
    }
    public bool IsShooting { get; private set; }


    private void Update()
    {
        SwitchWeaponPressed = Input.GetKeyDown(KeyCode.Q);
        PickUpPressed = Input.GetKeyDown(KeyCode.E);
        IsShooting = Input.GetMouseButton(0);
    }
}