using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mother : Singleton<Mother>
{
    private int _currentKey = 0;
    private int _maximumKeyNeeded = 3;

    private void Start()
    {
        _currentKey = 0;
    }

    public void OnPlayerUseKey()
    {
        _currentKey++;
        if(_currentKey >= _maximumKeyNeeded)
        {
            GameManager.Instance.ChangeGameState(GameState.Victory);
        }
    }
}
