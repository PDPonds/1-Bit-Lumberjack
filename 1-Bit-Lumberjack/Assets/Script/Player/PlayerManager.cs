using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerManager : Singleton<PlayerManager>
{
    public LayerMask UIMask;
    [HideInInspector] public Vector2 touchPoint;

    private void OnEnable()
    {
        InputSystem.OnTapEvent += Tap;
    }

    private void OnDisable()
    {
        InputSystem.OnTapEvent -= Tap;
    }

    public void Tap()
    {
        Attack();
    }

    public void Attack()
    {
        Debug.Log("Attack");
    }

}
