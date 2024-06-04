using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyFactory : IEnemyFactory
{
    public EnemyController Create(EnemyConfig enemyConfig)
    {
        EnemyController newEnemy = Object.Instantiate(enemyConfig.EnemyPrefab);
        newEnemy.Initialize(enemyConfig);
        return newEnemy;
    }
}
