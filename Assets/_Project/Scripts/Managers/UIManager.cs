using DG.Tweening;
using DG.Tweening.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject _gameOverObject, _pauseObject;
    [SerializeField] private CanvasGroup _backgroundGameOver,_backgroundPause;
    [Header("GameOver Related")]
    [SerializeField] private RectTransform _gameOverParent;
    [SerializeField] private RectTransform _gameOverDestination,_gameOverStart;

    [Header("Pause Related")]
    [SerializeField] private RectTransform _pauseParent;
    [SerializeField] private RectTransform _pauseDestination, _pauseStart;

    [Header("Buttons related")]
    [SerializeField] private Button _resumeButton,_replay;
    [SerializeField] private Button[] _quitButtons;

    protected override void Awake()
    {
        base.Awake();
        GameManager.OnGameStateChanged += OnGameStateChanged;
        _resumeButton.onClick.AddListener(() => DisplayPause(false));
        _replay.onClick.AddListener(() => GameManager.Instance.RestartScene());
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
                DisplayGameOver(false);
                break;
            case GameState.InGame:
                DisplayPause(false);
                break;
            case GameState.Pause:
                DisplayPause(true);
                break;
            case GameState.Victory:
                break;
            case GameState.GameOver:
                //Display GameOver
                DisplayGameOver(true);
                break;
        }
    }

    private void DisplayGameOver(bool display)
    {

        DOTween.defaultTimeScaleIndependent = true;
        _gameOverObject.SetActive(display);
        if (display)
        {
            _gameOverParent.transform.position = _gameOverStart.transform.position;
            DOTween.To(() => _backgroundGameOver.alpha, f => _backgroundGameOver.alpha = f, 1.0f, 1.0f);
            _gameOverParent.DOMoveY(_gameOverDestination.position.y, 2.0f).SetEase(Ease.OutElastic).SetAutoKill(false);
        }
        else
        {
            _pauseParent.DOPlayBackwards();
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
