using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Data/Enemy/Enemy Config")]
public class EnemyConfig : ScriptableObject
{
    public enum EnemyType
    {
        Basic,
        Ranged,
        Mage,
        Elite,
        Boss
    }

    [Header("EnemyType")]
    public EnemyType Type;

    [Header("Base stats")]
    public float BaseHealth = 15.0f;
    public float BaseDamage = 10.0f;
    public float BaseSpeed = 8.0f;
    //Strategy Enemy
    public float Range = 5.0f;
    public float AttackRadius = 2.0f;
    public float AttackLenght = 2.0f;
    public float DashAttackStrenght = 15.0f;


    [Header("Prefab")]
    public EnemyController EnemyPrefab;

    [Header("Boss Patterns")]
    public BossPattern[] BossPatterns = new BossPattern[3];

    public IEnemyStrategy GetStrategy(EnemyController controller, PlayerController player, Animator anim)
    {
        switch (Type)
        {
            case EnemyType.Basic:
                return new BasicStrategy(controller, player, anim);
            case EnemyType.Boss:
                return new BossStrategy(controller, player, anim, BossPatterns);
            default:
                return new BasicStrategy(controller, player, anim);
        }
    }
    
}
