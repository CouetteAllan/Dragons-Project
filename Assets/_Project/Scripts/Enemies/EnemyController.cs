using MoreMountains.Feedbacks;
using System;
using UnityEngine;

public enum EnemyState
{
    WalkInRange,
    Attack,
    ReceiveDamage
}

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour, IHittable, IHitSource, IHealth
{
    public static event Action<EnemyController> OnEnemyDeath;

    [SerializeField] private MMF_Player _feedbackHit,_deathFeedback;
    public Transform Transform => this.transform;
    public float MaxHealth => _datas.BaseHealth;
    public float CurrentHealth => _currentHealth;

    private Rigidbody2D _rb;
    private EnemyConfig _datas;
    private float _currentHealth;

    

    public void Initialize(EnemyConfig datas)
    {
        _datas = datas;
        _currentHealth = _datas.BaseHealth;
        _rb = GetComponent<Rigidbody2D>();
    }

    public void ReceiveDamage(IHitSource source, float damage)
    {
        //Some hit feedback
        _feedbackHit.PlayFeedbacks();
        _rb.AddForce((source.Transform.position - this.transform.position) * 5.0f);
        ChangeHealth(-damage);

    }

    //TODO: Enemy walk in range, then attack => repeat
    private void ChangeEnemyState(EnemyState state)
    {

    }

    public void ChangeHealth(float healthChange)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + healthChange,0,MaxHealth);
        //Display health on top of the enemy
        if (IsDead())
        {
            //TODO: Explosion feedback
            OnEnemyDeath?.Invoke(this);
            _deathFeedback.PlayFeedbacks();
        }
    }

    private bool IsDead()
    {
        return _currentHealth <= 0.0f;
    }
}
