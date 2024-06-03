using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    public event Action<Vector2> OnFireAction;
    public event Action OnInteractAction;
    public Vector2 Dir { get; private set; }
    
    private PlayerInput _playerInput;
    private PlayerInputActions _inputActions;
    private void Awake()
    {
        _inputActions = new();
        _inputActions.Player.Move.performed += Move_performed;
        _inputActions.Player.Move.canceled += Move_canceled;
        _inputActions.Player.Fire.performed += Fire_performed;
        _inputActions.Player.Interact.performed += Interact_performed;
        _inputActions.Enable();

        _playerInput = GetComponent<PlayerInput>();
        _playerInput.onControlsChanged += onControlsChanged;
    }

    private void OnDisable()
    {
        _inputActions.Player.Move.performed -= Move_performed;
        _inputActions.Player.Fire.performed -= Fire_performed;
        _inputActions.Player.Interact.performed -= Interact_performed;
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


    private void Fire_performed(InputAction.CallbackContext obj)
    {
        OnFireAction?.Invoke(Dir);
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
        OnInteractAction?.Invoke();
    }


}
