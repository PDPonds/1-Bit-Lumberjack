using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : Singleton<CoinGenerator>
{
    [SerializeField] int minCoinDrop;
    [SerializeField] int maxCoinDrop;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] Transform coinSpawnPoint;

    private void OnEnable()
    {
        EnemyController.Instance.OnEnemyDead += GenerateCoin;
    }

    void SpawnAndSetupCoin(int amount)
    {
        int coinCount = Random.Range(minCoinDrop, maxCoinDrop);
        int amountPerCoin = amount / coinCount;

        for (int i = 0; i < coinCount; i++)
        {
            //Create Coin Object
            GameObject coinObj = Instantiate(coinPrefab, coinSpawnPoint.position, Quaternion.identity);

            //Setup Coin Amount
            Coin coin = coinObj.GetComponent<Coin>();
            coin.amount = amountPerCoin;

            //Random Add Force
            Rigidbody2D rb = coinObj.GetComponent<Rigidbody2D>();
            float ranX = Random.Range(-1f, 1f);
            Vector2 dir = new Vector2(ranX, 0);
            rb.AddForce(dir, ForceMode2D.Impulse);
        }

    }

    int CalDropCoinAmount()
    {
        return EnemyController.Instance.maxHP / 2;
    }

    void GenerateCoin()
    {
        SpawnAndSetupCoin(CalDropCoinAmount());
    }

}