using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Singleton<EnemyController>
{
    public delegate void EnemyTakeDamage();
    public event EnemyTakeDamage OnEnemyTakeDamage;
    public delegate void EnemyDead();
    public event EnemyDead OnEnemyDead;

    [HideInInspector] public Animator anim;

    [Header("===== Enemy =====")]
    public AllEnemy enemys;
    [HideInInspector] public Enemy curEnemy;

    [Header("===== HP =====")]
    public int maxHP;
    public int curHP;

    [Header("===== Visual =====")]
    [SerializeField] Transform enemyVisual;
    [SerializeField] Transform spawnAttackParticle;

    private void OnEnable()
    {
        PlayerManager.Instance.OnPlayerAttack += TakeDamage;
        GameManager.Instance.lumberjack.OnTeamAttack += TakeDamage;
        GameManager.Instance.woodpecker.OnTeamAttack += TakeDamage;
        GameManager.Instance.OnExitBossState += SetupEnemy;
    }

    private void Awake()
    {
        anim = enemyVisual.GetComponent<Animator>();
    }

    private void Start()
    {
        SetupEnemy();
    }

    public void SetupEnemy()
    {
        int enemyIndex = 0;
        SpriteRenderer enemySpriteRen = enemyVisual.GetComponent<SpriteRenderer>();

        if (GameManager.curState == GameManager.Instance.maxStatePerPhase - 1)
        {
            enemyIndex = Random.Range(0, enemys.boss.Count);
            curEnemy = enemys.boss[enemyIndex];
            enemySpriteRen.sprite = enemys.boss[enemyIndex].enemySprite;
        }
        else
        {
            enemyIndex = Random.Range(0, enemys.enemys.Count);
            curEnemy = enemys.enemys[enemyIndex];
            enemySpriteRen.sprite = enemys.enemys[enemyIndex].enemySprite;
        }


        CalAndResetHP();
    }

    void CalAndResetHP()
    {
        switch (GameManager.Instance.curGameState)
        {
            case GameState.Normal:

                if (GameManager.curPhase > 2)
                {
                    int GamePhase = GameManager.curPhase;
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
                int bossHp = GameManager.curPhase * 100;
                curHP = bossHp;
                maxHP = bossHp;
                break;
        }
    }

    public void TakeDamage(int amount)
    {
        curHP -= amount;
        anim.Play("EnemyTakeDamage");
        ParticleManager.Instance.SpawnParticle("AttackParticle", spawnAttackParticle.position);
        OnEnemyTakeDamage?.Invoke();
        if (curHP <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        SetupEnemy();
        anim.Play("EnemyDead");
        OnEnemyDead?.Invoke();
        GameManager.curKillEnemyCount++;
        ArchievementUI.Instance.UpdateKillEnemy();
        SaveSystem.Save();
    }

}
