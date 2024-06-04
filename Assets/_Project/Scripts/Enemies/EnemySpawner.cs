using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyConfig _enemyDatas;
    private IEnemyFactory factory = new BasicEnemyFactory();
    public bool _spawnOnAwake = false;

    public void Start()
    {
        if (_spawnOnAwake)
        {

            FunctionTimer.Create(() => SpawnEnemy(), 2.0f);
        }
    }

    public void SpawnEnemy(EnemyConfig enemy)
    {
        EnemyController enemySpawned = factory.Create(enemy);
        enemySpawned.transform.position = this.transform.position;
    }
    public void SpawnEnemy()
    {
        SpawnEnemy(_enemyDatas);
    }

}
