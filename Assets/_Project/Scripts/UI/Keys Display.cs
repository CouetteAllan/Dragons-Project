using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UtilsClass;
public class KeysDisplay : MonoBehaviour
{
    [SerializeField] private Image[] _keys = new Image[3];
    [SerializeField] private RectTransform _rectTransform;

    private Vector3 _worldPos;
    public void Awake()
    {
        PlayerController.OnPlayerUpdateKeyNumber += UpdateKeyDisplay;
        PlayerController.OnPlayerPickUpKey += PlayerController_OnPlayerPickUpKey;
    }

    private  void Update()
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(_rectTransform,this.gameObject.transform.position,Utils.MainCamera,out _worldPos);
    }

    private void PlayerController_OnPlayerPickUpKey(Transform keyTransform)
    {
        keyTransform.DOMove(_worldPos, .3f).SetEase(Ease.InBack);
        keyTransform.DOScale(Vector3.one * 5.0f, .15f).SetEase(Ease.InOutQuad).SetLoops(2, LoopType.Yoyo).OnComplete(() => Destroy(keyTransform.gameObject));
    }

    public void OnDisable()
    {
        PlayerController.OnPlayerUpdateKeyNumber -= UpdateKeyDisplay;
        PlayerController.OnPlayerPickUpKey -= PlayerController_OnPlayerPickUpKey;

    }

    public void UpdateKeyDisplay(int currentKeys)
    {
        foreach (var key in _keys)
        {
            key.gameObject.SetActive(false);
        }

        for (int i = 0; i < currentKeys; i++)
        {
            _keys[i].gameObject.SetActive(true);
            _keys[i].transform.DOPunchScale(Vector3.one, 1.5f, vibrato: 8, elasticity: .1f).OnComplete(() => _keys[i].transform.localScale = Vector3.one);
        }
    }
}
