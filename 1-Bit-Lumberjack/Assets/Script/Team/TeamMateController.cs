using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamMateController : MonoBehaviour
{
    public delegate void TeamAttackEvent(int amount);
    public event TeamAttackEvent OnTeamAttack;

    Animator anim;

    public TeamMate teamMate;

    float curAtkTime;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        curAtkTime = teamMate.speed;
    }

    private void Update()
    {
        TryAttack();
    }

    void TryAttack()
    {
        curAtkTime -= Time.deltaTime;
        if (curAtkTime < 0)
        {
            Attack();
            curAtkTime = teamMate.speed;
        }
    }

    void Attack()
    {
        OnTeamAttack?.Invoke(GetCurDamage());
        TextGenerator.Instance.GenerateText(GetCurDamage());
        anim.Play("Attack");
    }

    public int GetCurDamage()
    {
        if (teamMate.name == "Lumberjack")
        {
            if (GameManager.curLumberjackLevel > 1) return teamMate.startDamage + (GameManager.curLumberjackLevel * teamMate.mulDamagPerLevel);
            else return teamMate.startDamage;
        }
        else if (teamMate.name == "Woodpecker")
        {
            if (GameManager.curWoodpeckerLevel > 1) return teamMate.startDamage + (GameManager.curWoodpeckerLevel * teamMate.mulDamagPerLevel);
            else return teamMate.startDamage;
        }

        else return 0;
    }

    public int GetCurCost()
    {
        if (teamMate.name == "Lumberjack")
        {
            if (GameManager.curLumberjackLevel == 0) return teamMate.startCost;
            else
                return teamMate.startCost + (GameManager.curLumberjackLevel * teamMate.mulCostPerLevel);
        }
        else if (teamMate.name == "Woodpecker")
        {
            if (GameManager.curWoodpeckerLevel == 0) return teamMate.startCost;
            else
                return teamMate.startCost + (GameManager.curWoodpeckerLevel * teamMate.mulCostPerLevel);
        }
        else return 0;
    }

}
