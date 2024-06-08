using System;
using MoreMountains.Tools;
using Rayqdr.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    public static event Action OnPauseButtonPressed;
    public event Action<Vector2> OnFireAction;
    public event Action OnInteractAction;
    public event Action OnDash;
    public Vector2 Dir { get; private set; }
    public Vector2 LastValidDir {
        get
        {
            if (Mathf.Abs(Dir.x) > .1f || Mathf.Abs(Dir.y) > .1f)
            {
                return Dir;
            }
            else
                return Vector2.right;
        }
            }
    
    private PlayerInput _playerInput;
    private PlayerInputActions _inputActions;

    private bool _isDisable;
    private bool _isFiring = false;
    private void Awake()
    {
        _inputActions = new();
        _inputActions.Player.Move.performed += Move_performed;
        _inputActions.Player.Move.canceled += Move_canceled;
        _inputActions.Player.Fire.performed += Fire_performed;
        _inputActions.Player.Fire.canceled += Fire_canceled;
        _inputActions.Player.Interact.performed += Interact_performed;
        _inputActions.Player.PauseButton.performed += PauseButton_performed;
        _inputActions.Enable();

        _playerInput = GetComponent<PlayerInput>();
        _playerInput.onControlsChanged += onControlsChanged;


    }

    private void Fire_canceled(InputAction.CallbackContext obj)
    {
        _isFiring = false;
    }

    private void PauseButton_performed(InputAction.CallbackContext obj)
    {
        OnPauseButtonPressed?.Invoke();
    }

    public void DisableInputs(bool disable)
    {
        _isDisable = disable;
    }

    private void OnDisable()
    {
        _inputActions.Player.Move.performed -= Move_performed;
        _inputActions.Player.Move.canceled -= Move_canceled;
        _inputActions.Player.Fire.performed -= Fire_performed;
        _inputActions.Player.Fire.canceled -= Fire_canceled;
        _inputActions.Player.Interact.performed -= Interact_performed;
        _inputActions.Player.PauseButton.performed -= PauseButton_performed;
        _inputActions.Player.Dash.performed -= Dash_performed;
        _inputActions.Disable();

        _playerInput.onControlsChanged -= onControlsChanged;
    }

    private void onControlsChanged(PlayerInput obj)
    {
        //Change the aiming behaviour wether the player has a mouse and keyboard or the player uses a gamepad
        if (obj.currentControlScheme == "Keyboard&Mouse")
            Debug.Log("Device changed to keyboard"); //Use aim on mouse
        else
            Debug.Log("Device changed to gamepad"); // Use twin stick shooter 

    }

    private void FixedUpdate()
    {
        if (!_isFiring || _isDisable) return;
        Vector2 launchDirection = Rayqdr.Utils.UtilsClass.GetDirToMouse(this.transform.position);
        OnFireAction?.Invoke(launchDirection);
    }

    private void Fire_performed(InputAction.CallbackContext obj)
    {
        _isFiring = true;
    }


    private void Move_performed(InputAction.CallbackContext obj)
    {
        Dir = _inputActions.Player.Move.ReadValue<Vector2>();
    }

    private void Move_canceled(InputAction.CallbackContext obj)
    {
        Dir = Vector2.zero;
    }
    private void Interact_performed(InputAction.CallbackContext obj)
    {
        if(_isDisable) return;
        OnInteractAction?.Invoke();
    }

    public void UnlockDash()
    {
        _inputActions.Player.Dash.performed += Dash_performed;
    }

    private void Dash_performed(InputAction.CallbackContext obj)
    {
        OnDash?.Invoke();
    }
}
