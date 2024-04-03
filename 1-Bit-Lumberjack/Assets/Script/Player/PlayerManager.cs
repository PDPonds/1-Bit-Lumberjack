using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerManager : Singleton<PlayerManager>
{
    public delegate void PlayerAttackEvent(int amount);
    public event PlayerAttackEvent OnPlayerAttack;

    //Ref
    [HideInInspector] public Animator anim;

    // Input
    [HideInInspector] public Vector2 touchPoint;

    //Variable
    public int curAttackDamage;

    private void OnEnable()
    {
        InputSystem.OnTapEvent += Tap;
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    private void OnDisable()
    {
        InputSystem.OnTapEvent -= Tap;
    }

    public void Tap()
    {
        ParticleManager.Instance.SpawnParticle("TapParticle", GetWorldPosFormTouchPoint3D());
        Attack();
    }

    public void Attack()
    {
        OnPlayerAttack?.Invoke(curAttackDamage);
        anim.Play("Player_Attack");
    }

    public Vector3 GetWorldPosFormTouchPoint3D()
    {
        Vector3 tapWorldPos = Camera.main.ScreenToWorldPoint(touchPoint);
        return tapWorldPos;
    }

}
