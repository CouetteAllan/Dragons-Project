using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    [SerializeField] private Material _fadeMat;

    public void FadeOut()
    {
        StartCoroutine(FadeCoroutine());
    }
    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }

    private IEnumerator FadeCoroutine()
    {
        float startFade = 0;
        _fadeMat.SetFloat("_FadeTime", startFade);
        while (startFade <= 1.0f)
        {
            _fadeMat.SetFloat("_FadeTime", startFade);
            startFade += Time.deltaTime * .5f;
            yield return null;
        }
        _fadeMat.SetFloat("_FadeTime", 1.0f);

    }

    private IEnumerator FadeInCoroutine()
    {
        float startFade = 1.0f;
        _fadeMat.SetFloat("_FadeTime", startFade);
        while (startFade >= 0.0f)
        {
            Debug.Log("we fadin'");
            _fadeMat.SetFloat("_FadeTime", startFade);
            startFade -= Time.deltaTime * .2f;
            yield return null;
        }
        _fadeMat.SetFloat("_FadeTime", 0.0f);

    }
}
