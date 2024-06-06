using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    [SerializeField] private List<EnemyWaveDatas> _waves= new List<EnemyWaveDatas>();
    private List<UpdateTimers> _timersWave = new List<UpdateTimers>();

    private bool _pauseEnemies = false;
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
            if(!_pauseEnemies)
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

    public void PauseSpawn()
    {
        _pauseEnemies = true;
        foreach (var timer in _timersWave)
        {
            timer._isPaused = true;
        }
    }

    public void ResumeSpawn()
    {
        _pauseEnemies = false;
        foreach (var timer in _timersWave)
        {
            timer._isPaused = false;
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
        Debug.Log("Currently Spawning the wave: " + wave.name + " at " + Time.timeSinceLevelLoad);
        StartCoroutine(SpawnEnemies(wave));
    }

    private IEnumerator SpawnEnemies(EnemyWaveDatas wave)
    {
        foreach (var component in wave.waveComponents)
        {
            yield return new WaitForSeconds(component.DelayWave);
            yield return new WaitUntil(() => _pauseEnemies == false);
            EnemyManager.Instance.SpawnEnemy(component.EnemyToSpawn);
        }
    }
}
