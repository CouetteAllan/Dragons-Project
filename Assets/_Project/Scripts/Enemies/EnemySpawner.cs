using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyConfig _enemyDatas;
    private IEnemyFactory factory = new BasicEnemyFactory();

    // Start is called before the first frame update
    void Start()
    {
        EnemyController enemySpawned = factory.Create(_enemyDatas);
        enemySpawned.transform.position = this.transform.position;
    }

}
