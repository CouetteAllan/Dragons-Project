using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


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

    private void Start()
    {
        PlayerInputs.OnPauseButtonPressed += OnPauseButtonPressed;
        PlayerController.OnPlayerDeath += OnPlayerDeath;
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        var scene = SceneManager.GetActiveScene();
        if(scene.buildIndex == 0)
            ChangeGameState(GameState.MainMenu);
        else
            ChangeGameState(GameState.StartGame);
        
    }

    private void SceneManager_activeSceneChanged(Scene previousScene, Scene newActiveScene)
    {
        if(newActiveScene == SceneManager.GetSceneByBuildIndex(1))
        {
            ChangeGameState(GameState.StartGame);
        }

        if(newActiveScene != SceneManager.GetSceneByBuildIndex(0))
            this.gameObject.GetComponent<FadeScreen>().FadeIn();
    }

    public void FadeOut()
    {
        this.gameObject.GetComponent<FadeScreen>().FadeOut();
        StartCoroutine(DelayChangeSceneCoroutine());
    }

    IEnumerator DelayChangeSceneCoroutine()
    {
        yield return new WaitForSecondsRealtime(2.0f);
        SceneManager.LoadScene(2);
    }

    private void OnPlayerDeath()
    {
        GameManager.Instance.ChangeGameState(GameState.GameOver);
    }

    private void OnDisable()
    {
        PlayerInputs.OnPauseButtonPressed -= OnPauseButtonPressed;
        PlayerController.OnPlayerDeath -= OnPlayerDeath;
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
            case GameState.Victory:
                Debug.Log("you won the game !");
                EnemyManager.Instance.DisableAllEnemies();
                break;
            case GameState.StartGame:
                StartCoroutine(StartGame());
                EnemyManager.Instance.ReEnableAllEnemies();

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


    public void BackToMainMenu()
    {
        ChangeGameState(GameState.MainMenu);
        SceneManager.LoadScene(0);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(1);
    }
}
