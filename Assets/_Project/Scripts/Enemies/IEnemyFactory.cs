using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyFactory
{
    public EnemyController Create(EnemyConfig enemyConfig);
}

