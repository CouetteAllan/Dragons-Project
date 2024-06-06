using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    [SerializeField] private PickUpEffect _datas;
    private bool _canBePicked = true;
    public void InitObject()
    {
        //Do Drop anim
        this.transform.localScale = Vector3.one * .1f;
        transform.DOScale(Vector3.one, .2f).SetEase(Ease.OutBounce);
        transform.DOPunchPosition(Vector2.up, 3.0f);
        Breath();
        _canBePicked = true;
    }

    public void Breath()
    {
        transform.DOBlendableScaleBy(Vector2.one * .8f, 1.0f).SetEase(Ease.OutBack).SetDelay(.5f).SetLoops(-1, LoopType.Yoyo);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_canBePicked)
            return;
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController player))
        {
            if(player.PickUpPowerUp(_datas))
                Destroy(this.gameObject);
        }
    }

    public void SetPickObject(bool canBePicked)
    {
        _canBePicked = canBePicked;
    }
}
