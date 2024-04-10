using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoxState
{
    OnBird, OnGround, OpenAready
}

public class GiftBox : MonoBehaviour, ITouchObject
{
    Animator anim;
    Rigidbody2D rb;

    public BoxState boxState;

    public float autoOpenTime;
    float curOpenTime;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        UpdateState();
    }

    public void OnTouch()
    {
        if (IsState(BoxState.OnGround))
        {
            CoinGenerator.Instance.SpawnAndSetupCoin(RandomCoinAmount(), transform.position);
            SwitchState(BoxState.OpenAready);
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
                curOpenTime = autoOpenTime;
                break;
            case BoxState.OpenAready:
                anim.SetBool("isOpen", true);
                break;
        }
    }

    bool IsState(BoxState state)
    {
        return boxState == state;
    }

    void UpdateState()
    {
        switch (boxState)
        {
            case BoxState.OnBird:

                break;
            case BoxState.OnGround:

                curOpenTime -= Time.deltaTime;
                if(curOpenTime <= 0)
                {
                    CoinGenerator.Instance.SpawnAndSetupCoin(RandomCoinAmount(), transform.position);
                    SwitchState(BoxState.OpenAready);
                }

                break;
            case BoxState.OpenAready:

                Destroy(gameObject, 1f);

                break;
        }

    }

    #endregion

    #region Gift Box

    int RandomCoinAmount()
    {
        float min = GameManager.Instance.minBirdDropPercent;
        float max = GameManager.Instance.maxBirdDropPercent;

        float percent = Random.Range(min, max);
        float drop = (GameManager.curPhase * 100) * percent;
        return (int)drop;
    }

    #endregion
}
