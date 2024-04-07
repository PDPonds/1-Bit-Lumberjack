using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SkillState
{
    Ready, Use, Delay
}

public class SkillManager : Singleton<SkillManager>
{
    public delegate void StrikeReadyEvent();
    public event StrikeReadyEvent OnStrikeReady;

    public delegate void StrikeUseEvent();
    public event StrikeUseEvent OnStrikeUse;

    public delegate void StrikeDelayEvent();
    public event StrikeDelayEvent OnStrikeDelay;

    public Skill[] skills;

    public SkillState strikeSkillState;
    [HideInInspector] public float curStrikeDelay;
    [HideInInspector] public float curStrikeTime;

    private void Update()
    {
        UpdateStrikeState();
    }

    #region State Strike Skill
    void SwitchSkrikeState(SkillState state)
    {
        strikeSkillState = state;
        Skill skill = GetSkill("Strike");
        switch (strikeSkillState)
        {
            case SkillState.Ready:
                OnStrikeReady?.Invoke();
                break;
            case SkillState.Use:
                OnStrikeUse?.Invoke();
                curStrikeTime = skill.useTime;
                break;
            case SkillState.Delay:
                OnStrikeDelay?.Invoke();
                curStrikeDelay = skill.delayTime;
                break;
        }
    }

    void UpdateStrikeState()
    {
        switch (strikeSkillState)
        {
            case SkillState.Ready:
                break;
            case SkillState.Use:

                curStrikeTime -= Time.deltaTime;
                if (curStrikeTime < 0)
                {
                    SwitchSkrikeState(SkillState.Delay);
                }

                break;
            case SkillState.Delay:
                curStrikeDelay -= Time.deltaTime;
                if (curStrikeDelay < 0)
                {
                    SwitchSkrikeState(SkillState.Ready);
                }
                break;
        }
    }

    public bool CheckStrikeState(SkillState state)
    {
        return strikeSkillState == state;
    }

    public void UseStrikeSkill()
    {
        Skill skill = GetSkill("Strike");
        if (CheckStrikeState(SkillState.Ready) && GetCanUse(skill))
        {
            GameManager.Instance.RemoveMana(GetSkillMana(skill));
            SwitchSkrikeState(SkillState.Use);
        }
    }

    #endregion

    public Skill GetSkill(string name)
    {
        Skill skill = Array.Find(skills, skill => skill.skillName == name);
        if (skill == null) return null;
        return skill;
    }

    public string GetSkillName(Skill skill)
    {
        return skill.skillName;
    }

    public string GetSkillDiscription(Skill skill)
    {
        return skill.skillDiscription;
    }

    public int GetSkillLevel(Skill skill)
    {
        return skill.curLevel;
    }

    public float GetSkillValue(Skill skill)
    {
        return skill.GetValue();
    }

    public float GetSkillMana(Skill skill)
    {
        return skill.manaCost;
    }

    public float GetMulValue(Skill skill)
    {
        return skill.mulValue;
    }

    public float GetValue(Skill skill)
    {
        return skill.GetValue();
    }

    public int GetCostToUpgrade(Skill skill)
    {
        return skill.GetCostToUpgrade();
    }

    public void UpgradeSkill(Skill skill)
    {
        skill.Upgrade();
    }

    public bool GetCanUse(Skill skill)
    {
        return skill.CanUseSkill();
    }

    public bool GetCanUpgrade(Skill skill)
    {
        return skill.CanUpgrade();
    }

    public bool HasSkill(Skill skill)
    {
        return skill.HasSkill();
    }

}

[Serializable]
public class Skill
{
    public delegate void UpgradeSkillEvent();
    public UpgradeSkillEvent OnUpgradeSkill;

    [Header("===== Infomation =====")]
    public string skillName;
    public Sprite skillSprite;
    public string skillDiscription;
    [Header("===== Mana =====")]
    public float manaCost;
    [Header("===== Level =====")]
    public int curLevel;
    public int startUpgradeCost;
    public int upgradeCostPerLevel;
    [Header("===== Value =====")]
    public float startValue;
    public float mulValue;
    [Header("===== Time =====")]
    public float useTime;
    public float delayTime;

    public bool HasSkill()
    {
        return curLevel > 0;
    }

    public int GetCostToUpgrade()
    {
        if (curLevel == 0) return startUpgradeCost;
        else return startUpgradeCost + (curLevel * upgradeCostPerLevel);
    }

    public bool CanUseSkill()
    {
        return GameManager.Instance.CheckMana(manaCost) && HasSkill();
    }

    public bool CanUpgrade()
    {
        return GameManager.Instance.CheckCoin(GetCostToUpgrade());
    }

    public float GetValue()
    {
        return startValue + (curLevel * mulValue);
    }

    public void Upgrade()
    {
        if (CanUpgrade())
        {
            int cost = GetCostToUpgrade();
            GameManager.Instance.RemoveCoin(cost);
            curLevel++;
            OnUpgradeSkill?.Invoke();
        }
    }


}