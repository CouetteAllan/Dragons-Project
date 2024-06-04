using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField]
    private List<EnemySpawner> _spawners= new List<EnemySpawner>();
    [SerializeField] private EnemyWaveManager _waveManager;
    private List<EnemyController> _enemies = new List<EnemyController>();
    private int _currentSpawnerIndex;



    protected override void Awake()
    {
        base.Awake();
        GameManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState newState)
    {
        if(newState == GameState.StartGame)
        {
            Initialize();
        }
    }
    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }

    private void Initialize()
    {
        _waveManager.Init();
    }

    public void SpawnEnemy(EnemyConfig enemy)
    {
        _spawners[_currentSpawnerIndex].SpawnEnemy(enemy);
        _currentSpawnerIndex = (_currentSpawnerIndex + 1) % _spawners.Count;
    }
}
