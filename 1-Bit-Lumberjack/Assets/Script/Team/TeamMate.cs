using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TeamMate : ScriptableObject
{
    [Header("===== Infomation =====")]
    public string mateName;
    public string mateDescription;
    [Header("===== Cost =====")]
    public int startCost;
    public int mulCostPerLevel;
    [Header("===== Damage =====")]
    public int startDamage;
    public int mulDamagPerLevel;
    [Header("===== Speed =====")]
    public float speed;

}
