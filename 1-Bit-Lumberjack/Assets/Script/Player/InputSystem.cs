using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    PlayerInput playerInput;
    private void OnEnable()
    {
        if (playerInput == null)
        {
            playerInput = new PlayerInput();

            playerInput.PlayerAction.Touch.performed += i => DebugText();

            playerInput.PlayerAction.TouchPosition.performed += i =>
            PlayerManager.Instance.touchPoint = i.ReadValue<Vector2>();
        }

        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    void DebugText()
    {
        Debug.Log("Tab");
    }

}
