using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionAnims : MonoBehaviour
{
    [SerializeField] private Transform _graphTransform, _bodyTransform;

    private void Awake()
    {

    }

    private void OnDisable()
    {

    }
    public void CompanionPlayAnim()
    {
        _bodyTransform.DOBlendableLocalRotateBy(Vector3.forward * 360.0f, .5f, RotateMode.FastBeyond360).SetEase(Ease.InOutQuad);
        _bodyTransform.DOBlendableScaleBy(Vector2.one, .4f).SetLoops(2, LoopType.Yoyo).OnComplete(() => _bodyTransform.localScale = Vector3.one);
        
    }

    public void SwapGraphScale(bool facingRight)
    {
        if (facingRight && _graphTransform.localScale.x < 0)
        {
            Swap();
        }
        else if (!facingRight && _graphTransform.localScale.x > 0)
        {
            Swap();
        }
    }

    private void Swap()
    {
        Vector3 localScale = _graphTransform.localScale;
        localScale.x *= -1;
        _graphTransform.localScale = localScale;
    }
}

