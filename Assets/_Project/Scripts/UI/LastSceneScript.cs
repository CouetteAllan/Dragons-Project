using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastSceneScript : MonoBehaviour
{
    public CanvasGroup TextFade;

    private void Awake()
    {
        this.GetComponent<FadeScreen>().FadeIn();
    }

    private void Start()
    {
        FunctionTimer.Create(() => FadeInText(), 6.0f);
    }

    private void FadeInText()
    {
        TextFade.transform.localScale = Vector3.zero;
        TextFade.alpha = 0.0f;
        DOTween.To(() => TextFade.alpha, (value) => TextFade.alpha = value, 1.0f, 2.0f);
        TextFade.transform.DOScale(Vector3.one, 1.0f);
    }
}
