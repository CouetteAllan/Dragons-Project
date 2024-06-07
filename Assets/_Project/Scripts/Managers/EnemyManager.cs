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

    [SerializeField] private int _maxEnemyOnField = 12;
    [SerializeField] private int _minimumEnemiesOnField = 5;

    private bool _isKilling = false;

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
        _isKilling = false;
        _waveManager.Init();
    }

    public void SpawnEnemy(EnemyConfig enemy)
    {
        _spawners[_currentSpawnerIndex].SpawnEnemy(enemy);
        _currentSpawnerIndex = (_currentSpawnerIndex + 1) % _spawners.Count;
    }

    public void AddEnemy(EnemyController enemy)
    {
        if (_isKilling)
            return;
        _enemies.Add(enemy);
        if(_enemies.Count >= _maxEnemyOnField)
        {
            _waveManager.PauseSpawn();
        }
    }

    public void RemoveEnemy(EnemyController enemy)
    {
        if (_isKilling)
            return;
        _enemies.Remove(enemy);
        if (_enemies.Count <= _minimumEnemiesOnField)
        {
            _waveManager.ResumeSpawn();
        }
    }

    public void DisableAllEnemies()
    {
        foreach (var enemy in _enemies)
        {
            enemy.ChangeEnemyState(EnemyState.IsStun);
        }
    }

    public void ReEnableAllEnemies()
    {
        foreach (var enemy in _enemies)
        {
            enemy.ChangeEnemyState(EnemyState.WalkInRange);
        }
    }

    public void KillAllRemainingEnemies()
    {
        _isKilling = true;
        StartCoroutine(KillCoroutine());
    }

    private IEnumerator KillCoroutine()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (_enemies[i] == null)
                continue;
            _enemies[i].ChangeEnemyState(EnemyState.IsDead);
            yield return new WaitForSecondsRealtime(.3f);
        }
    }
}
