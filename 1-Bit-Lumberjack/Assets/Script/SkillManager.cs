using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillState
{
    Ready, Use, Delay
}

public class SkillManager : Singleton<SkillManager>
{
    #region Strike Delegate And Event
    public delegate void StrikeReadyEvent();
    public event StrikeReadyEvent OnStrikeReady;

    public delegate void StrikeUseEvent();
    public event StrikeUseEvent OnStrikeUse;

    public delegate void StrikeDelayEvent();
    public event StrikeDelayEvent OnStrikeDelay;
    #endregion

    #region Looting Delegate And Event
    public delegate void LootingReadyEvent();
    public event LootingReadyEvent OnLootingReady;

    public delegate void LootingUseEvent();
    public event LootingUseEvent OnLootingUse;

    public delegate void LootingDelayEvent();
    public event LootingDelayEvent OnLootingDelay;
    #endregion

    #region Teamwork Delegate And Event
    public delegate void TeamworkReadyEvent();
    public event TeamworkReadyEvent OnTeamworkReady;

    public delegate void TeamworkUseEvent();
    public event TeamworkUseEvent OnTeamworkUse;

    public delegate void TeamworkDelayEvent();
    public event TeamworkDelayEvent OnTeamworkDelay;
    #endregion


    public Skill[] skills;

    [Header("===== Strike =====")]
    public SkillState strikeSkillState;
    [HideInInspector] public float curStrikeDelay;
    [HideInInspector] public float curStrikeTime;
    [Header("===== Looting =====")]
    public SkillState lootingSkillState;
    [HideInInspector] public float curLootingDelay;
    [HideInInspector] public float curLootingTime;
    [Header("===== Teamwork =====")]
    public SkillState teamworkSkillState;
    [HideInInspector] public float curTeamworkDelay;
    [HideInInspector] public float curTeamworkTime;

    private void Update()
    {
        UpdateStrikeState();
        UpdateLootingState();
        UpdateTeamworkState();
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

    #region State Looting Skill

    void SwitchLootingState(SkillState state)
    {
        lootingSkillState = state;
        Skill skill = GetSkill("Looting");
        switch (lootingSkillState)
        {
            case SkillState.Ready:
                OnLootingReady?.Invoke();
                break;
            case SkillState.Use:
                OnLootingUse?.Invoke();
                curLootingTime = skill.useTime;
                break;
            case SkillState.Delay:
                OnLootingDelay?.Invoke();
                curLootingDelay = skill.delayTime;
                break;
        }
    }

    void UpdateLootingState()
    {
        switch (lootingSkillState)
        {
            case SkillState.Ready:
                break;
            case SkillState.Use:

                curLootingTime -= Time.deltaTime;
                if (curLootingTime < 0)
                {
                    SwitchLootingState(SkillState.Delay);
                }

                break;
            case SkillState.Delay:
                curLootingDelay -= Time.deltaTime;
                if (curLootingDelay < 0)
                {
                    SwitchLootingState(SkillState.Ready);
                }
                break;
        }
    }

    public bool CheckLootingState(SkillState state)
    {
        return lootingSkillState == state;
    }

    public void UseLootingSkill()
    {
        Skill skill = GetSkill("Looting");
        if (CheckLootingState(SkillState.Ready) && GetCanUse(skill))
        {
            GameManager.Instance.RemoveMana(GetSkillMana(skill));
            SwitchLootingState(SkillState.Use);
        }
    }

    #endregion

    #region State Teamwork Skill

    void SwitchTeamworkState(SkillState state)
    {
        teamworkSkillState = state;
        Skill skill = GetSkill("Teamwork");
        switch (teamworkSkillState)
        {
            case SkillState.Ready:
                OnTeamworkReady?.Invoke();
                break;
            case SkillState.Use:
                OnTeamworkUse?.Invoke();
                curTeamworkTime = skill.useTime;
                break;
            case SkillState.Delay:
                OnTeamworkDelay?.Invoke();
                curTeamworkDelay = skill.delayTime;
                break;
        }
    }

    void UpdateTeamworkState()
    {
        switch (teamworkSkillState)
        {
            case SkillState.Ready:
                break;
            case SkillState.Use:

                curTeamworkTime -= Time.deltaTime;
                if (curTeamworkTime < 0)
                {
                    SwitchTeamworkState(SkillState.Delay);
                }

                break;
            case SkillState.Delay:
                curTeamworkDelay -= Time.deltaTime;
                if (curTeamworkDelay < 0)
                {
                    SwitchTeamworkState(SkillState.Ready);
                }
                break;
        }
    }

    public bool CheckTeamworkState(SkillState state)
    {
        return teamworkSkillState == state;
    }

    public void UseTeamworkSkill()
    {
        Skill skill = GetSkill("Teamwork");
        if (CheckTeamworkState(SkillState.Ready) && GetCanUse(skill))
        {
            GameManager.Instance.RemoveMana(GetSkillMana(skill));
            SwitchTeamworkState(SkillState.Use);
        }
    }
    #endregion

    #region Function
    public Skill GetSkill(string name)
    {
        Skill skill = Array.Find(skills, skill => skill.skillName == name);
        if (skill == null) return null;
        return skill;
    }

    public string GetSkillDiscription(Skill skill)
    {
        return skill.skillDiscription;
    }

    public int GetSkillLevel(Skill skill)
    {
        return skill.curLevel;
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

    #endregion


}

[Serializable]
public class Skill
{
    public delegate void UpgradeSkillEvent();
    public UpgradeSkillEvent OnUpgradeSkill;

    [Header("===== Infomation =====")]
    public string skillName;
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
            SaveSystem.Save();
        }
    }


}