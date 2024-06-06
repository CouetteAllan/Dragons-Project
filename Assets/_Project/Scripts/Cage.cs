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
    }

    public void DisplayInteraction()
    {
        if (_isDisabled)
            return;
        _interactionCanvas.SetActive(true);
        DOTween.To(() => _canvasRenderer.alpha, (value) => _canvasRenderer.alpha = value, 1.0f,.5f);
        if(GameManager.Instance.Player.KeysNumber <= 0)
        {
            _text.text = "You need a key to free " + _companion.CompanionName + " !";
        }
        else
        {
            _text.text = "Press E to free " + _companion.CompanionName;
        }
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
        int currentIndex = player.AddCompanion(_companionController);
        _companionController.SetCompanionIndex(currentIndex);
        HideInteraction();
        _isDisabled = true;
    }
}
