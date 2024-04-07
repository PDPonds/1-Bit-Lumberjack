using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoxState
{
    OnBird, OnGround
}

public class GiftBox : MonoBehaviour, ITouchObject
{
    Animator anim;
    Rigidbody2D rb;

    public BoxState boxState;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    public void OnTouch()
    {
        if (IsState(BoxState.OnGround))
        {
            anim.SetBool("isOpen", true);
            CoinGenerator.Instance.SpawnAndSetupCoin(RandomCoinAmount(), transform.position);
        }
    }

    #region State
    public void SwitchState(BoxState state)
    {
        boxState = state;
        switch (boxState)
        {
            case BoxState.OnBird:
                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.simulated = false;
                anim.SetBool("isOpen", false);
                break;
            case BoxState.OnGround:
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.simulated = true;
                break;
        }
    }

    bool IsState(BoxState state)
    {
        return boxState == state;
    }
    #endregion

    #region Gift Box

    int RandomCoinAmount()
    {
        float min = GameManager.Instance.minBirdGiftDropCoinPercentage;
        float max = GameManager.Instance.maxBirdGiftDropCoinPercentage;

        float percent = Random.Range(min, max);
        float drop = (GameManager.Instance.curPhase * 100) * percent;
        return (int)drop;
    }

    #endregion
}
