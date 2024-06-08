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
        transform.DOScale(Vector3.one, .8f).SetEase(Ease.OutBounce);
        transform.DOPunchPosition(Vector2.up, 3.0f,vibrato: 5);
        Breath();
        _canBePicked = true;
    }

    public void Breath()
    {
        DOTween.To(() => (Vector3)transform.localScale, (v) =>
        {
            if (transform == null)
                return;
            transform.localScale = v;
        }, Vector3.one * 1.2f, 1.0f)
            .SetDelay(.5f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.OutBack);
        //this.gameObject.transform?.DOBlendableScaleBy().SetEase(Ease.OutBack).SetDelay(.5f).SetLoops(-1, LoopType.Yoyo).SetId(this.gameObject).SetAutoKill(true);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_canBePicked)
            return;
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController player))
        {
            if (player.PickUpPowerUp(_datas, this))
                this.GetComponent<DisablerScript>().DisableObject(this);
        }
    }

    public void SetPickObject(bool canBePicked)
    {
        _canBePicked = canBePicked;
    }
}
