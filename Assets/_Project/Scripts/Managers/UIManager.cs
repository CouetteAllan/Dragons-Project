using DG.Tweening;
using DG.Tweening.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject _gameOverObject, _pauseObject, _victoryObject;
    [SerializeField] private CanvasGroup _backgroundGameOver,_backgroundPause, _backgroundVictory;
    [Header("GameOver Related")]
    [SerializeField] private RectTransform _gameOverParent;
    [SerializeField] private RectTransform _gameOverDestination,_gameOverStart;

    [Header("Pause Related")]
    [SerializeField] private RectTransform _pauseParent;
    [SerializeField] private RectTransform _pauseDestination, _pauseStart;

    [Header("Victory Related")]
    [SerializeField] private RectTransform _victoryParent;
    [SerializeField] private RectTransform _victoryDestination, _victoryStart;

    [Header("Buttons related")]
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button[] _replayButtons;
    [SerializeField] private Button[] _quitButtons;

    protected override void Awake()
    {
        base.Awake();
        GameManager.OnGameStateChanged += OnGameStateChanged;
        _resumeButton.onClick.AddListener(() => DisplayPause(false));
        foreach (var replay in _replayButtons)
        {
            replay.onClick.AddListener(() => GameManager.Instance.RestartScene());
        }
        foreach (var b in _quitButtons)
        {
            b.onClick.AddListener(() => GameManager.Instance.BackToMainMenu());
        }
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState newState)
    {
        switch (newState)
        {
            case GameState.MainMenu:
                break;
            case GameState.StartGame:
                DisplayPause(false);
                DisplayEndGame(false, _gameOverObject, _gameOverParent, _gameOverDestination, _gameOverStart, _backgroundGameOver);
                DisplayEndGame(false, _victoryObject, _victoryParent, _victoryDestination, _victoryStart, _backgroundVictory);
                break;
            case GameState.InGame:
                DisplayPause(false);
                break;
            case GameState.Pause:
                DisplayPause(true);
                break;
            case GameState.Victory:
                DisplayEndGame(true, _victoryObject, _victoryParent, _victoryDestination, _victoryStart, _backgroundVictory);
                break;
            case GameState.GameOver:
                //Display GameOver
                DisplayEndGame(true, _gameOverObject, _gameOverParent,_gameOverDestination,_gameOverStart,_backgroundGameOver);
                break;
        }
    }

    private void DisplayEndGame(bool display,GameObject objectParent, RectTransform parent, RectTransform destination, RectTransform start, CanvasGroup canvas)
    {

        DOTween.defaultTimeScaleIndependent = true;
        objectParent.SetActive(display);
        if (display)
        {
            parent.transform.position = start.transform.position;
            DOTween.To(() => canvas.alpha, f => canvas.alpha = f, 1.0f, 1.0f);
            parent.DOMoveY(destination.position.y, 2.0f).SetEase(Ease.OutElastic).SetAutoKill(false);
        }
        else
        {
            parent.DOPlayBackwards();
        }

    }

    private void DisplayPause(bool display)
    {
        DOTween.defaultTimeScaleIndependent = true;
        if (display)
        {
            _pauseObject.SetActive(display);
            _pauseParent.transform.position = _pauseStart.transform.position;
            DOTween.To(() => _backgroundPause.alpha, f => _backgroundPause.alpha = f, 1.0f, 1.0f);
            _pauseParent.DOMoveY(_pauseDestination.position.y, 1.0f)
                .SetEase(Ease.OutBack)
                .SetAutoKill(false)
                .OnRewind(() => { 
                    _pauseObject.SetActive(false);
                    GameManager.Instance.ChangeGameState(GameState.InGame);
                });
        }
        else
        {
            _pauseParent.DOPlayBackwards();
        }
    }



}
