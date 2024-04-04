using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, ITouchObject
{
    [HideInInspector] public int amount;
    [SerializeField] float autoAddCoinTime;
    float curAddTime;

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
        if (curAddTime < 0)
        {
            GameManager.Instance.AddCoin(amount);
            Destroy(gameObject);
        }
    }

    public void OnTouch()
    {
        GameManager.Instance.AddCoin(amount);
        Destroy(gameObject);
    }

}
