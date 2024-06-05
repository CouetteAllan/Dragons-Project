using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public enum GameState
{
    MainMenu,
    StartGame,
    InGame,
    Pause,
    Victory,
    GameOver
}

public class GameManager : Singleton<GameManager>
{
    public static event Action<GameState> OnGameStateChanged;

    public GameState CurrentState { get; private set; }

    public PlayerController Player { get; private set; }

    private bool _isInPause = false;

    public void Start()
    {
        PlayerInputs.OnPauseButtonPressed += OnPauseButtonPressed;
        ChangeGameState(GameState.StartGame);
    }

    private void OnDisable()
    {
        PlayerInputs.OnPauseButtonPressed -= OnPauseButtonPressed;
    }

    private void OnPauseButtonPressed()
    {
        _isInPause = !_isInPause;
        GameState pauseState = _isInPause ? GameState.Pause : GameState.InGame;
        ChangeGameState(pauseState);
    }

    public void ChangeGameState(GameState newState)
    {
        if (newState == CurrentState)
            return;

        CurrentState = newState;
        switch (newState)
        {
            case GameState.MainMenu:
                break;
            case GameState.StartGame:
                StartCoroutine(StartGame());
                EnemyManager.Instance.ReenableAllEnemies();

                break;
            case GameState.InGame:
                Time.timeScale = 1.0f;
                Time.fixedDeltaTime = Time.timeScale * 0.01f;
                _isInPause = false;
                break;
            case GameState.GameOver:
                Time.timeScale = .5f;
                Time.fixedDeltaTime = Time.timeScale * 0.01f;
                EnemyManager.Instance.DisableAllEnemies();
                break;

            case GameState.Pause:
                Time.timeScale = 0.0f;
                Time.fixedDeltaTime = Time.timeScale * 0.01f;
                _isInPause = true;
                break;
        }

        OnGameStateChanged?.Invoke(CurrentState);
    }

    public void SetPlayer(PlayerController player)
    {
        Player = player;
    }

    public IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2.0f);
        ChangeGameState(GameState.InGame);
    }

    private void Update()
    {
        if (Keyboard.current.jKey.wasPressedThisFrame)
            ChangeGameState(GameState.Pause);
    }

    public void BackToMainMenu()
    {

    }
}
