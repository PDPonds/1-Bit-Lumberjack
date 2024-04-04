using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerManager : Singleton<PlayerManager>
{
    public delegate void TapEvent();
    public event TapEvent OnPlayerTap;
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

    public void Tap()
    {
        OnPlayerTap?.Invoke();
        Attack();
        TryGetTouchObject();
    }

    void Attack()
    {
        OnPlayerAttack?.Invoke(curAttackDamage);
        anim.Play("Player_Attack");
    }

    void TryGetTouchObject()
    {
        Vector3 worldPos3D = GetWorldPosFormTouchPoint3D();
        Vector2 worldPos2D = new Vector2(worldPos3D.x, worldPos3D.y);
        RaycastHit2D hit = Physics2D.Raycast(worldPos2D, Vector2.zero);
        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent<ITouchObject>(out ITouchObject touchObj))
            {
                touchObj.OnTouch();
            }
        }

    }

    public Vector3 GetWorldPosFormTouchPoint3D()
    {
        Vector3 tapWorldPos = Camera.main.ScreenToWorldPoint(touchPoint);
        return tapWorldPos;
    }

}
