using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mother : MonoBehaviour
{
    private int _currentKey = 0;
    private int _maximumKeyNeeded = 3;

    private void Awake()
    {
        _currentKey = 0;
        KeyHole.OnPlayerUseKey += OnPlayerUseKey;
    }
    private void OnDisable()
    {
        KeyHole.OnPlayerUseKey -= OnPlayerUseKey;
    }

    private void OnPlayerUseKey()
    {
        _currentKey++;
        if(_currentKey >= _maximumKeyNeeded)
        {
            GameManager.Instance.ChangeGameState(GameState.Victory);
        }
    }
}
