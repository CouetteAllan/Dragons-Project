using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyConfig _enemyDatas;
    private IEnemyFactory factory = new BasicEnemyFactory();

    public void SpawnEnemy()
    {
        EnemyController enemySpawned = factory.Create(_enemyDatas);
        enemySpawned.transform.position = this.transform.position;
    }

}
