using System;
using UnityEngine;

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