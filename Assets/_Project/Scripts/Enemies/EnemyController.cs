using MoreMountains.Feedbacks;
using System;
using System.Collections;
using UnityEngine;

public enum EnemyState
{
    WalkInRange,
    Attack,
    ReceiveDamage,
    IsDead
}

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour, IHittable, IHitSource, IHealth
{
    public static event Action<EnemyController> OnEnemyDeath;

    [SerializeField] private MMF_Player _feedbackHit,_deathFeedback;
    [SerializeField] private LayerMask _playerLayer;
    public Transform Transform => this.transform;
    public float MaxHealth => _datas.BaseHealth;
    public float CurrentHealth => _currentHealth;

    private Rigidbody2D _rb;
    private EnemyConfig _datas;
    private float _currentHealth;
    private EnemyState _currentState = EnemyState.WalkInRange;

    private PlayerController _player;

    public void Initialize(EnemyConfig datas)
    {
        _datas = datas;
        _currentHealth = _datas.BaseHealth;
        _rb = GetComponent<Rigidbody2D>();
        _player = GameManager.Instance.Player;
    }

    private void FixedUpdate()
    {
        if (_currentState != EnemyState.WalkInRange)
        {
            _rb.velocity *= .5f;
            return;
        }

        WalkInRange();
    }

    public void ReceiveDamage(IHitSource source, float damage)
    {
        //Some hit feedback
        _feedbackHit.PlayFeedbacks();
        _rb.AddForce((this.transform.position - source.Transform.position).normalized * 15.0f);
        ChangeHealth(-damage);

    }

    //TODO: Enemy walk in range, then attack => repeat
    private void ChangeEnemyState(EnemyState state)
    {
        if (state == _currentState)
            return;

        _currentState = state;
        switch (state)
        {
            case EnemyState.WalkInRange:
                WalkInRange();
                break;
            case EnemyState.Attack:
                StartAttack();
                break;
            case EnemyState.ReceiveDamage:
                break;
        }
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
            ChangeEnemyState(EnemyState.IsDead);
        }
    }

    private bool IsDead()
    {
        return _currentHealth <= 0.0f;
    }

    private bool IsInAttackRange()
    {
        var cast = Physics2D.OverlapCircle(this.transform.position, _datas.Range, _playerLayer);
        return cast;
    }

    private void WalkInRange()
    {
        //Check if in range, else we continue to walk toward the player
        if (IsInAttackRange())
        {
            ChangeEnemyState(EnemyState.Attack);
            return;
        }

        var directionTowardPlayer =_player.transform.position - this.transform.position;
        _rb.velocity = directionTowardPlayer.normalized * _datas.BaseSpeed;

    }

    private void StartAttack()
    {
        Debug.Log("Enemy Start his attack");
        StartCoroutine(DelayChangeState());
    }

    IEnumerator DelayChangeState()
    {
        yield return new WaitForSeconds(1.0f);
        ChangeEnemyState(EnemyState.WalkInRange);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(this.transform.position, _datas.Range);
    }
}
