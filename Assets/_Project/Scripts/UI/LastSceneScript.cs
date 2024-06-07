using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastSceneScript : MonoBehaviour
{
    public CanvasGroup TextFade;
    public Transform _devText, _graphText;

    private void Awake()
    {
        this.GetComponent<FadeScreen>().FadeIn();
    }

    private void Start()
    {
        FunctionTimer.Create(() => FadeInText(), 6.0f);
        TextFade.transform.localScale = Vector3.zero;
        _devText.transform.localScale = Vector3.zero;
        _graphText.transform.localScale = Vector3.zero;
        TextFade.alpha = 0.0f;
    }

    private void FadeInText()
    {
        
        DOTween.To(() => TextFade.alpha, (value) => TextFade.alpha = value, 1.0f, 2.0f);
        TextFade.transform.DOScale(Vector3.one, 1.0f).OnComplete(() => _devText.transform.DOScale(1.0f,2.0f).SetDelay(2.0f).OnComplete(() => _graphText.DOScale(1.0f,2.0f)));
    }
}
