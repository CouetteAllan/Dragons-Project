using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDrop : MonoBehaviour
{
    [SerializeField] private PickUpObject _keyToDrop;

    public void DropKey()
    {
        var key = Instantiate(_keyToDrop, this.transform.position, Quaternion.identity);
        key.SetPickObject(canBePicked: false);
        key.transform.DOMove(Vector2.up * 2.0f + Vector2.right * 2.0f, .07f).SetEase(Ease.OutQuart).SetRelative().OnComplete(() =>
        {
            key.transform.DOMoveY(-4.0f, 1.0f).SetEase(Ease.OutBounce).SetRelative();
        });

        key.transform.DOLocalRotate(Vector3.forward * 720.0f, 1.6f, RotateMode.FastBeyond360).SetEase(Ease.OutExpo).OnComplete(() => key.SetPickObject(canBePicked: true));

    }
}
