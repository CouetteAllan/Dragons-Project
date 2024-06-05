using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cage : MonoBehaviour, IInteractable
{
    [SerializeField] private CompanionData _companion;
    [SerializeField] private CompanionController _companionController;
    [SerializeField] private GameObject _interactionCanvas;
    [SerializeField] private CanvasGroup _canvasRenderer;
    [SerializeField] private TextMeshProUGUI _text;

    private bool _isDisabled = false;
    private void Awake()
    {
        HideInteraction();
        _text.text += " " + _companion.CompanionName;
    }

    public void DisplayInteraction()
    {
        if (_isDisabled)
            return;
        _interactionCanvas.SetActive(true);
        DOTween.To(() => _canvasRenderer.alpha, (value) => _canvasRenderer.alpha = value, 1.0f,.5f);
    }

    public void HideInteraction()
    {
        if (_isDisabled)
            return;
        _canvasRenderer.alpha = 0.0f;
        _interactionCanvas.SetActive(false);
    }

    public void Interact(PlayerController player)
    {
        if (player.KeysNumber <= 0)
            return;
        _companionController.Deliver(player);
        player.AddCompanion(_companionController);
        HideInteraction();
        _isDisabled = true;
    }
}
