using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyHole : MonoBehaviour, IInteractable
{
    [SerializeField] private CanvasGroup _fadeCanvas;
    [SerializeField] private TextMeshProUGUI _text;
    public static event Action OnPlayerUseKey;
    private bool _isDisabled;


    private void DeliverMom()
    {
        OnPlayerUseKey?.Invoke();
        HideInteraction();
        _isDisabled = true;
    }

    public void DisplayInteraction()
    {
        if (_isDisabled)
            return;
        _fadeCanvas.gameObject.SetActive(true);
        DOTween.To(() => _fadeCanvas.alpha, (value) => _fadeCanvas.alpha = value, 1.0f, .5f);
        if (GameManager.Instance.Player.KeysNumber <= 0)
        {
            _text.text = "Find a key !";
        }
        else
        {
            _text.text = "Press E to free your Mom";
        }
    }

    public void HideInteraction()
    {
        if (_isDisabled)
            return;
        _fadeCanvas.alpha = 0.0f;
        _fadeCanvas.gameObject.SetActive(false);
    }

    public void Interact(PlayerController player)
    {
        if (_isDisabled)
            return;
        if (player.KeysNumber > 0)
        {
            DeliverMom();
        }
    }
}
