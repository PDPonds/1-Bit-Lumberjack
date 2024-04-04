using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Singleton<EnemyController>
{
    public delegate void EnemyTakeDamage();
    public event EnemyTakeDamage OnEnemyTakeDamage;
    public delegate void EnemyDead();
    public event EnemyDead OnEnemyDead;

    [Header("===== Enemy =====")]
    public AllEnemy enemys;

    [Header("===== HP =====")]
    public int maxHP;
    public int curHP;

    [Header("===== Visual =====")]
    [SerializeField] Transform enemyVisual;

    private void OnEnable()
    {
        //GameManager.Instance.OnNextState += SetupEnemy;
        PlayerManager.Instance.OnPlayerAttack += TakeDamage;
    }

    private void Start()
    {
        SetupEnemy();
    }

    public void SetupEnemy()
    {
        int enemyIndex = Random.Range(0, enemys.enemys.Count);
        SpriteRenderer enemySpriteRen = enemyVisual.GetComponent<SpriteRenderer>();
        enemySpriteRen.sprite = enemys.enemys[enemyIndex].enemySprite;
        CalAndResetHP();
    }

    void CalAndResetHP()
    {
        switch (GameManager.Instance.curGameState)
        {
            case GameState.Normal:

                if (GameManager.Instance.curPhase > 2)
                {
                    int GamePhase = GameManager.Instance.curPhase;
                    int min = ((GamePhase - 1) * 100) - 50;
                    int max = (GamePhase - 1) * 100;
                    int hp = Random.Range(min, max);
                    maxHP = hp;
                    curHP = hp;
                }
                else
                {
                    int hp = Random.Range(50, 100);
                    maxHP = hp;
                    curHP = hp;
                }

                break;
            case GameState.Boss:
                int bossHp = GameManager.Instance.curPhase * 100;
                curHP = bossHp;
                maxHP = bossHp;
                break;
        }
    }

    public void TakeDamage(int amount)
    {
        curHP -= amount;
        OnEnemyTakeDamage?.Invoke();
        if (curHP <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        SetupEnemy();
        OnEnemyDead?.Invoke();
    }

}
