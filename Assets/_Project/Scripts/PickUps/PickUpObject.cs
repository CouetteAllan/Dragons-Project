using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    [SerializeField] private PickUpEffect _datas;

    public void InitObject()
    {
        //Do Drop anim
        this.transform.localScale = Vector3.one * .1f;
        transform.DOScale(Vector3.one, .2f).SetEase(Ease.OutBounce);
        transform.DOPunchPosition(Vector2.up, .5f);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController player))
        {
            player.PickUpPowerUp(_datas);
            Destroy(this.gameObject);
        }
    }
}
