using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    [SerializeField] private List<EnemyWaveDatas> _waves= new List<EnemyWaveDatas>();
    private List<UpdateTimers> _timersWave = new List<UpdateTimers>();

    private void Awake()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }
    private void OnGameStateChanged(GameState newState)
    {
        if(newState == GameState.InGame)
        {
            foreach (var timer in _timersWave)
            {
                timer._isPaused = false;
            }
        }
        else
        {
            foreach (var timer in _timersWave)
            {
                timer._isPaused = true;
            }
        }
    }

    public void Init()
    {
        foreach(var wave in _waves)
        {
            _timersWave.Add(new UpdateTimers(wave.TimeToSpawnInSeconds,() => SpawnWave(wave)));
        }
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.InGame)
            return;

        foreach(var timer in _timersWave)
        {
            timer.Update();
        }
    }

    public void SpawnWave(EnemyWaveDatas wave)
    {
        StartCoroutine(SpawnEnemies(wave));
    }

    private IEnumerator SpawnEnemies(EnemyWaveDatas wave)
    {
        foreach (var component in wave.waveComponents)
        {
            yield return new WaitForSeconds(component.DelayWave);
            EnemyManager.Instance.SpawnEnemy(component.EnemyToSpawn);
        }
        yield return null;
        yield return null;
    }
}



[Serializable]
public class UpdateTimers
{
    private float _time;
    private Action Callback;
    public bool _isPaused = false;
    private bool _done = false;
    public UpdateTimers(float time, Action callbackOnFinish)
    {
        _time = time;
        Callback = callbackOnFinish;
    }

    public void Update()
    {
        if (_done || _isPaused)
            return;
        _time -= Time.deltaTime;
        if (_time <= 0)
        {
            _done = true;
            Callback?.Invoke();
        }
    }
}