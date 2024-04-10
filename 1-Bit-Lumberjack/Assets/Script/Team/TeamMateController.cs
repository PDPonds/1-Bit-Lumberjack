using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamMateController : MonoBehaviour
{
    Animator anim;

    [SerializeField] TeamMate teamMate;

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
        Debug.Log("Attack");
    }

    public int GetCurDamage()
    {
        if (teamMate.name == "Lumberjack")
            return teamMate.startDamage + (GameManager.curLumberjackLevel * teamMate.mulDamagPerLevel);
        else if (teamMate.name == "Woodpecker")
            return teamMate.startDamage + (GameManager.curWoodpeckerLevel * teamMate.mulDamagPerLevel);
        else return 0;
    }

    public int GetCurCost()
    {
        if (teamMate.name == "Lumberjack")
            return teamMate.startCost + (GameManager.curLumberjackLevel * teamMate.mulCostPerLevel);
        else if (teamMate.name == "Woodpecker")
            return teamMate.startCost + (GameManager.curWoodpeckerLevel * teamMate.mulCostPerLevel);
        else return 0;
    }

}
