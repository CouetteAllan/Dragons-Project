using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTweener : MonoBehaviour
{
    [SerializeField] private Transform _graphTransform;
    // Start is called before the first frame update
    void Start()
    {
        _graphTransform.DOLocalRotate(Vector3.forward * 360.0f, 1.0f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart).SetId(_graphTransform.parent);
    }
}
