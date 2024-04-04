using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    PlayerInput playerInput;

    public delegate void TapEvent();
    public static event TapEvent OnTapEvent;

    private void OnEnable()
    {
        if (playerInput == null)
        {
            playerInput = new PlayerInput();

            playerInput.PlayerAction.TouchPosition.performed += i =>
            PlayerManager.Instance.touchPoint = i.ReadValue<Vector2>();

            playerInput.PlayerAction.Tap.performed += i => OnTapEvent?.Invoke();
            
        }

        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }
}
