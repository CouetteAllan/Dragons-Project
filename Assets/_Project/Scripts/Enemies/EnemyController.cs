using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IHittable, IHitSource
{
    [SerializeField] private MMF_Player _feedbackHit;
    private EnemyConfig _datas;
    private float _currentHealth;

    public Transform Transform => this.transform;


    public void Initialize(EnemyConfig datas)
    {
        _datas = datas;
        _currentHealth = _datas.BaseHealth;
    }

    public void ReceiveDamage(IHitSource source, float damage)
    {
        //Some hit feedback
        _feedbackHit.PlayFeedbacks();
    }

    //TODO: Enemy walk in range, then attack => repeat
}
