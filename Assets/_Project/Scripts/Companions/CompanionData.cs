using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CompanionData",menuName = "Data/Companion/General Datas")]
public class CompanionData : ScriptableObject
{
    public CompanionAttackStrategy CompanionStrategyAttack;
}
