using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BirdBehavior
{
    MoveLeft, MoveRight, MoveExit
}

public class Bird : MonoBehaviour, ITouchObject
{
    public BirdBehavior birdBehavior;
    [SerializeField] float moveSpeed;
    [SerializeField] Transform giftSpawnPoint;
    [SerializeField] GameObject giftBoxPrefab;
    GiftBox curGiftBox;
    BirdBehavior lastBehavior;
    Transform exitPos;

    float curBirdGoBackTime;

    public void OnTouch()
    {
        curGiftBox.SwitchState(BoxState.OnGround);
        lastBehavior = birdBehavior;
        if (lastBehavior == BirdBehavior.MoveLeft)
        {
            exitPos = BirdGenerator.Instance.spawnAndExitLeftPos;
        }
        else
        {
            exitPos = BirdGenerator.Instance.spawnAndExitRightPos;
        }
        SwitchBehavior(BirdBehavior.MoveExit);
    }

    private void Update()
    {
        UpdateBehavior();
    }

    #region Bird Behavior

    public void OnSpawn()
    {
        SpawnGiftBox();
    }

    public void SwitchBehavior(BirdBehavior behavior)
    {
        birdBehavior = behavior;
        SpriteRenderer spriteRnd = GetComponent<SpriteRenderer>();
        switch (birdBehavior)
        {
            case BirdBehavior.MoveLeft:

                spriteRnd.flipX = true;
                curBirdGoBackTime = BirdGenerator.Instance.birdGoBackTime;

                break;
            case BirdBehavior.MoveRight:

                spriteRnd.flipX = false;
                curBirdGoBackTime = BirdGenerator.Instance.birdGoBackTime;

                break;
            case BirdBehavior.MoveExit:

                if (lastBehavior == BirdBehavior.MoveLeft)
                {
                    spriteRnd.flipX = true;
                }
                else if (lastBehavior == BirdBehavior.MoveRight)
                {
                    spriteRnd.flipX = false;
                }

                break;
        }
    }

    void UpdateBehavior()
    {
        switch (birdBehavior)
        {
            case BirdBehavior.MoveLeft:

                transform.position = Vector2.MoveTowards(transform.position,
                    BirdGenerator.Instance.moveLeftPos.position,
                    moveSpeed * Time.deltaTime);
                float leftDis = Vector2.Distance(transform.position, BirdGenerator.Instance.moveLeftPos.position);
                if (leftDis < 0.1f)
                {
                    SwitchBehavior(BirdBehavior.MoveRight);
                }

                AutoResetBird();

                break;
            case BirdBehavior.MoveRight:

                transform.position = Vector2.MoveTowards(transform.position,
                    BirdGenerator.Instance.moveRightPos.position,
                    moveSpeed * Time.deltaTime);

                float rightDis = Vector2.Distance(transform.position, BirdGenerator.Instance.moveRightPos.position);
                if (rightDis < 0.1f)
                {
                    SwitchBehavior(BirdBehavior.MoveLeft);
                }

                AutoResetBird();

                break;
            case BirdBehavior.MoveExit:

                transform.position = Vector2.MoveTowards(transform.position,
                    exitPos.position, moveSpeed * Time.deltaTime);

                float exitDis = Vector2.Distance(transform.position, exitPos.position);
                if (exitDis < 0.1f)
                {
                    BirdGenerator.Instance.ResetDelayTime();
                    BirdGenerator.Instance.curBird = null;
                    Destroy(gameObject);
                }

                break;
        }
    }

    void AutoResetBird()
    {
        curBirdGoBackTime -= Time.deltaTime;
        if (curBirdGoBackTime < 0)
        {
            lastBehavior = birdBehavior;
            if (lastBehavior == BirdBehavior.MoveLeft)
            {
                exitPos = BirdGenerator.Instance.spawnAndExitLeftPos;
            }
            else
            {
                exitPos = BirdGenerator.Instance.spawnAndExitRightPos;
            }
            SwitchBehavior(BirdBehavior.MoveExit);
        }
    }

    #endregion

    #region GiftBox

    void SpawnGiftBox()
    {
        GameObject box = Instantiate(giftBoxPrefab, giftSpawnPoint);
        GiftBox giftBox = box.GetComponent<GiftBox>();
        curGiftBox = giftBox;
        giftBox.SwitchState(BoxState.OnBird);
    }

    #endregion

}
