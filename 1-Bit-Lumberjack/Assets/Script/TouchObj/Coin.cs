using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, ITouchObject
{
    [HideInInspector] public int amount;
    [SerializeField] float autoAddCoinTime;
    float curAddTime;
    bool isMove;

    private void Start()
    {
        curAddTime = autoAddCoinTime;
    }

    private void Update()
    {
        AutoAddCoin();
    }

    void AutoAddCoin()
    {
        curAddTime -= Time.deltaTime;
        if (curAddTime < 0 && !isMove)
        {
            MoveToCoinIcon();
            isMove = true;
        }
    }

    public void OnTouch()
    {
        MoveToCoinIcon();
    }

    void MoveToCoinIcon()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(UIManager.Instance.coinIcon.position);
        Vector2 pos2D = new Vector2(pos.x, pos.y);
        LeanTween.move(gameObject, pos2D, 0.5f)
            .setEaseInOutCubic()
            .setOnComplete(AddCoin);
    }

    void AddCoin()
    {
        GameManager.Instance.AddCoin(amount);
        GameManager.curCollectGoldCount += amount;
        ArchievementUI.Instance.UpdateCollectCoin();
        SaveSystem.Save();
        Destroy(gameObject);
    }

}
