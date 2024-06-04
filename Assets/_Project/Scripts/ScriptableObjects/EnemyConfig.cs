using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Data/Enemy/Enemy Config")]
public class EnemyConfig : ScriptableObject
{
    [Header("Base stats")]
    public float BaseHealth = 15.0f;
    public float BaseDamage = 10.0f;
    public float BaseSpeed = 8.0f;
    //Strategy Enemy
    public float Range = 5.0f;

    [Header("Prefab")]
    public EnemyController EnemyPrefab;
}
