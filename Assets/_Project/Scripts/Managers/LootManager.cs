using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    [SerializeField] private MMLootTableGameObjectSO _lootTable;

    private void Awake()
    {
        EnemyController.OnEnemyDeath += OnEnemyDeath;
    }

    private void OnDisable()
    {
        EnemyController.OnEnemyDeath -= OnEnemyDeath;
    }
    private void OnEnemyDeath(EnemyController enemy)
    {
        var lootObject = _lootTable.GetLoot();
        if (lootObject != null)
        {
            PickUpObject powerUp = Instantiate(lootObject, enemy.transform.position, Quaternion.identity).GetComponent<PickUpObject>();
            powerUp.InitObject();
        }
    }
}
