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
    private Animator _animator;
    [SerializeField] private EnemyConfig _datas;
    private float _currentHealth;
    private EnemyState _currentState = EnemyState.WalkInRange;

    private PlayerController _player;
    private Vector2 _attackDirection;

    public void Initialize(EnemyConfig datas)
    {
        _datas = datas;
        _currentHealth = _datas.BaseHealth;
        _rb = GetComponent<Rigidbody2D>();
        _player = GameManager.Instance.Player;
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (_currentState != EnemyState.WalkInRange)
        {
            _rb.velocity *= .2f;
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
        _currentState = state;
    }

    public void ChangeHealth(float healthChange)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + healthChange,0,MaxHealth);
        //Display health on top of the enemy
        if (IsDead())
        {
            Destroy(_animator);
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
        

        var directionTowardPlayer =_player.transform.position - this.transform.position;
        _rb.velocity = directionTowardPlayer.normalized * _datas.BaseSpeed;
        _animator.SetFloat("Y Velocity", _rb.velocity.normalized.y);
        if (IsInAttackRange())
        {
            ChangeEnemyState(EnemyState.Attack);
            return;
        }

    }

    private void StartAttack()
    {
        Debug.Log("Enemy Start his attack");
        //Change animator state
        _attackDirection = (_player.transform.position - this.transform.position).normalized;
        _animator.SetFloat("Y Velocity", _rb.velocity.normalized.y);
        _animator.SetTrigger("Attack");
        this._rb.velocity = _attackDirection * 30.0f;
    }


    public void DoAttack()
    {
        //Actual attack
        var attack = Physics2D.CircleCast(this.transform.position, _datas.AttackRadius, _attackDirection, _datas.AttackLenght, _playerLayer);
        if (attack)
        {
            if (attack.rigidbody.TryGetComponent(out PlayerController playerHit))
            {
                playerHit.ReceiveDamage(this, _datas.BaseDamage);
            }
        }
    }

    public void EndAttackAnimation()
    {
        ChangeEnemyState(EnemyState.WalkInRange);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(this.transform.position, _datas.Range);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position,_datas.AttackRadius);
        Gizmos.DrawWireSphere(this.transform.position + (transform.right * (_datas.AttackLenght)),_datas.AttackRadius);
    }
}
