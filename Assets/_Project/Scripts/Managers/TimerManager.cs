using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : Singleton<TimerManager>
{
    private float _currentTimer = 0.0f;
    public float CurrentTimer => _currentTimer;

    private void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.InGame)
            return;
        _currentTimer += Time.deltaTime;
    }
}
